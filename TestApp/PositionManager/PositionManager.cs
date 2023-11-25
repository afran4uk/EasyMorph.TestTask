using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using TestApp.PositionManager.Dto;

namespace TestApp.PositionManager
{
    public class PositionManager
    {
        private readonly string _positionFileName;

        public PositionManager()
        {
            _positionFileName = "positions.txt";
        }

        /// <summary>
        /// Save our shapes and window position.
        /// </summary>
        /// <param name="collection">Shapes collection.</param>
        public void SaveShapePositions(UIElementCollection collection)
        {
            var shapeCollection = new List<ShapePositionInfo>();
            foreach (UIElement uiElement in collection)
            {
                if (uiElement is ContentControl contentControl)
                {
                    shapeCollection.Add(new ShapePositionInfo
                    {
                        Left = Canvas.GetLeft(contentControl),
                        Top = Canvas.GetTop(contentControl),
                        Height = contentControl.ActualHeight,
                        Width = contentControl.ActualWidth,
                        ZIndex = Panel.GetZIndex(contentControl)
                    });
                }
            }

            var mainWindow = Application.Current.MainWindow;

            var mainWindowInfo = new WindowPositionInfo
            {
                ShapePositionInfos = shapeCollection,
                Left = Canvas.GetLeft(mainWindow),
                Top = Canvas.GetTop(mainWindow),
                Height = mainWindow.ActualHeight,
                Width = mainWindow.ActualWidth
            };

            var json = JsonConvert.SerializeObject(mainWindowInfo);
            File.WriteAllText(_positionFileName, json);
        }

        /// <summary>
        /// Get window and shapes position from file.
        /// </summary>
        /// <returns></returns>
        public WindowPositionInfo GetMainWindowPosition()
        {
            if (File.Exists(_positionFileName) == false)
                return null;
            
            var text = File.ReadAllText(_positionFileName);
            if (string.IsNullOrEmpty(text))
                return null;

            try
            {
                return JsonConvert.DeserializeObject<WindowPositionInfo>(text);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
