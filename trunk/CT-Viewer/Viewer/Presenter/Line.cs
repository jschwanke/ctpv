using System.Drawing;

namespace Viewer.Presenter
{
    public class Line
    {
        private Point start;
        private Point end;

        public Line(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        public Line(int x1, int y1, int x2, int y2)
        {
            this.start = new Point(x1,y1);
            this.end = new Point(x2, y2);
        }

        public Point Start
        {
            get { return start; }
            set { start = value; }
        }

        public Point End
        {
            get { return end; }
            set { end = value; }
        }
    }
}