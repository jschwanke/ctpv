using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Viewer.View.MDI;

namespace Viewer.View
{
    public partial class View : Form
    {
        private readonly ListBox listBox;
        private readonly Presenter.Presenter presenter;

        public View(Presenter.Presenter presenter)
        {
            this.presenter = presenter;
            InitializeComponent();
            this.SizeChanged += new EventHandler(View_SizeChanged);
            this.Closing += new CancelEventHandler(Exit_Click);
            listBox = new ListBox();
            listBox.FormattingEnabled = true;
            listBox.Location = new System.Drawing.Point(12, 61);
            listBox.Name = "listBox";
            listBox.Size = new System.Drawing.Size(120, 95);
            listBox.TabIndex = 4;
            listBox.HorizontalScrollbar = true;
            listBox.ScrollAlwaysVisible = true;
            listBox.Dock = DockStyle.Fill;
            listBox.Click += new EventHandler(ListBox_Click);
            splitter.Controls.Add(listBox);
            listBox.Visible = false;
        }

        /// <summary>
        /// Erzeugt ein neues MDIControl Fenster.
        /// </summary>
        /// <param name="imageID">Die ImageID des ImageCube.</param>
        /// <param name="scrollTransversal">Die maximale Scrolllänge des transversalen Bildes.</param>
        /// <param name="scrollSagittal">Die maximale Scrolllänge des sgittalen Bildes.<</param>
        /// <param name="scrollFrontal">Die maximale Scrolllänge des frontalen Bildes.<</param>
        /// <param name="imageTransversal">Das Transversale Bild.</param>
        /// <param name="imageSagittal">Das Sagittale Bild.</param>
        /// <param name="imageFrontal">Das Frontale Bild.</param>
        /// <param name="filename">Der Dateiname des ImageCube.</param>
        /// <returns>Gibt das erzeugte IMDICOntrol zurück.</returns>
        public MDIControl OpenMDIControl(Guid imageID, int scrollTransversal,
                                            int scrollSagittal, int scrollFrontal, Image imageTransversal,
                                            Image imageSagittal, Image imageFrontal, string filename)
        {
            MDIControl form = new MDIControl(presenter, imageID, scrollTransversal,
                scrollSagittal, scrollFrontal, imageTransversal,
                imageSagittal, imageFrontal);
            int height = (int)(this.Size.Height * 0.8);
            int width = (int)((this.Size.Width - 250) * 0.9);
            form.Size = new Size(width, height);
            form.MdiParent = this;
            form.Text = filename;
            form.Closing += new CancelEventHandler(MDI_Closed);
            form.Show();
            listBox.Visible = true;
            listBox.Items.Add(filename);
            return form;
        }

        /// <summary>
        /// Schließt das übergebende MDIControl.
        /// </summary>
        /// <param name="mdiControl">Das zuschließende IMDIControl.</param>
        public void CloseMDIControl(MDIControl mdiControl)
        {
            mdiControl.Close();
            listBox.Items.Remove(mdiControl.Text);
            if(listBox.Items.Count == 0)
            {
                listBox.Visible = false;
            }
            presenter.CloseMDIControl(presenter.GetImageCubeByFilename(this.ActiveMdiChild.Text));
        }

        /// <summary>
        /// Setzt eine Nachricht in der Statusleiste.
        /// </summary>
        /// <param name="message">Die zusetztende Nachricht.</param>
        public void SetInformationMessage(string message)
        {
            toolStripStatusLabel.Text = message;
        }

        /// <summary>
        /// Löscht die derzeit angezeigte Nachricht in der Statusleiste.
        /// </summary>
        public void ClearInformationMessage()
        {
            toolStripStatusLabel.Text = "Status";
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.presenter.CreateImageCubes(openFileDialog.FileNames);
            }
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.presenter.CreateImageCubesFromDirectory(folderBrowserDialog.SelectedPath);
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }
            listBox.Items.Clear();
            listBox.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "Wollen Sie wirklich beenden?";
            DialogResult result = MessageBox.Show(msg,
                                                  Application.ProductName,
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                this.presenter.CloseApplication();
            }
        }

        private void Exit_Click(object sender, CancelEventArgs e)
        {
            string msg = "Wollen Sie wirklich beenden?";
            DialogResult result = MessageBox.Show(msg,
                                                  Application.ProductName,
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                this.presenter.CloseApplication();
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void View_SizeChanged(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                int height = (int)(this.Size.Height * 0.8);
                int width = (int)((this.Size.Width - 250) * 0.9);
                form.Size = new Size(width, height);
            }
        }

        public void MDI_Closed(object sender, CancelEventArgs e)
        {
            MDIControl mdi = (MDIControl) sender;
            listBox.Items.Remove(mdi.Text);
            if (listBox.Items.Count == 0)
            {
                listBox.Visible = false;
            }
            presenter.CloseMDIControl(presenter.GetImageCubeByFilename(mdi.Text));
            e.Cancel = false;
        }

        public void ListBox_Click(object sender, EventArgs e)
        {
            string filename = (string) listBox.SelectedItem;
            foreach (Form form in this.MdiChildren)
            {
                if(form.Text == filename)
                {
                    form.Activate();
                    break;
                }
            }
        }

        public void SetActiveMDI(string name)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.Text == name)
                {
                    form.Activate();
                    form.Focus();
                    break;
                }
            }
        }
    }
}