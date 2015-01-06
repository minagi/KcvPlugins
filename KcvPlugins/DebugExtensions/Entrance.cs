﻿using Grabacr07.KanColleViewer.Composition;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.DebugExtensions
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "DebugExtensions")]
    [ExportMetadata("Description", "KCV Debug Extensions")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("Author", "@AMing")]
    public class Entrance : IToolPlugin
    {
        public string ToolName
        {
            get { return "DebugExtensions"; }
        }

        public object GetToolView()
        {
            return new TextBlock
            {
                Text = "拦截未处理的全局异常"
            };
        }

        public object GetSettingsView()
        {
            return null;
        }

        public Entrance()
        {
            Init();
        }

        ~Entrance()
        {
            Exit();
        }

        private void Init()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //#if DEBUG
            MessageBox.Show(string.Format("Error:{0}\n{1}\n{2}",
                e.Exception.Message,
                e.Exception.Source,
                e.Exception.StackTrace));
            //#endif
        }

        private void Exit()
        {
        }
    }
}
