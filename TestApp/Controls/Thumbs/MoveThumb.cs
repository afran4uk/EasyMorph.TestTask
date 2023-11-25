using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System;

namespace TestApp.Controls.Thumbs
{
    public class MoveThumb : Thumb
    {
        private Panel _panel;
        public MoveThumb()
        {
            DragDelta += MoveThumb_DragDelta;
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is Control designerItem)
            {
                var left = Canvas.GetLeft(designerItem) + e.HorizontalChange;
                var top = Canvas.GetTop(designerItem) + e.VerticalChange;

                if (_panel == null)
                    _panel = FindParentPanel(designerItem);

                left = Math.Max(0, Math.Min(_panel.ActualWidth - designerItem.ActualWidth, left));
                top = Math.Max(0, Math.Min(_panel.ActualHeight - designerItem.ActualHeight, top));

                Canvas.SetLeft(designerItem, left);
                Canvas.SetTop(designerItem, top);
            }
        }

        private Panel FindParentPanel(Control contentControl)
        {
            var parent = VisualTreeHelper.GetParent(contentControl);

            while (parent != null && !(parent is Panel))
                parent = VisualTreeHelper.GetParent(parent);

            return parent as Panel;
        }
    }
}
