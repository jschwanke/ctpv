using System;
using System.Drawing;
using System.Windows.Forms;
using Viewer.Controller;
using Viewer.Core;

namespace Viewer.View.MDI
{
    public partial class MDIControl : Form
    {
        private readonly Presenter presenter;
        private readonly Guid imageID;
        private readonly ImageControl transversal;
        private readonly ImageControl sagittal;
        private readonly ImageControl frontal;
        private readonly GWControl gw;

        public MDIControl(Presenter presenter, Guid imageID, int scrollTransversal,
            int scrollSagittal, int scrollFrontal, Image imageTransversal,
            Image imageSagittal, Image imageFrontal)
        {
            this.presenter = presenter;
            this.imageID = imageID;
            InitializeComponent();
            EventHandler eventHandler = TrackBar_Scroll;

            transversal = new ImageControl(imageTransversal, scrollTransversal, eventHandler);
            sagittal = new ImageControl(imageSagittal, scrollSagittal, eventHandler);
            frontal = new ImageControl(imageFrontal, scrollFrontal, eventHandler);

            tableLayoutPanel.Controls.Add(sagittal, 0, 0);
            transversal.Dock = DockStyle.Fill;

            tableLayoutPanel.Controls.Add(transversal, 1, 0);
            sagittal.Dock = DockStyle.Fill;

            tableLayoutPanel.Controls.Add(frontal, 0, 1);
            frontal.Dock = DockStyle.Fill;

            gw = new GWControl(TrackBar_Scroll);
            tableLayoutPanel.Controls.Add(gw, 1, 1);
            gw.Dock = DockStyle.Fill;
        }

        public Guid ImageID
        {
            get { return imageID; }
        }

        public int GWWidth
        {
            get { return gw.GWWidth; }
        }

        public int GWCenter
        {
            get { return gw.GWCenter; }
        }

        public int GetScrollPosition(ImageCube.EImageOrientation orientation)
        {
            int position = 0;
            switch (orientation)
            {
                case ImageCube.EImageOrientation.Transversal:
                    position = transversal.ScrollPosition;
                    break;
                case ImageCube.EImageOrientation.Sagittal:
                    position = sagittal.ScrollPosition;
                    break;
                case ImageCube.EImageOrientation.Frontal:
                    position = frontal.ScrollPosition;
                    break;
            }
            return position;
        }

        public void UpdateImage(ImageCube.EImageOrientation orientation, Image image)
        {
            switch (orientation)
            {
                case ImageCube.EImageOrientation.Transversal:
                    transversal.Image = image;
                    break;
                case ImageCube.EImageOrientation.Sagittal:
                    sagittal.Image = image;
                    break;
                case ImageCube.EImageOrientation.Frontal:
                    frontal.Image = image;
                    break;
            }
        }

        public void TrackBar_Scroll(object sender, EventArgs e)
        {
            presenter.UpdateImages(this);
        }

        public void DrawLine(ImageCube.EImageOrientation orientation, Line line1, Line line2)
        {
            switch (orientation)
            {
                case ImageCube.EImageOrientation.Transversal:
                    transversal.DrawLine(line1, line2);
                    break;
                case ImageCube.EImageOrientation.Sagittal:
                    sagittal.DrawLine(line1, line2);
                    break;
                case ImageCube.EImageOrientation.Frontal:
                    frontal.DrawLine(line1, line2);
                    break;
            }
        }
    }
}