using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TestApp.PositionManager.Dto;
using TestApp.PositionManager.Dto.Base;

namespace TestApp.Controls.Panels
{
    /// <summary>
    /// Interaction logic for EasyMorphPanel.xaml
    /// </summary>
    public partial class EasyMorphPanel : UserControl, IDisposable
    {
        private Random _random;
        private ContentControl _selectedContentControl;
        private readonly PositionManager.PositionManager _positionManager;

        private readonly Brush _blueBrush;
        private readonly Brush _orangeBrush;

        public EasyMorphPanel()
        {
            _random = new Random();
            _positionManager = new PositionManager.PositionManager();

            _blueBrush = (Brush)FindResource("BlueBrush");
            _orangeBrush = (Brush)FindResource("OrangeBrush");

            InitializeComponent();
        }

        /// <summary>
        /// Add new shape to panel.
        /// </summary>
        public void AddNewShape()
        {
            AddNewShape(ShapePositionInfo.GetRandomShapePosition(CanvasLayout));
        }

        /// <summary>
        /// Save positions after any actions with mouse.
        /// </summary>
        public void ActionMouseUp()
        {
            _positionManager.SaveShapePositions(CanvasLayout.Children);
        }

        /// <summary>
        /// Init all panel components after initialize main window.
        /// </summary>
        public void AfterInitComponents()
        {
            var windowInfo = _positionManager.GetMainWindowPosition();
            if (windowInfo == null)
                return;

            var mainWindow = Application.Current.MainWindow;

            if (mainWindow != null)
            {
                mainWindow.Top = windowInfo.Top;
                mainWindow.Height = windowInfo.Height;
                mainWindow.Width = windowInfo.Width;
                mainWindow.Left = windowInfo.Left;
            }

            var positions = windowInfo.ShapePositionInfos;

            if (positions == null)
                return;

            foreach (var shapePositionInfo in positions)
                AddNewShape(shapePositionInfo);
        }

        /// <summary>
        /// Remove selected shape and change it stroke brush.
        /// </summary>
        public void RemoveSelectedShape()
        {
            if (_selectedContentControl == null) 
                return;

            _selectedContentControl.PreviewMouseDown -= OnContentControlMouseDown;
            CanvasLayout.Children.Remove(_selectedContentControl);
            _selectedContentControl = null;

            _positionManager.SaveShapePositions(CanvasLayout.Children);
        }

        /// <summary>
        /// Add new shape with specific shape position.
        /// </summary>
        /// <param name="shapePositionInfo">Previous shape position.</param>
        private void AddNewShape(ShapePositionInfo shapePositionInfo)
        {
            var newContentControl = new ContentControl
            {
                Width = shapePositionInfo.Width,
                Height = shapePositionInfo.Height,
                MinHeight = 20,
                MinWidth = 20,
                Template = (ControlTemplate)FindResource("ResizeShapeItemTemplate")
            };

            if (shapePositionInfo.ZIndex != null)
                Panel.SetZIndex(newContentControl, shapePositionInfo.ZIndex.Value);

            var newRectangle = new Rectangle
            {
                Fill = Brushes.Transparent,
                StrokeThickness = 1,
                Stroke = _blueBrush,
                IsHitTestVisible = false
            };

            newContentControl.Content = newRectangle;

            newContentControl.PreviewMouseDown += OnContentControlMouseDown;

            Canvas.SetLeft(newContentControl, shapePositionInfo.Left);
            Canvas.SetTop(newContentControl, shapePositionInfo.Top);

            CanvasLayout.Children.Add(newContentControl);
        }

        /// <summary>
        /// Handler to mouse down action.
        /// </summary>
        private void OnContentControlMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tempContentControl = sender as ContentControl;

            if (tempContentControl == _selectedContentControl)
                return;

            if (_selectedContentControl?.Content is Shape previousShape)
                previousShape.Stroke = _blueBrush;
            
            _selectedContentControl = tempContentControl;

            if (_selectedContentControl?.Content is Shape shape)
                shape.Stroke = _orangeBrush;

            RecalculateZIndexes();
        }

        /// <summary>
        /// Recalculate ZIndex for each elements.
        /// </summary>
        private void RecalculateZIndexes()
        {
            var zIndex = 0;
            Panel.SetZIndex(_selectedContentControl, int.MaxValue);

            foreach (UIElement canvasLayoutChild in CanvasLayout.Children)
            {
                if (canvasLayoutChild != _selectedContentControl)
                    Panel.SetZIndex(canvasLayoutChild, zIndex++);
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            _positionManager.SaveShapePositions(CanvasLayout.Children);
        }
    }
}
