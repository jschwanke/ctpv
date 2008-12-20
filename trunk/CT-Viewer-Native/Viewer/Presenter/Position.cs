using System;
using System.Collections.Generic;
using System.Text;

namespace Viewer.Presenter
{
    public class Position
    {
        private int lastWidth;
        private int lastCenter;
        private int lastTransversal;
        private int lastSagittal;
        private int lastFrontal;


        public Position(int lastWidth, int lastCenter, int lastTransversal, int lastSagittal, int lastFrontal)
        {
            this.lastWidth = lastWidth;
            this.lastCenter = lastCenter;
            this.lastTransversal = lastTransversal;
            this.lastSagittal = lastSagittal;
            this.lastFrontal = lastFrontal;
        }

        public int LastTransversal
        {
            get { return lastTransversal; }
            set { lastTransversal = value; }
        }

        public int LastSagittal
        {
            get { return lastSagittal; }
            set { lastSagittal = value; }
        }

        public int LastFrontal
        {
            get { return lastFrontal; }
            set { lastFrontal = value; }
        }

        public int LastWidth
        {
            get { return lastWidth; }
            set { lastWidth = value; }
        }

        public int LastCenter
        {
            get { return lastCenter; }
            set { lastCenter = value; }
        }
    }
}
