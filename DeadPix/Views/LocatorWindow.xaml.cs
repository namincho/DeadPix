﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DeadPix.Business.Locator;

namespace DeadPix.Views
{
    /// <summary>
    /// Interaction logic for LocatorWindow.xaml
    /// </summary>
    public partial class LocatorWindow
    {
        #region Variables
        private readonly LocatorController _locatorController;
        private readonly SolidColorBrush _brush;
        private readonly bool _changeColorOnClick;
        #endregion

        /// <summary>
        /// Initialize a new LocatorWindow
        /// </summary>
        /// <param name="controller">The LocatorController that can be used to specify certain parameters</param>
        public LocatorWindow(LocatorController controller)
        {
            _locatorController = controller;
            InitializeComponent();

            _brush = new SolidColorBrush(_locatorController.SelectedColor);
            Background = _brush;

            try
            {
                _changeColorOnClick = Properties.Settings.Default.LocatorChangeColorOnClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeadPix", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (_locatorController.RandomizeColors)
            {
                _locatorController.ColorChangedEvent += LocatorControllerOnColorChangedEvent;
            }
        }

        /// <summary>
        /// Method that is called when the LocatorController object has changed the selected color
        /// </summary>
        /// <param name="color">The new color that was generated by the LocatorController object</param>
        private void LocatorControllerOnColorChangedEvent(Color color)
        {
            _brush.Color = color;
        }

        /// <summary>
        /// Method that is called when a mouse button is down
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_changeColorOnClick || _locatorController.RandomizeColors) return;

            _locatorController.SelectedColor = _locatorController.GenerateColor();
            _brush.Color = _locatorController.SelectedColor;
        }

        /// <summary>
        /// Method that is called when the user has double clicked
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The MouseButtonEventArgs</param>
        private void Window_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Method that is called when the window has loaded
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            // This is a workaround to make the window display maximized on the active monitor instead of the primary monitor
            WindowState = WindowState.Maximized;
        }
    }
}
