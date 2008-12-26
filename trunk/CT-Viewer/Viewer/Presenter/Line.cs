using System.Drawing;

namespace Viewer.Controller
{
    public class Line
    {
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Line(int x1, int y1, int x2, int y2)
        {
            Start = new Point(x1,y1);
            End = new Point(x2, y2);
        }

        public Point Start { get; set; }

        public Point End { get; set; }
    }
}