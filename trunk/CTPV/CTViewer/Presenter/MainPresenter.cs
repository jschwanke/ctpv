using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CTViewer.View;


namespace CTViewer.Presenter
{
    public class MainPresenter
    {
        private MainView view;

        public MainPresenter()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(view = new MainView());
        }


    }
}
