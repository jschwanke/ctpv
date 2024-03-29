using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Core;
using InterfaceLayer.Enumeration;
using InterfaceLayer.Image;
using Viewer.Properties;
using Viewer.View.MDI;

namespace Viewer.Presenter
{
    public class Presenter
    {
        private readonly View.View view;
        private readonly CubeManager manager;
        private readonly Dictionary<Guid, Position> lastPosition;

        public Presenter()
        {
            manager = new CubeManager();
            lastPosition = new Dictionary<Guid, Position>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(view = new View.View(this));
        }

        /// <summary>
        /// Erzeugt neue IImageCube Objekte aus den übergebenen Dateinamen.
        /// </summary>
        /// <param name="filenames">
        /// Die Dateinamen, welche die Daten der IImageCube Objekte enthalten.
        /// </param>
        public void CreateImageCubes(string[] filenames)
        {
            foreach (string filename in filenames)
            {
                CreateImageCube(filename);
            }
        }

        /// <summary>
        /// Erzeugt ein neues IImageCube Objekt aus dem übergebenen Dateinamen.
        /// </summary>
        /// <param name="filename">
        /// Der Dateiname, welcher die Daten des IImageCube Objekts enthält.
        /// </param>
        public void CreateImageCube(string filename)
        {
            string hedFile = "";
            string ctxFile = "";

            if (manager.GetImageCubeByFilename(filename) == Guid.Empty)
            {
                if (filename.EndsWith(".hed"))
                {
                    hedFile = filename;
                    ctxFile = filename.Replace(".hed", ".ctx");
                }
                else if (filename.EndsWith(".ctx"))
                {
                    ctxFile = filename;
                    hedFile = filename.Replace(".ctx", ".hed");
                }
                else
                {
                    //Error
                }

                IImageCube imageCube = manager.CreateImageCube(hedFile, ctxFile);
                Settings settings = new Settings();
                int width = settings.gw_width;
                int center = settings.gw_center;

                int transversal = (imageCube.DimZ - 1) / 2;
                int sagittal = (imageCube.DimY - 1) / 2;
                int frontal = (imageCube.DimX - 1) / 2;
                int pos = imageCube.TrafoSliceToScreenPos(transversal);

                lastPosition.Add(imageCube.ImageID, new Position(width, center, transversal, sagittal, frontal));

                //Transversal
                Image transversalImage = Helper.GetImage(
                    imageCube.GetSlice(ImageOrientation.Transversal, transversal),
                    center, width, imageCube.Dimension);

                //Sagittal
                Image sagittalImage = Helper.GetImage(
                    imageCube.GetSlice(ImageOrientation.Sagittal, sagittal),
                    center, width, imageCube.Dimension);

                //Frontal
                Image fronatlImage = Helper.GetImage(
                    imageCube.GetSlice(ImageOrientation.Frontal, frontal),
                    center, width, imageCube.Dimension);

                MDIControl mdiControl = view.OpenMDIControl(imageCube.ImageID, imageCube.DimZ, imageCube.DimY, imageCube.DimX,
                    transversalImage, sagittalImage, fronatlImage, filename);

                Line line1 = new Line(0, frontal, imageCube.Dimension, frontal);
                Line line2 = new Line(sagittal, 0, sagittal, imageCube.Dimension);
                mdiControl.DrawLine(ImageOrientation.Transversal, line1, line2);

                line1 = new Line(0, pos, imageCube.Dimension, pos);
                line2 = new Line(frontal, 0, frontal, imageCube.Dimension);
                mdiControl.DrawLine(ImageOrientation.Sagittal, line1, line2);

                line1 = new Line(0, pos, imageCube.Dimension, pos);
                line2 = new Line(sagittal, 0, sagittal, imageCube.Dimension);
                mdiControl.DrawLine(ImageOrientation.Frontal, line1, line2);
            }
            else
            {
                view.SetActiveMDI(filename);
            }
        }

        /// <summary>
        /// Erzeugt neue IImageCube Objekte aus dem übergebenen Verzeichnisspfad.
        /// </summary> 
        /// <param name="directoryPath">
        /// In dem Verzeichnisspfad befinden sich die Dateinen um IImageCube Objekte
        /// zu erzeugen.
        /// </param>
        public void CreateImageCubesFromDirectory(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            if(directory.Exists)
            {
                foreach (FileInfo file in directory.GetFiles("*.hed"))
                {
                    CreateImageCube(file.FullName);
                }
            }
            else
            {
                
            }
        }

        /// <summary>
        /// Beendet das Programm.
        /// </summary>
        public void CloseApplication()
        {
            manager.Dispose();
            view.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// Aktualisiert die Images des IMDIControls, wenn beispielweise
        /// eine andere Schicht ausgewählt wurde. 
        /// </summary>
        /// <param name="mdiControl">
        /// Das IMDIControl welches eine Aktualisierung ihrer ImageCubes verlagt.
        /// </param>
        public void UpdateImages(MDIControl mdiControl)
        {
            IImageCube imageCube = manager.GetImageCube(mdiControl.ImageID);
            Position position = lastPosition[imageCube.ImageID]; 

            int width = mdiControl.GWWidth;
            int center = mdiControl.GWCenter;

            int transversal = mdiControl.GetScrollPosition(ImageOrientation.Transversal);
            int sagittal = mdiControl.GetScrollPosition(ImageOrientation.Sagittal);
            int frontal = mdiControl.GetScrollPosition(ImageOrientation.Frontal);
            int pos = imageCube.TrafoSliceToScreenPos(transversal);


            if(position.LastWidth != width || position.LastCenter != center)
            {
                position.LastWidth = width;
                position.LastCenter = center;

                //Transversal
                Image transversalImage = Helper.GetImage(
                    imageCube.GetSlice(ImageOrientation.Transversal, transversal),
                    center, width, imageCube.Dimension);

                //Sagittal
                Image sagittalImage = Helper.GetImage(
                    imageCube.GetSlice(ImageOrientation.Sagittal, sagittal),
                    center, width, imageCube.Dimension);

                //Frontal
                Image fronatlImage = Helper.GetImage(
                    imageCube.GetSlice(ImageOrientation.Frontal, frontal),
                    center, width, imageCube.Dimension);

                mdiControl.UpdateImage(ImageOrientation.Transversal, transversalImage);
                mdiControl.UpdateImage(ImageOrientation.Sagittal, sagittalImage);
                mdiControl.UpdateImage(ImageOrientation.Frontal, fronatlImage);
            }

            else if(position.LastTransversal != transversal)
            {
                position.LastTransversal = transversal;

                Image image = Helper.GetImage(
                imageCube.GetSlice(ImageOrientation.Transversal, mdiControl.GetScrollPosition(ImageOrientation.Transversal)),
                center, width, imageCube.Dimension);

                mdiControl.UpdateImage(ImageOrientation.Transversal, image);
            }
            else if(position.LastSagittal != sagittal)
            {
                position.LastSagittal = sagittal;

                Image image = Helper.GetImage(
                imageCube.GetSlice(ImageOrientation.Sagittal, mdiControl.GetScrollPosition(ImageOrientation.Sagittal)),
                center, width, imageCube.Dimension);

                mdiControl.UpdateImage(ImageOrientation.Sagittal, image);

            }
            else if (position.LastFrontal != frontal)
            {
                position.LastFrontal = frontal;

                Image image = Helper.GetImage(
                imageCube.GetSlice(ImageOrientation.Frontal, mdiControl.GetScrollPosition(ImageOrientation.Frontal)),
                center, width, imageCube.Dimension);

                mdiControl.UpdateImage(ImageOrientation.Frontal, image);
            }

            Line line1 = new Line(0, frontal, imageCube.Dimension, frontal);
            Line line2 = new Line(sagittal, 0, sagittal, imageCube.Dimension);
            mdiControl.DrawLine(ImageOrientation.Transversal, line1, line2);

            line1 = new Line(0, pos, imageCube.Dimension, pos);
            line2 = new Line(frontal, 0, frontal, imageCube.Dimension);
            mdiControl.DrawLine(ImageOrientation.Sagittal, line1, line2);

            line1 = new Line(0, pos, imageCube.Dimension, pos);
            line2 = new Line(sagittal, 0, sagittal, imageCube.Dimension);
            mdiControl.DrawLine(ImageOrientation.Frontal, line1, line2);
        }

        /// <summary>
        /// Entfernt denIImageCube aus dem Speicher.
        /// </summary>
        /// <param name="imageID">Das aus dem Speicher zu entfernende IMDIControl.</param>
        public void CloseMDIControl(Guid imageID)
        {
            manager.RemoveImageCube(imageID);
        }

        /// <summary>
        /// Gibt die IImageCube ID, des ImageCube zurück welche aus den Daten
        /// der Datei, der mit filename übergeben wurde, erzeugt wurde.
        /// </summary>
        /// <param name="filename">Die zum IImageCube passende Datei.</param>
        /// <returns>
        /// IImageCube ID zu der dazugehörigen Datei, welche mit filename
        /// übergeben wurde oder Guid.Empty falls keine passender ID gefunden wurde
        /// </returns>
        public Guid GetImageCubeByFilename(string filename)
        {
            return manager.GetImageCubeByFilename(filename);
        }
    }
}
