using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Viewer.Core;
using Viewer.Core.Enumeration;
using Viewer.Properties;
using Viewer.View.MDI;

namespace Viewer.Controller
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

        public void CreateImageCubes(string[] filenames)
        {
            foreach (string filename in filenames)
            {
                CreateImageCube(filename);
            }
        }

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

                ImageCube imageCube = manager.CreateImageCube(hedFile, ctxFile);
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

        public void CreateImageCubesFromDirectory(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists) return;
            foreach (FileInfo file in directory.GetFiles("*.hed"))
            {
                CreateImageCube(file.FullName);
            }
        }

        public void CloseApplication()
        {
            Application.Exit();
        }

        public void UpdateImages(MDIControl mdiControl)
        {
            ImageCube imageCube = manager.GetImageCube(mdiControl.ImageID);
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

        public void CloseMDIControl(Guid imageID)
        {
            manager.RemoveImageCube(imageID);
        }

        public Guid GetImageCubeByFilename(string filename)
        {
            return manager.GetImageCubeByFilename(filename);
        }
    }
}
