using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CTViewer.Presenter;

namespace CTViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainPresenter presenter = new MainPresenter();
        }
    }
}