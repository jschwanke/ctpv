using System;
using System.Collections.Generic;
using InterfaceLayer;
using InterfaceLayer.Image;
using InterfaceLayer.IO;

namespace Core
{
    /// <summary>
    /// Eine Instanz dieser Klasse definiert die Methoden zur Verwaltung
    /// von IImageCube.
    /// </summary>
    public class CubeManager : ICubeManager
    {
        private readonly Dictionary<Guid, IImageCube> imageCubes;
        private readonly ICTXReader reader;
        private bool dispose = false;

        public CubeManager()
        {
            imageCubes = new Dictionary<Guid, IImageCube>();
            reader = new CTXReader();
        }

        ~CubeManager()
        {
            Dispose();
        }

        /// <summary>
        /// Durchsucht die IImageCube nach der passenden ID.
        /// </summary>
        /// <param name="imageID">ID des gesuchten IImageCube.</param>
        /// <returns>
        /// Falls ein IImageCube zum passenden Schl�ssel gefunden wurde, wird
        /// das Objekt zur�ckgegeben, andernfalls wird NULL zur�ckgegeben.
        /// </returns>
        public IImageCube GetImageCube(Guid imageID)
        {
            return imageCubes[imageID];
        }

        /// <summary>
        /// Gibt eine Collection mit allen IImageCube zur�ck.
        /// </summary>
        /// <returns>Eine Collection mit allen ImageCubes.</returns>
        public List<IImageCube> GetAllImageCubes()
        {
            List<IImageCube> cubes = new List<IImageCube>(imageCubes.Count);
            foreach (Guid key in imageCubes.Keys)
            {
                cubes.Add(imageCubes[key]);
            }
            return cubes;
        }

        /// <summary>
        /// Entfernt ein IImageCube aus der Collectrion.
        /// </summary>
        /// <param name="imageID">Die ID des zu l�scheneden IImageCubes.</param>
        public void RemoveImageCube(Guid imageID)
        {
            IImageCube cube = imageCubes[imageID];
            imageCubes.Remove(imageID);
            cube.Dispose();
        }

        /// <summary>
        /// Erzeugt ein neues IImageCube Objekt.
        /// </summary>
        /// <param name="hedFile">Der Dateiname der ".hed" Datei.</param>
        /// <param name="ctxFile">Der Dateiname der ".ctx" Datei.</param>
        /// <returns>
        /// Ein IImageCube Objekt mit den Daten aus der ".hed" und der ".ctx" Datei.
        /// </returns>
        public IImageCube CreateImageCube(string hedFile, string ctxFile)
        {
            IImageCube cube = reader.ReadFiles(hedFile, ctxFile);
            imageCubes.Add(cube.ImageID,cube);
            return cube;
        }

        /// <summary>
        /// Gibt die IImageCube ID, des ImageCube zur�ck welche aus den Daten
        /// der Datei, der mit filename �bergeben wurde, erzeugt wurde.
        /// </summary>
        /// <param name="filename">Die zum IImageCube passende Datei.</param>
        /// <returns>
        /// IImageCube ID zu der dazugeh�rigen Datei, welche mit filename
        /// �bergeben wurde oder Guid.Empty falls keine passender ID gefunden wurde
        /// </returns>
        public Guid GetImageCubeByFilename(string filename)
        {
            Guid returnValue = Guid.Empty;
            foreach (KeyValuePair<Guid,IImageCube> entry in imageCubes)
            {
                if(entry.Value.HedFile.CompareTo(filename) == 0 ||
                    entry.Value.CtxFile.CompareTo(filename) == 0 )
                {
                    returnValue = entry.Key;
                }
            }
            return returnValue;
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            if(!dispose)
            {
                imageCubes.Clear();
                dispose = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
