using InterfaceLayer.Image;

namespace InterfaceLayer.IO
{
    /// <summary>
    /// ICTXReader definiert Methoden um ".hed" und ".ctx" Dateien zu lesen.
    /// </summary>
    public interface ICTXReader
    {
        /// <summary>
        /// Liest alle Daten aus der ".hed" und der ".ctx" Datei in das
        /// IImageCube Objekt.
        /// </summary>
        /// <exception cref="ReaderException">
        /// Tirtt auf falls es bei dem Auslesen der Daten zu einem Fehler kommt.
        /// </exception>
        /// <returns>Eine IImageCube Objekt mit allen Daten aus der ".hed" und ".ctx" Datei.</returns>
        IImageCube ReadFiles(string hedFile, string ctxFile);
    }
}
