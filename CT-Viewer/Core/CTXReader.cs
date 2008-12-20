using System;
using System.Globalization;
using System.IO;
using InterfaceLayer.Enumeration;
using InterfaceLayer.Image;
using InterfaceLayer.IO;

namespace Core
{
    /// <summary>
    /// Eine Instanz dieser Klasse stellt die Realisierung der ICTXReader
    /// Schnittstelle da. 
    /// </summary>
    public class CTXReader : ICTXReader
    {
        private string hedFile;
        private string ctxFile;

        /// <summary>
        /// Erzeugt eine neue CTXReader Klasse.
        /// </summary>
        internal CTXReader()
        {
            
        }

        /// <summary>
        /// Liefert oder setzt den Dateipfad zur ".hed" Datei.
        /// </summary>
        string ICTXReader.HEDFile
        {
            get { return hedFile; }
        }

        /// <summary>
        /// Liefert oder setzt den Dateipfad zur ".ctx" Datei.
        /// </summary>
        string ICTXReader.CTXFile
        {
            get { return ctxFile; }
        }

        /// <summary>
        /// Liest alle Daten aus der ".hed" und der ".ctx" Datei in das
        /// IImageCube Objekt.
        /// </summary>
        /// <returns>Eine IImageCube Objekt mit allen Daten aus der ".hed" und ".ctx" Datei.</returns>
        public IImageCube ReadFiles(string hedFile, string ctxFile)
        {
            this.hedFile = hedFile;
            this.ctxFile = ctxFile;
            IImageCube cube = new ImageCube();
            cube.HedFile = hedFile;
            cube.CtxFile = ctxFile;
            ReadHedFile(ref cube);
            ReadCtxFile(ref cube);
            return cube;
        }

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
        public void ReadHedFile(ref IImageCube imageCube)
        {
            StreamReader streamReader = new StreamReader(hedFile);
            try
            {
                while (streamReader.Peek() != -1)
                {
                    ExtractFileData(ref imageCube, streamReader.ReadLine());
                }
            }
            catch (IOException e)
            {
                throw new ReaderException("An error occurred by reading the HED File", e);
            }
            finally
            {
                streamReader.Close();
            }
        }

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
        public void ReadCtxFile(ref IImageCube imageCube)
        {
            FileStream fileStream = new FileStream(ctxFile, FileMode.Open);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            try
            {
                if(imageCube.DataType == DataType.Short)
                {
                    short[,,] shortData = new short[imageCube.DimX, imageCube.DimY, imageCube.DimZ];
                    byte[] byteData = new byte[imageCube.DataSize];

                    for (int z = 0; z < imageCube.DimZ; z++)
                    {
                        for (int y = 0; y < imageCube.DimY; y++)
                        {
                            for (int x = 0; x < imageCube.DimX; x++)
                            {
                                for (int i = 0; i < imageCube.DataSize; i++ )
                                {
                                    byteData[i] = binaryReader.ReadByte();
                                }

                                if(imageCube.ByteOrder == ByteOrder.MSB)
                                {
                                    Array.Reverse(byteData);
                                }
                                shortData[x,y,z] = BitConverter.ToInt16(byteData, 0);
                            }
                        }
                    }
                    imageCube.ShortCubeData = shortData;
                }
                else
                {
                    float[,,] floatData = new float[imageCube.DimX, imageCube.DimY, imageCube.DimZ];
                    byte[] byteData = new byte[imageCube.DataSize];

                    for (int z = 0; z < imageCube.DimZ; z++)
                    {
                        for (int y = 0; y < imageCube.DimY; y++)
                        {
                            for (int x = 0; x < imageCube.DimX; x++)
                            {
                                for (int i = 0; i < imageCube.DataSize; i++)
                                {
                                    byteData[i] = binaryReader.ReadByte();
                                }

                                if (imageCube.ByteOrder == ByteOrder.MSB)
                                {
                                    Array.Reverse(byteData);
                                }
                                floatData[z, y, x] = BitConverter.ToSingle(byteData, 0);
                            }
                        }
                    }
                    imageCube.FloatCubeData = floatData;
                }
            }
            catch (IOException e)
            {
                throw new ReaderException("An error occurred by reading the HED File", e);
            }
            finally
            {
                fileStream.Close();
                binaryReader.Close();
            }
        }

        /// <summary>
        /// Extrahiert, die Daten aus dem String passend zu den Werten der ".hed" Datei.
        /// </summary>
        /// <param name="imageCube">
        /// Das IImageCube Objekt, in welches die Daten der ".hed" Datei. gespeichert werden sollen.
        /// </param>
        /// <param name="input">
        /// Die eingelesene Zeile aus der ".hed" Datei.
        /// </param>
        private static void ExtractFileData(ref IImageCube imageCube, string input)
        {
            input.Trim();
            string value = input.Substring(input.IndexOf(" ")).Trim();
            input = input.ToLower();

            if (input.StartsWith("modality"))
            {
                imageCube.Modality = value;
            }
            else if (input.StartsWith("created_by"))
            {
                imageCube.CreatedBy = value;
            }
            else if (input.StartsWith("creation_info"))
            {
                imageCube.CreationInfo = value;
            }
            else if (input.StartsWith("primary_view"))
            {
                value = value.ToLower();
                if (value == "transversal")
                {
                    imageCube.ImageOrientation = ImageOrientation.Transversal;
                }
                else if (value == "sagittal")
                {
                    imageCube.ImageOrientation = ImageOrientation.Sagittal;
                }
                else if (value == "frontal")
                {
                    imageCube.ImageOrientation = ImageOrientation.Frontal;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading primary_view.");
                }
            }
            else if (input.StartsWith("data_type"))
            {
                value = value.ToLower();
                if (value == "integer")
                {
                    imageCube.DataType = DataType.Short;
                }
                else if (value == "float")
                {
                    imageCube.DataType = DataType.Float;
                }
                else if (value == "vaxfloat")
                {
                    imageCube.DataType = DataType.Float;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading data_type.");
                }
            }
            else if (input.StartsWith("num_bytes"))
            {
                int var;
                if (Int32.TryParse(value, out var))
                {
                    imageCube.DataSize = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading num_bytes.");
                }
            }
            else if (input.StartsWith("byte_order"))
            {
                value = value.ToLower();
                if (value == "vms")
                {
                    imageCube.ByteOrder = ByteOrder.MSB;
                }
                else if (value == "aix")
                {
                    imageCube.ByteOrder = ByteOrder.MSB;
                }
                else if (value == "ultrix")
                {
                    imageCube.ByteOrder = ByteOrder.LSB;
                }
                else if (value == "decunix")
                {
                    imageCube.ByteOrder = ByteOrder.LSB;
                }
                else if (value == "linux")
                {
                    imageCube.ByteOrder = ByteOrder.LSB;
                }
                else if (value == "msdos")
                {
                    imageCube.ByteOrder = ByteOrder.LSB;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading byte_order.");
                }
            }
            else if (input.StartsWith("slice_dimension"))
            {
                int var;
                if (Int32.TryParse(value, out var))
                {
                    imageCube.Dimension = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading slice_dimension.");
                }
            }
            else if (input.StartsWith("pixel_size"))
            {
                float var;
                if (Single.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-us"), out var))
                {
                    imageCube.PixelSize = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading pixel size.");
                }
            }
            else if (input.StartsWith("slice_distance"))
            {
                float var;
                if (Single.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-us"), out var))
                {
                    imageCube.SliceDistance = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading slice distance.");
                }
            }
            else if (input.StartsWith("xoffset"))
            {
                int var;
                if (Int32.TryParse(value, out var))
                {
                    imageCube.OffsetX = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading xoffset.");
                }
            }
            else if (input.StartsWith("yoffset"))
            {
                int var;
                if (Int32.TryParse(value, out var))
                {
                    imageCube.OffsetY = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading yoffset.");
                }
            }
            else if (input.StartsWith("dimx"))
            {
                int var;
                if (Int32.TryParse(value, out var))
                {
                    imageCube.DimX = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading dimx.");
                }
            }
            else if (input.StartsWith("dimy"))
            {
                int var;
                if (Int32.TryParse(value, out var))
                {
                    imageCube.DimY = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading dimy.");
                }
            }
            else if (input.StartsWith("dimz"))
            {
                int var;
                if (Int32.TryParse(value, out var))
                {
                    imageCube.DimZ = var;
                }
                else
                {
                    throw new ReaderException("Error occurred by reading dimz.");
                }
            }
        }
    }
}
