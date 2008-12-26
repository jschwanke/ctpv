using System;
using System.Globalization;
using System.IO;
using Viewer.Core.Enumeration;

namespace Viewer.Core
{
    public static class CTXReader
    {
        public static ImageCube ReadFiles(string hedFile, string ctxFile)
        {
            ImageCube cube = new ImageCube {HedFile = hedFile, CtxFile = ctxFile};
            ReadHedFile(cube, hedFile);
            ReadCtxFile(cube, ctxFile);
            return cube;
        }

        private static void ReadHedFile(ImageCube imageCube, string hedFile)
        {
            StreamReader streamReader = new StreamReader(hedFile);
            try
            {
                while (streamReader.Peek() != -1)
                {
                    ExtractFileData(imageCube, streamReader.ReadLine());
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
        private static void ReadCtxFile(ImageCube imageCube, string ctxFile)
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

        private static void ExtractFileData(ImageCube imageCube, string input)
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