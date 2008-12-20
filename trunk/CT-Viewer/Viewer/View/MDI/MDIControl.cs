using System;
using System.Drawing;
using System.Windows.Forms;
using InterfaceLayer.Enumeration;
using Viewer.Presenter;

namespace Viewer.View.MDI
{
    public partial class MDIControl : Form
    {
        private readonly Presenter.Presenter presenter;
        private readonly Guid imageID;
        private readonly ImageControl transversal;
        private readonly ImageControl sagittal;
        private readonly ImageControl frontal;
        private readonly GWControl gw;

        public MDIControl(Presenter.Presenter presenter, Guid imageID, int scrollTransversal,
            int scrollSagittal, int scrollFrontal, Image imageTransversal,
            Image imageSagittal, Image imageFrontal)
        {
            this.presenter = presenter;
            this.imageID = imageID;
            InitializeComponent();
            EventHandler eventHandler = new EventHandler(TrackBar_Scroll);

            transversal = new ImageControl(ImageOrientation.Transversal, 
                imageTransversal, scrollTransversal, eventHandler);

            sagittal = new ImageControl(ImageOrientation.Sagittal,
                imageSagittal, scrollSagittal, eventHandler);

            frontal = new ImageControl(ImageOrientation.Frontal,
                imageFrontal, scrollFrontal, eventHandler);

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

        /// <summary>
        /// Liefert oder setzt den Width Wert (Contrast).
        /// </summary>
        public int GWWidth
        {
            get { return gw.GWWidth; }
        }

        /// <summary>
        /// Setzt oder liefert den Center Wert (Birghtness).
        /// </summary>
        public int GWCenter
        {
            get { return gw.GWCenter; }
        }

        /// <summary>
        /// Liefert die Scroll Position abhängig von der Orientierung.
        /// </summary>
        /// <param name="orientation">Orientierung des Bildes.</param>
        /// <returns></returns>
        public int GetScrollPosition(ImageOrientation orientation)
        {
            int position = 0;
            if (orientation == ImageOrientation.Transversal)
            {
                position = transversal.ScrollPosition;
            }
            else if (orientation == ImageOrientation.Sagittal)
            {
                position = sagittal.ScrollPosition;
            }
            else if (orientation == ImageOrientation.Frontal)
            {
                position = frontal.ScrollPosition;
            }
            return position;
        }

        /// <summary>
        /// Updated das Image abhängig von der Orientierung.
        /// </summary>
        /// <param name="orientation">Orientierung des zu erneuernden Images.</param>
        /// <param name="image">Das neue Image.</param>
        public void UpdateImage(ImageOrientation orientation, Image image)
        {
            if(orientation == ImageOrientation.Transversal)
            {
                transversal.Image = image;
            }
            else if(orientation == ImageOrientation.Sagittal)
            {
                sagittal.Image = image;
            }
            else if (orientation == ImageOrientation.Frontal)
            {
                frontal.Image = image;
            }
        }

        public void TrackBar_Scroll(object sender, EventArgs e)
        {
            presenter.UpdateImages(this);
        }

        public void DrawLine(ImageOrientation orientation, Line line1, Line line2)
        {
            if (orientation == ImageOrientation.Transversal)
            {
                transversal.DrawLine(line1, line2);
            }
            else if (orientation == ImageOrientation.Sagittal)
            {
                sagittal.DrawLine(line1, line2);
            }
            else if (orientation == ImageOrientation.Frontal)
            {
                frontal.DrawLine(line1, line2);
            }
        }
    }
}