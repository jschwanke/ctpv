using System;
using System.Windows.Forms;
using Viewer.Properties;

namespace Viewer.View.MDI
{
    public partial class GWControl : UserControl
    {
        public GWControl(EventHandler scrollHandler)
        {
            InitializeComponent();
            trackBarCenter.Scroll += scrollHandler;
            trackBarWidth.Scroll += scrollHandler;

            Settings settings = new Settings();

            trackBarWidth.Minimum = settings.gw_width_min;
            trackBarWidth.Maximum = settings.gw_width_max;
            trackBarWidth.Value = settings.gw_width;
            trackBarWidth.TickFrequency = 100;
            groupBoxWidth.Text = "Width (Contrast) : " + trackBarWidth.Value;

            trackBarCenter.Minimum = settings.gw_center_min;
            trackBarCenter.Maximum = settings.gw_center_max;
            trackBarCenter.Value = settings.gw_center;
            trackBarCenter.TickFrequency = 250;
            groupBoxCenter.Text = "Center (Brightness) : " + trackBarCenter.Value;
        }

        public int GWWidth
        {
            get { return trackBarWidth.Value; }
        }

        public int GWCenter
        {
            get { return trackBarCenter.Value; }
        }

        private void trackBarWidth_Scroll(object sender, EventArgs e)
        {
            groupBoxWidth.Text = "Width (Contrast) : " + trackBarWidth.Value;
        }

        private void trackBarCenter_Scroll(object sender, EventArgs e)
        {
            groupBoxCenter.Text = "Center (Brightness) : " + trackBarCenter.Value;
        }
    }
}
