using System;
using System.Globalization;
using System.IO;

namespace Viewer.Core
{
    public static class Reader
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
            finally
            {
                streamReader.Close();
            }
        }
        private static void ReadCtxFile(ImageCube imageCube, string ctxFile)
        {
            BinaryReader binaryReader = new BinaryReader(new FileStream(ctxFile, FileMode.Open));
            try
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

                            if(imageCube.ByteOrder == ImageCube.EByteOrder.MSB)
                            {
                                Array.Reverse(byteData);
                            }
                            shortData[x,y,z] = BitConverter.ToInt16(byteData, 0);
                        }
                    }
                    imageCube.ShortCubeData = shortData;
                }
            }
            finally
            {
                binaryReader.Close();
            }
        }

        private static void ExtractFileData(ImageCube imageCube, string input)
        {
            input.Trim();
            string value = input.Substring(input.IndexOf(" ")).Trim();
            input = input.ToLower();

            if (input.StartsWith("primary_view"))
            {
                switch (value.ToLower())
                {
                    case "transversal":
                        imageCube.ImageOrientation = ImageCube.EImageOrientation.Transversal;
                        break;
                    case "sagittal":
                        imageCube.ImageOrientation = ImageCube.EImageOrientation.Sagittal;
                        break;
                    case "frontal":
                        imageCube.ImageOrientation = ImageCube.EImageOrientation.Frontal;
                        break;
                }
            }
            else if (input.StartsWith("num_bytes"))
            {
                int var;
                if (!Int32.TryParse(value, out var)) return;
                imageCube.DataSize = var;
            }
            else if (input.StartsWith("byte_order"))
            {
                switch (value.ToLower())
                {
                    case "vms":
                        imageCube.ByteOrder = ImageCube.EByteOrder.MSB;
                        break;
                    case "aix":
                        imageCube.ByteOrder = ImageCube.EByteOrder.MSB;
                        break;
                    case "ultrix":
                        imageCube.ByteOrder = ImageCube.EByteOrder.LSB;
                        break;
                    case "decunix":
                        imageCube.ByteOrder = ImageCube.EByteOrder.LSB;
                        break;
                    case "linux":
                        imageCube.ByteOrder = ImageCube.EByteOrder.LSB;
                        break;
                    case "msdos":
                        imageCube.ByteOrder = ImageCube.EByteOrder.LSB;
                        break;
                }
            }
            else if (input.StartsWith("slice_dimension"))
            {
                int var;
                if (!Int32.TryParse(value, out var)) return;
                imageCube.Dimension = var;
            }
            else if (input.StartsWith("pixel_size"))
            {
                float var;
                if (!Single.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-us"), out var)) return;
                imageCube.PixelSize = var;
            }
            else if (input.StartsWith("slice_distance"))
            {
                float var;
                if (!Single.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-us"), out var))return;
                imageCube.SliceDistance = var;
            }
            else if (input.StartsWith("xoffset"))
            {
                int var;
                if (!Int32.TryParse(value, out var)) return;
                imageCube.OffsetX = var;
            }
            else if (input.StartsWith("yoffset"))
            {
                int var;
                if (!Int32.TryParse(value, out var)) return;
                imageCube.OffsetY = var;
            }
            else if (input.StartsWith("dimx"))
            {
                int var;
                if (!Int32.TryParse(value, out var)) return;
                imageCube.DimX = var;
            }
            else if (input.StartsWith("dimy"))
            {
                int var;
                if (!Int32.TryParse(value, out var)) return;
                imageCube.DimY = var;
            }
            else if (input.StartsWith("dimz"))
            {
                int var;
                if (!Int32.TryParse(value, out var)) return;
                imageCube.DimZ = var;
            }
        }
    }
}