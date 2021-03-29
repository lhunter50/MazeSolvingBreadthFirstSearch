using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class Point
    {
        public int Row { get; set; }
        public int Column { get; }

        public Point ParentPoint { get; set; }

        public Point(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public Point(int row, int column, Point parentPoint)
        {
            Row = row;
            Column = column;
            ParentPoint = parentPoint;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", Row, Column);
        }

    }
}
