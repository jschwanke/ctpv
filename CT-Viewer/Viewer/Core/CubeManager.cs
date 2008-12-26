using System;
using System.Collections.Generic;

namespace Viewer.Core
{
    /// <summary>
    /// Eine Instanz dieser Klasse definiert die Methoden zur Verwaltung
    /// von IImageCube.
    /// </summary>
    public class CubeManager
    {
        private readonly Dictionary<Guid, ImageCube> imageCubes;

        public CubeManager()
        {
            imageCubes = new Dictionary<Guid, ImageCube>();
        }

        /// <summary>
        /// Durchsucht die IImageCube nach der passenden ID.
        /// </summary>
        /// <param name="imageID">ID des gesuchten IImageCube.</param>
        /// <returns>
        /// Falls ein IImageCube zum passenden Schlüssel gefunden wurde, wird
        /// das Objekt zurückgegeben, andernfalls wird NULL zurückgegeben.
        /// </returns>
        public ImageCube GetImageCube(Guid imageID)
        {
            return imageCubes[imageID];
        }

        /// <summary>
        /// Gibt eine Collection mit allen IImageCube zurück.
        /// </summary>
        /// <returns>Eine Collection mit allen ImageCubes.</returns>
        public List<ImageCube> GetAllImageCubes()
        {
            List<ImageCube> cubes = new List<ImageCube>(imageCubes.Count);
            foreach (Guid key in imageCubes.Keys)
            {
                cubes.Add(imageCubes[key]);
            }
            return cubes;
        }

        /// <summary>
        /// Entfernt ein IImageCube aus der Collectrion.
        /// </summary>
        /// <param name="imageID">Die ID des zu löscheneden IImageCubes.</param>
        public void RemoveImageCube(Guid imageID)
        {
            imageCubes.Remove(imageID);
        }

        /// <summary>
        /// Erzeugt ein neues IImageCube Objekt.
        /// </summary>
        /// <param name="hedFile">Der Dateiname der ".hed" Datei.</param>
        /// <param name="ctxFile">Der Dateiname der ".ctx" Datei.</param>
        /// <returns>
        /// Ein IImageCube Objekt mit den Daten aus der ".hed" und der ".ctx" Datei.
        /// </returns>
        public ImageCube CreateImageCube(string hedFile, string ctxFile)
        {
            ImageCube cube = CTXReader.ReadFiles(hedFile, ctxFile);
            imageCubes.Add(cube.ImageID,cube);
            return cube;
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
            Guid returnValue = Guid.Empty;
            foreach (KeyValuePair<Guid,ImageCube> entry in imageCubes)
            {
                if(entry.Value.HedFile.CompareTo(filename) == 0 ||
                   entry.Value.CtxFile.CompareTo(filename) == 0 )
                {
                    returnValue = entry.Key;
                }
            }
            return returnValue;
        }
    }
}