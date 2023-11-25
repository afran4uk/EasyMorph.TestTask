using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SourceInitialized += OnSourceInitialized;
            Closing += OnClosing;
            KeyDown += OnKeyDown;
            PreviewMouseUp += OnPreviewMouseUp;

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                MyEasyMorphPanel.RemoveSelectedShape();
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            MyEasyMorphPanel.Dispose();
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            MyEasyMorphPanel.AfterInitComponents();
        }

        private void OnAddNewRectangleClick(object sender, RoutedEventArgs e)
        {
            MyEasyMorphPanel.AddNewShape();
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MyEasyMorphPanel.ActionMouseUp();
        }
    }
}
