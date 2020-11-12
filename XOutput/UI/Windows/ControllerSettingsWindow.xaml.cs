﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using XOutput.Devices;

namespace XOutput.UI.Windows
{
    /// <summary>
    /// Interaction logic for ControllerSettings.xaml
    /// </summary>
    public partial class ControllerSettingsWindow : Window, IViewBase<ControllerSettingsViewModel, ControllerSettingsModel>
    {
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly ControllerSettingsViewModel viewModel;
        public ControllerSettingsViewModel ViewModel => viewModel;
        private readonly GameController controller;

        public ControllerSettingsWindow(ControllerSettingsViewModel viewModel, GameController controller)
        {
            this.controller = controller;
            this.viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.Update();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            viewModel.Update();
        }

        protected override void OnClosed(EventArgs e)
        {
            timer.Tick -= TimerTick;
            timer.Stop();
            viewModel.Dispose();
            base.OnClosed(e);
        }

        private void ConfigureAllButtonClick(object sender, RoutedEventArgs e)
        {
            viewModel.ConfigureAll();
        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            viewModel.SetStartWhenConnected();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SetForceFeedback();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            controller.Mapper.Name = ViewModel.Model.Title;
        }
    }
}
