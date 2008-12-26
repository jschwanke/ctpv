using System;
using System.Collections.Generic;
using System.Text;

namespace Viewer.Controller
{
    public class Position
    {
        public Position(int lastWidth, int lastCenter, int lastTransversal, int lastSagittal, int lastFrontal)
        {
            LastWidth = lastWidth;
            LastCenter = lastCenter;
            LastTransversal = lastTransversal;
            LastSagittal = lastSagittal;
            LastFrontal = lastFrontal;
        }

        public int LastTransversal { get; set; }

        public int LastSagittal { get; set; }

        public int LastFrontal { get; set; }

        public int LastWidth { get; set; }

        public int LastCenter { get; set; }
    }
}
