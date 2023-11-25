using System;
using System.Windows;
using TestApp.PositionManager.Dto.Base;

namespace TestApp.PositionManager.Dto
{
    [Serializable]
    public class ShapePositionInfo : BasePositionInfo
    {
        public static ShapePositionInfo GetRandomShapePosition(FrameworkElement element)
        {
            var random = new Random();

            return new ShapePositionInfo
            {
                Height = random.Next(100, 250),
                Width = random.Next(100, 250),
                Left = random.Next((int)element.ActualWidth / 8, (int)element.ActualWidth / 2),
                Top = random.Next((int)element.ActualHeight / 8, (int)element.ActualHeight / 2)
            };
        }
    }
}
