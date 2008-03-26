using System;
using System.Drawing;
using System.Windows.Forms;
using InterfaceLayer.Enumeration;
using Viewer.Presenter;

namespace Viewer.View.MDI
{
    public partial class ImageControl : UserControl
    {
        private Graphics pictureGraphic;
        private Image saveImage;

        public ImageControl(ImageOrientation orientation, Image image, 
            int scrollMax, EventHandler scrollHandler)
        {
            InitializeComponent();
            this.trackBar.Scroll += scrollHandler;
            Image = image;
            trackBar.Minimum = 0;
            trackBar.Maximum = scrollMax-1;
            trackBar.Value = (scrollMax-1)/2;
        }

        public Image Image
        {
            set
            {
                saveImage = value;
                pictureBox.Image = new Bitmap(saveImage.Width, saveImage.Height);
                pictureGraphic = Graphics.FromImage(pictureBox.Image);
                pictureGraphic.DrawImage(saveImage, 0, 0, saveImage.Width, saveImage.Height);
                pictureBox.Refresh();
            }
        }

        public int ScrollPosition
        {
            get { return trackBar.Value; }
            set { trackBar.Value = value; }
        }

        public void DrawLine(Line line1, Line line2)
        {
            pictureGraphic.DrawImage(saveImage, 0, 0, saveImage.Width, saveImage.Height);
            pictureGraphic.DrawLine(Pens.GreenYellow, line1.Start, line1.End);
            pictureGraphic.DrawLine(Pens.GreenYellow, line2.Start, line2.End);
            pictureBox.Refresh();
        }
    }
}
