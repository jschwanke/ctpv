using System;
using Viewer.Core.Enumeration;


namespace Viewer.Core
{
    /// <summary>
    /// Eine Instanz dieser Klasse stellt die Grundlegenden Methoden zur Verfügung
    /// eine Bildserie bereitzustellen.
    /// </summary>
    public class ImageCube
    {
        private readonly Guid imageID;

        /// <summary>
        /// Erzeugt ein neues ImageCube Objekt mit einer einmaligen ID.
        /// </summary>
        internal ImageCube()
        {
            imageID = Guid.NewGuid();
        }

        /// <summary>
        /// Liefert die einalige ID der Bildserie.
        /// </summary>
        public Guid ImageID
        {
            get { return imageID; }
        }

        /// <summary>
        /// Liefert oder setzt die ByteOrder unter welcher das Bild
        /// abgespeichert wurde.
        /// </summary>
        public ByteOrder ByteOrder { get; set; }

        /// <summary>
        /// Liefert oder setzt den Datentyp der gespeicherten Voxelwerte Short oder Float.
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// Liefert oder setzt die Orientierung des Bildes: transversal, sagittal, frontal.
        /// </summary>
        public ImageOrientation ImageOrientation { get; set; }
        
        /// <summary>
        /// Liefert oder setzt den Kommentar, der bei der Erzeugung eingegeben wurde.
        /// </summary>
        public string CreationInfo { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen des Geraets, mit dem die Bilder 
        /// erzeugt wurden.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Liefert oder setzt die Groesse eines Voxels in der Datei in Bytes.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int DataSize { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Pixel in x,y Richtung, naechst groesste 
        /// Zweierpotenz aus dimX und dimY.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int Dimension { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in x-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int DimX { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in y-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int DimY { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in z-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int DimZ { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen der Datei, aus der die Bilddaten gelesen wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public string HedFile { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen der Datei, aus der die Bilddaten gelesen wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public string CtxFile { get; set; }

        /// <summary>
        /// Liefert oder setzt die Modalität z.B. CT, MR, PET ...
        /// </summary>
        public string Modality { get; set; }

        /// <summary>
        ///  Liefert oder setzt die Pixelgroesse in mm.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public float PixelSize { get; set; }

        /// <summary>
        /// Liefert oder setzt den Schichtabstand (Dicke) der Schichtbilder
        /// (fuer multi-planare Rekonstruktionen)
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public float SliceDistance { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Spalten (von x=0 kommend), 
        /// die nicht abgespeichert wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int OffsetX { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Zeilen (von y=0 kommend), 
        /// die nicht abgespeichert wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int OffsetY { get; set; }

        /// <summary>
        /// Liefert oder setzt die Pixeldaten (short) des Bildes.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public short[, ,] ShortCubeData { get; set; }

        /// <summary>
        /// Liefert oder setzt die Pixeldaten (float) des Bildes.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public float[, ,] FloatCubeData { get; set; }

        /// <summary>
        /// Liefert den unteren Rand (in mm) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der untere Rand (in mm)</returns>
        public float GetBottomLineInMM()
        {
            float img_height_mm = Dimension * PixelSize;
            float cube_height_mm = DimZ * SliceDistance;
            float start_mm = (img_height_mm + cube_height_mm) / 2.0f;
            return start_mm;
        }

        /// <summary>
        /// Liefert den unteren Rand (in pixel) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der untere Rand (in pixel)</returns>
        public int GetBottomLineInPixel()
        {
            return ((int)(GetBottomLineInMM() / PixelSize));
        }

        /// <summary>
        /// Liefert den oberen Rand (in mm) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der oberen Rand (in mm)</returns>
        public float GetTopLineInMM()
        {
            float img_height_mm = Dimension * PixelSize;
            float cube_height_mm = DimZ * SliceDistance;
            float offset_mm = (img_height_mm - cube_height_mm) / 2.0f;
            return offset_mm;
        }

        /// <summary>
        /// Liefert den oberen Rand (in pixel) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der oberen Rand (in pixel)</returns>
        public int GetTopLineInPixel()
        {
            return ((int)(GetTopLineInMM() / PixelSize));
        }

        /// <summary>
        /// Prueft die Verfuegbarkeit der Pixeldaten. Liefert true, wenn die 
        /// Pixel(Voxel)daten verfuegbar sind, sonst false.
        /// </summary>
        /// <returns>True, wenn die Pixel(Voxel)daten verfuegbar sind, sonst false</returns>
        public bool IsPixelDataAvailable()
        {
            bool result = false;
            if (DataType == DataType.Short)
            {
                if (ShortCubeData != null)
                {
                    result = true;
                }
            }
            else if (DataType == DataType.Float)
            {
                if (FloatCubeData != null)
                {
                    result = true;
                }
            }
            return result;
        }

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
        public short[,] GetSlice(ImageOrientation orientation, int sliceIndex)
        {
            short[,] result = null;
		    int i, j;
		    int zIndex = 0;
		    float curPos = 0;
		    float curPixel = 0;
		    float bottomLine = GetBottomLineInMM();
		    float topLine = GetTopLineInMM();

		    if (IsPixelDataAvailable())
		    {
		        zIndex = DimZ - 1;
		        switch (orientation)
		        {
		            case ImageOrientation.Transversal:
		                if (sliceIndex >= 0 && sliceIndex < DimZ)
		                {
		                    result = new short[DimY, DimX];
		                    for (int y = 0; y < DimY; y++)
		                    {
		                        for (int x = 0; x < DimX; x++)
		                        {
		                            result[y, x] = ShortCubeData[x,y,sliceIndex];
		                        }
		                    }
		                }
		                break;

		            case ImageOrientation.Frontal:
		                if (sliceIndex >= 0 && sliceIndex < Dimension)
		                {
		                    result = new short[Dimension, Dimension];
		                    curPos = DimZ * SliceDistance;
		                    for (i = 0; i < Dimension; i++)
		                    {
		                        curPixel = (Dimension - i) * PixelSize;
		                        if ((curPixel >= topLine) && (curPixel <= bottomLine))
		                        {
		                            if (curPos < zIndex*SliceDistance)
		                            {
		                                zIndex--;
		                            }

		                            if ((zIndex >= 0) && (zIndex < DimZ)
		                                && (sliceIndex >= OffsetY)
		                                && (sliceIndex < (OffsetY + DimY)))
		                            {
		                                for (int x = 0; x < DimX; x++)
		                                {
		                                    result[i, x] = ShortCubeData[x, sliceIndex, zIndex];
		                                }
		                            }
		                            curPos = curPos - PixelSize;
		                        }
		                    }
		                }
		                break;

		            case ImageOrientation.Sagittal:
		                result = new short[Dimension, Dimension];
		                if (sliceIndex >= 0 && sliceIndex < Dimension)
		                {
		                    curPos = DimZ * SliceDistance;
		                    for (i = 0; i < Dimension; i++)
		                    {
		                        curPixel = (Dimension - i) * PixelSize;
		                        if ((curPixel >= topLine) && (curPixel <= bottomLine))
		                        {
		                            if (curPos < zIndex * SliceDistance)
		                            {
		                                zIndex--;
		                            }
                                
		                            if ((zIndex >= 0) && (zIndex < DimZ)
		                                && (sliceIndex >= OffsetY)
		                                && (sliceIndex < (OffsetY + DimY)))
		                            {
		                                for (j = 0; j < Dimension; j++)
		                                {
		                                    result[i, j] = ShortCubeData[sliceIndex, j, zIndex];
		                                }
		                            }
		                            curPos = curPos - PixelSize;
		                        }
		                    }
		                }
		                break;
		        }
		    }
            return result;
        }

        /// <summary>
        /// Transformiert den y-Index eines Pixels in einer sagittalen oder frontalen 
        /// Rekonstruktion in einen Schichtindex in der Bildserie.
        /// </summary>
        /// <param name="y">y-Index</param>
        /// <returns>Schichtindex (0-basiert)</returns>
        public int TrafoScreenPosToSlice(int y)
        {
            int top = GetTopLineInPixel();
            int bottom = GetBottomLineInPixel();
            int slice;

            if (y <= top)
            {
                slice = DimZ - 1;
            }
            else if (y >= bottom)
            {
                slice = 0;
            }
            else
            {
                int dist_in_pix = y - top;
                float dist_in_mm = dist_in_pix * PixelSize;
                slice = (int)(DimZ - (dist_in_mm / SliceDistance));
            }
            return slice;
        }

        /// <summary>
        /// Berechnet die Position (in Pixel) einer uebergebenen Schicht in einer 
        /// multi-planaren Rekonstruktion.
        /// </summary>
        /// <param name="slice">slice</param>
        /// <returns>Position</returns>
        public int TrafoSliceToScreenPos(int slice)
        {
            float top = GetTopLineInMM();
            int dist_in_slices = DimZ - slice;
            float dist_in_mm = (dist_in_slices - 0.5f) * SliceDistance;
            float offset = (top + dist_in_mm) / PixelSize;
            return ((int)(offset));
        }
    }
}
