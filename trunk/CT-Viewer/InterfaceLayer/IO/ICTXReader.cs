using InterfaceLayer.Image;

namespace InterfaceLayer.IO
{
    /// <summary>
    /// ICTXReader definiert Methoden um ".hed" und ".ctx" Dateien zu lesen.
    /// </summary>
    public interface ICTXReader
    {
        /// <summary>
        /// Liefert oder setzt den Dateipfad zur ".hed" Datei.
        /// </summary>
        string HEDFile
        { 
            get; 
        }

        /// <summary>
        /// Liefert oder setzt den Dateipfad zur ".ctx" Datei.
        /// </summary>
        string CTXFile
        {
            get;
        }

        /// <summary>
        /// Liest alle Daten aus der ".hed" und der ".ctx" Datei in das
        /// IImageCube Objekt.
        /// </summary>
        /// <returns>Eine IImageCube Objekt mit allen Daten aus der ".hed" und ".ctx" Datei.</returns>
        IImageCube ReadFiles(string hedFile, string ctxFile);

        /// <summary>
        /// Liest die Daten aus der ".hed" Datei das übergebende IImageCube Objekt.
        /// </summary>
        /// <param name="imageCube">
        /// Das Objekt in welche die Daten geschrieben werden sollen.
        /// </param>
        /// <exception cref="ReaderException">
        /// Bei Fehlern beim Lesen der Daten oder beim Schreiben der Daten in das Objekt
        /// wird eine ReaderException ausgelöst.
        /// </exception>
        void ReadHedFile(ref IImageCube imageCube);

        /// <summary>
        /// Liest die Pixelinformationen einer Bildserie in das Object cube und gibt 
        /// es als Wert der Methode wieder zurueck. Vor diesem Aufruf muss Cube mit 
        /// den Informationen aus dem entsprechenden ".hed" File gefuellt worden sein 
        /// (durch Aufruf von readHedFile).
        /// </summary>
        /// <param name="imageCube">
        /// Das Objekt in welche die Daten geschrieben werden sollen.
        /// </param>
        /// <exception cref="ReaderException">
        /// Bei Fehlern beim Lesen der Daten oder beim Schreiben der Daten in das Objekt
        /// wird eine ReaderException ausgelöst.
        /// </exception>
        void ReadCtxFile(ref IImageCube imageCube);
    }
}
