using System;
using InterfaceLayer.Enumeration;
using InterfaceLayer.Image;

namespace Core
{
    /// <summary>
    /// Eine Instanz dieser Klasse stellt die Realisierung der IImageCube
    /// Schnittstelle da.
    /// </summary>
    public class ImageCube : IImageCube
    {
        private readonly Guid imageID;
        private ByteOrder byteOrder;
        private DataType dataType;
        private ImageOrientation imageOrientation;
        private string creationInfo;
        private string createdBy;
        private int dataSize;
        private int dimension;
        private int dimX;
        private int dimY;
        private int dimZ;
        private string hedFile;
        private string ctxFile;
        private string modality;
        private float pixelSize;
        private float sliceDistance;
        private int offsetX;
        private int offsetY;
        private short[, ,] shortCubeData;
        private float[, ,] floatCubeData;
        private bool dispose = false;

        /// <summary>
        /// Erzeugt ein neues ImageCube Objekt mit einer einmaligen ID.
        /// </summary>
        internal ImageCube()
        {
            imageID = Guid.NewGuid();
        }

        ~ImageCube()
        {
            Dispose();
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
        public ByteOrder ByteOrder
        {
            get { return byteOrder; }
            set { byteOrder = value; }
        }

        /// <summary>
        /// Liefert oder setzt den Datentyp der gespeicherten Voxelwerte Short oder Float.
        /// </summary>
        public DataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Orientierung des Bildes: transversal, sagittal, frontal.
        /// </summary>
        public ImageOrientation ImageOrientation
        {
            get { return imageOrientation; }
            set { imageOrientation = value; }
        }

        /// <summary>
        /// Liefert oder setzt den Kommentar, der bei der Erzeugung eingegeben wurde.
        /// </summary>
        public string CreationInfo
        {
            get { return creationInfo; }
            set { creationInfo = value; }
        }

        /// <summary>
        /// Liefert oder setzt den Namen des Geraets, mit dem die Bilder 
        /// erzeugt wurden.
        /// </summary>
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Groesse eines Voxels in der Datei in Bytes.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int DataSize
        {
            get { return dataSize; }
            set { dataSize = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Pixel in x,y Richtung, naechst groesste 
        /// Zweierpotenz aus dimX und dimY.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int Dimension
        {
            get { return dimension; }
            set { dimension = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in x-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int DimX
        {
            get { return dimX; }
            set { dimX = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in y-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int DimY
        {
            get { return dimY; }
            set { dimY = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gespeicherten Pixel in z-Richtung.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int DimZ
        {
            get { return dimZ; }
            set { dimZ = value; }
        }

        /// <summary>
        /// Liefert oder setzt den Namen der Datei, aus der die Bilddaten gelesen wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public string HedFile
        {
            get { return hedFile; }
            set { hedFile = value; }
        }

        /// <summary>
        /// Liefert oder setzt den Namen der Datei, aus der die Bilddaten gelesen wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public string CtxFile
        {
            get { return ctxFile; }
            set { ctxFile = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Modalität z.B. CT, MR, PET ...
        /// </summary>
        public string Modality
        {
            get { return modality; }
            set { modality = value; }
        }

        /// <summary>
        ///  Liefert oder setzt die Pixelgroesse in mm.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public float PixelSize
        {
            get { return pixelSize; }
            set { pixelSize = value; }
        }

        /// <summary>
        /// Liefert oder setzt den Schichtabstand (Dicke) der Schichtbilder
        /// (fuer multi-planare Rekonstruktionen)
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public float SliceDistance
        {
            get { return sliceDistance; }
            set { sliceDistance = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Spalten (von x=0 kommend), 
        /// die nicht abgespeichert wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int OffsetX
        {
            get { return offsetX; }
            set { offsetX = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Zeilen (von y=0 kommend), 
        /// die nicht abgespeichert wurden.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public int OffsetY
        {
            get { return offsetY; }
            set { offsetY = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Pixeldaten (short) des Bildes.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public short[, ,] ShortCubeData
        {
            get { return shortCubeData; }
            set { shortCubeData = value; }
        }

        /// <summary>
        /// Liefert oder setzt die Pixeldaten (float) des Bildes.
        /// </summary>
        /// <exception cref="ImageCubeException">
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public float[, ,] FloatCubeData
        {
            get { return floatCubeData; }
            set { floatCubeData = value; }
        }

        /// <summary>
        /// Liefert den unteren Rand (in mm) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der untere Rand (in mm)</returns>
        public float GetBottomLineInMM()
        {
            float img_height_mm = dimension * pixelSize;
            float cube_height_mm = dimZ * sliceDistance;
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
            return ((int)(GetBottomLineInMM() / pixelSize));
        }

        /// <summary>
        /// Liefert den oberen Rand (in mm) der Bildinformation in einer sagittalen 
        /// oder frontalen Rekonstruktion, bezogen auf das ungezoomte 2D Bild.
        /// </summary>
        /// <returns>Der oberen Rand (in mm)</returns>
        public float GetTopLineInMM()
        {
            float img_height_mm = dimension * pixelSize;
            float cube_height_mm = dimZ * sliceDistance;
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
            return ((int)(GetTopLineInMM() / pixelSize));
        }

        /// <summary>
        /// Prueft die Verfuegbarkeit der Pixeldaten. Liefert true, wenn die 
        /// Pixel(Voxel)daten verfuegbar sind, sonst false.
        /// </summary>
        /// <returns>True, wenn die Pixel(Voxel)daten verfuegbar sind, sonst false</returns>
        public bool IsPixelDataAvailable()
        {
            bool result = false;
            if (DataType == InterfaceLayer.Enumeration.DataType.Short)
            {
                if (shortCubeData != null)
                {
                    result = true;
                }
            }
            else if (DataType == InterfaceLayer.Enumeration.DataType.Float)
            {
                if (floatCubeData != null)
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
		        zIndex = dimZ - 1;
                if (orientation == ImageOrientation.Transversal)
                {
                    if (sliceIndex >= 0 && sliceIndex < dimZ)
                    {
                        result = new short[dimY, dimX];
                        for (int y = 0; y < dimY; y++)
                        {
                            for (int x = 0; x < dimX; x++)
                            {
                                result[y, x] = shortCubeData[x,y,sliceIndex];
                            }
                        }
                    }
                }
                else if (orientation == ImageOrientation.Frontal)
                {
                    if (sliceIndex >= 0 && sliceIndex < dimension)
                    {
                        result = new short[dimension, dimension];
                        curPos = dimZ * sliceDistance;
                        for (i = 0; i < dimension; i++)
                        {
                            curPixel = (dimension - i) * pixelSize;
                            if ((curPixel >= topLine) && (curPixel <= bottomLine))
                            {
                                if (curPos < zIndex*sliceDistance)
                                {
                                    zIndex--;
                                }

                                if ((zIndex >= 0) && (zIndex < dimZ)
                                    && (sliceIndex >= OffsetY)
                                    && (sliceIndex < (OffsetY + dimY)))
                                {
                                    for (int x = 0; x < dimX; x++)
                                    {
                                        result[i, x] = shortCubeData[x, sliceIndex, zIndex];
                                    }
                                }
                                curPos = curPos - pixelSize;
                            }
                        }
                    }
                }
                else if (orientation == ImageOrientation.Sagittal)
                {
                    result = new short[dimension, dimension];
                    if (sliceIndex >= 0 && sliceIndex < dimension)
                    {
                        curPos = dimZ * sliceDistance;
                        for (i = 0; i < dimension; i++)
                        {
                            curPixel = (dimension - i) * pixelSize;
                            if ((curPixel >= topLine) && (curPixel <= bottomLine))
                            {
                                if (curPos < zIndex * sliceDistance)
                                {
                                    zIndex--;
                                }
                                
                                if ((zIndex >= 0) && (zIndex < dimZ)
                                    && (sliceIndex >= OffsetY)
                                    && (sliceIndex < (OffsetY + dimY)))
                                {
                                    for (j = 0; j < dimension; j++)
                                    {
                                        result[i, j] = shortCubeData[sliceIndex, j, zIndex];
                                    }
                                }
                                curPos = curPos - pixelSize;
                            }
                        }
                    }
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
            int slice = 0;

            if (y <= top)
            {
                slice = dimZ - 1;
            }
            else if (y >= bottom)
            {
                slice = 0;
            }
            else
            {
                int dist_in_pix = y - top;
                float dist_in_mm = dist_in_pix * pixelSize;
                slice = (int)(dimZ - (dist_in_mm / sliceDistance));
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
            int dist_in_slices = dimZ - slice;
            float dist_in_mm = (dist_in_slices - 0.5f) * sliceDistance;
            float offset = (top + dist_in_mm) / pixelSize;
            return ((int)(offset));
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            if(!dispose)
            {
                dispose = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
