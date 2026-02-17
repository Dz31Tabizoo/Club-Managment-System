using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WPF_APP.Helpers
{

    public static class WindowHelper
    {
        //  Drags 
        public static void EnableDrag(Window window, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }

        // Double-Click Toggle Fullscreen
        public static void HandleMaximizeDoubleClick(Window window, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                window.WindowState = (window.WindowState == WindowState.Maximized)
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        }

        public static void CloseApp() => Application.Current.Shutdown();
    }
}


