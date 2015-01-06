﻿using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KcvPlugins
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();

            AddPlugin(new AMing.DebugExtensions.Entrance());
            AddPlugin(new AMing.SettingsExtensions.Entrance());
        }


        private void AddPlugin(IToolPlugin plugin)
        {
            var tabitem = new TabItem
            {
                Header = plugin.ToolName,
                Content = plugin.GetToolView()
            };

            tabControl.Items.Add(tabitem);
        }
    }
}
