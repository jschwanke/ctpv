using System;
using InterfaceLayer.Enumeration;

namespace InterfaceLayer.Image
{
    /// <summary>
    /// Dieses Interface stellt die Grundlegenden Methoden zur Verfügung
    /// eine Bildserie bereitzustellen.
    /// </summary>
    /// <remarks>
    ///Im Prinzip dient eine Instanz dazu, Daten zu verwalten, die durch eine 
    /// ".hed"/".ctx" Dateikombination definiert werden. Deshalb orientieren sich die 
    /// Datenfelder an den Spezifikationen dieser Dateien. Die notwendigen
    /// Informationen koennen aber auch aus anderen Quellen bereitgestellt werden.
    /// </remarks>
    public unsafe interface IImageCube : IDisposable
    {
        /// <summary>
        /// Liefert die einalige ID der Bildserie.
        /// </summary>
        Guid ImageID
        {
            get;
        }

        /// <summary>
        /// Liefert oder setzt die ByteOrder unter welcher das Bild
        /// abgespeichert wurde.
        /// </summary>
        ByteOrder ByteOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Orientierung des Bildes: transversal, sagittal, frontal.
        /// </summary>
        ImageOrientation ImageOrientation
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Groesse eines Voxels in der Datei in Bytes.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        int DataSize
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Pixel in x,y Richtung, naechst groesste 
        /// Zweierpotenz aus dimX und dimY.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        int Dimension
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in x-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        int DimX
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in y-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        int DimY
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in z-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        int DimZ
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt den Namen der Datei, aus der die Bilddaten gelesen wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        string HedFile
        {
            get;
        }

        /// <summary>
        /// Liefert oder setzt den Namen der Datei, aus der die Bilddaten gelesen wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        string CtxFile
        {
            get;
        }

        /// <summary>
        ///  Liefert oder setzt die Pixelgroesse in mm.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        float PixelSize
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt den Schichtabstand (Dicke) der Schichtbilder
        /// (fuer multi-planare Rekonstruktionen)
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        float SliceDistance
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Spalten (von x=0 kommend), 
        /// die nicht abgespeichert wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        int OffsetX
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Zeilen (von y=0 kommend), 
        /// die nicht abgespeichert wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        int OffsetY
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert oder setzt die Pixeldaten (short) des Bildes.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        short*** CubeData
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert den unteren Rand (in mm) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der untere Rand (in mm)</returns>
        float GetBottomLineInMM();

        /// <summary>
        /// Liefert den unteren Rand (in pixel) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der untere Rand (in pixel)</returns>
        int GetBottomLineInPixel();

        /// <summary>
        /// Liefert den oberen Rand (in mm) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der oberen Rand (in mm)</returns>
        float GetTopLineInMM();

        /// <summary>
        /// Liefert den oberen Rand (in pixel) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der oberen Rand (in pixel)</returns>
        int GetTopLineInPixel();

        /// <summary>
        /// Prueft die Verfuegbarkeit der Pixeldaten. Liefert true, wenn die 
        /// Pixel(Voxel)daten verfuegbar sind, sonst false.
        /// </summary>
        /// <returns>True, wenn die Pixel(Voxel)daten verfuegbar sind, sonst false</returns>
        bool IsPixelDataAvailable();

        /// <summary>
        /// Berechnet aus den gespeicherten Daten Bilder entsprechend der 
        /// uebergebenen Orientierung. Methode geht davon aus, dass die Bilder in 
        /// transversaler Orientierung ohne Gantry-Verkippung akquiriert und
        /// gespeichert wurden. 
        /// </summary>
        /// <param name="orientation">Gewuenschte Orientierung der Bilder</param>
        /// <param name="sliceIndex">Index der gewuenschten Schicht (0-basiert)</param>
        /// <returns>Bild als zweidimensionales Array</returns>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        short** GetSlice(ImageOrientation orientation, int sliceIndex);

        /// <summary>
        /// Transformiert den y-Index eines Pixels in einer sagittalen oder frontalen 
        /// Rekonstruktion in einen Schichtindex in der Bildserie.
        /// </summary>
        /// <param name="y">y-Index</param>
        /// <returns>Schichtindex (0-basiert)</returns>
        int TrafoScreenPosToSlice(int y);

        /// <summary>
        /// Berechnet die Position (in Pixel) einer uebergebenen Schicht in einer 
        /// multi-planaren Rekonstruktion.
        /// </summary>
        /// <param name="slice">slice</param>
        /// <returns>Position</returns>
        int TrafoSliceToScreenPos(int slice);
    }
}
