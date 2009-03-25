using System;
using System.Collections.Generic;
using InterfaceLayer.Image;

namespace InterfaceLayer
{
    /// <summary>
    /// Das Interface definiert die Methoden zur Verwaltung
    /// von IImageCube.
    /// </summary>
    public interface ICubeManager : IDisposable
    {
        /// <summary>
        /// Durchsucht die IImageCube nach der passenden ID.
        /// </summary>
        /// <param name="imageID">ID des gesuchten IImageCube.</param>
        /// <returns>
        /// Falls ein IImageCube zum passenden Schlüssel gefunden wurde, wird
        /// das Objekt zurückgegeben, andernfalls wird NULL zurückgegeben.
        /// </returns>
        IImageCube GetImageCube(Guid imageID);

        /// <summary>
        /// Gibt eine Collection mit allen IImageCube zurück.
        /// </summary>
        /// <returns>Eine Collection mit allen ImageCubes.</returns>
        List<IImageCube> GetAllImageCubes();

        /// <summary>
        /// Entfernt ein IImageCube aus der Collectrion.
        /// </summary>
        /// <param name="imageID">Die ID des zu löscheneden IImageCubes.</param>
        void RemoveImageCube(Guid imageID);

        /// <summary>
        /// Erzeugt ein neues IImageCube Objekt.
        /// </summary>
        /// <param name="hedFile">Der Dateiname der ".hed" Datei.</param>
        /// <param name="ctxFile">Der Dateiname der ".ctx" Datei.</param>
        /// <returns>
        /// Ein IImageCube Objekt mit den Daten aus der ".hed" und der ".ctx" Datei.
        /// </returns>
        IImageCube CreateImageCube(string hedFile, string ctxFile);

        /// <summary>
        /// Gibt die IImageCube ID, des ImageCube zurück welche aus den Daten
        /// der Datei, der mit filename übergeben wurde, erzeugt wurde.
        /// </summary>
        /// <param name="filename">Die zum IImageCube passende Datei.</param>
        /// <returns>
        /// IImageCube ID zu der dazugehörigen Datei, welche mit filename
        /// übergeben wurde oder NULL falls keine passender ID gefunden wurde
        /// </returns>
        Guid GetImageCubeByFilename(string filename);
    }
}
