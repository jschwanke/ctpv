using System;
using System.Collections.Generic;

namespace Viewer.Core
{
    public class CubeManager
    {
        private readonly Dictionary<Guid, ImageCube> imageCubes;

        public CubeManager()
        {
            imageCubes = new Dictionary<Guid, ImageCube>();
        }

        public ImageCube GetImageCube(Guid imageID)
        {
            return imageCubes[imageID];
        }

        public void RemoveImageCube(Guid imageID)
        {
            imageCubes.Remove(imageID);
        }

        public ImageCube CreateImageCube(string hedFile, string ctxFile)
        {
            ImageCube cube = Reader.ReadFiles(hedFile, ctxFile);
            imageCubes.Add(cube.ImageID,cube);
            return cube;
        }

        public Guid GetImageCubeByFilename(string filename)
        {
            Guid returnValue = Guid.Empty;
            foreach (KeyValuePair<Guid,ImageCube> entry in imageCubes)
            {
                if(entry.Value.HedFile.CompareTo(filename) == 0 ||
                   entry.Value.CtxFile.CompareTo(filename) == 0 )
                {
                    returnValue = entry.Key;
                }
            }
            return returnValue;
        }
    }
}