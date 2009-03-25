using System;


namespace Viewer.Core
{
    /// <summary>
    /// Eine Instanz dieser Klasse stellt die Grundlegenden Methoden zur Verfügung
    /// eine Bildserie bereitzustellen.
    /// </summary>
    public class ImageCube
    {
        private readonly Guid imageID;
        public EByteOrder ByteOrder { get; set; }
        public EImageOrientation ImageOrientation { get; set; }
        public int DataSize { get; set; }
        public int Dimension { get; set; }
        public int DimX { get; set; }
        public int DimY { get; set; }
        public int DimZ { get; set; }
        public string HedFile { get; set; }
        public string CtxFile { get; set; }
        public float PixelSize { get; set; }
        public float SliceDistance { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public short[, ,] ShortCubeData { get; set; }

        public enum EByteOrder
        {
            LSB,MSB
        }

        public enum EImageOrientation
        {
            Transversal,Frontal,Sagittal
        }

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
        /// Berechnet aus den gespeicherten Daten Bilder entsprechend der 
        /// uebergebenen Orientierung. Methode geht davon aus, dass die Bilder in 
        /// transversaler Orientierung ohne Gantry-Verkippung akquiriert und
        /// gespeichert wurden. 
        /// </summary>
        /// <param name="orientation">Gewuenschte Orientierung der Bilder</param>
        /// <param name="sliceIndex">Index der gewuenschten Schicht (0-basiert)</param>
        /// <returns>Bild als zweidimensionales Array</returns>
        /// Bei der Übergabe eines fehlerhaften Wertes wird eine ImageCubeException
        /// ausgelöst.
        /// </exception>
        public short[,] GetSlice(EImageOrientation orientation, int sliceIndex)
        {
            short[,] result = null;
		    int i, j;
		    int zIndex = 0;
		    float curPos = 0;
		    float curPixel = 0;
		    float bottomLine = GetBottomLineInMM();
		    float topLine = GetTopLineInMM();

	        zIndex = DimZ - 1;
	        switch (orientation)
	        {
	            case EImageOrientation.Transversal:
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

	            case EImageOrientation.Frontal:
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

	            case EImageOrientation.Sagittal:
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
