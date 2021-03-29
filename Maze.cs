using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    class Maze
    {
        public Point StartingPoint { get; set; }
        public int RowLength { get; set; }
        public int ColumnLength { get; set; }
        public char[][] CharMaze { get; set; }

        Stack<Point> stack;

        public Maze(String fileName)
        {
            string[] fileLines = File.ReadAllLines(fileName);
            string[] lengths = fileLines[0].Split();
            RowLength = Convert.ToInt32(lengths[0]);
            ColumnLength = Convert.ToInt32(lengths[1]);
            string[] startingPointText = fileLines[1].Split();
            StartingPoint = new Point(Convert.ToInt32(startingPointText[0]), Convert.ToInt32(startingPointText[1]));

            CharMaze = new char[fileLines.Length - 2][];
            for (int i = 2; i < fileLines.Length; i++)
            {
                CharMaze[i - 2] = fileLines[i].ToCharArray();
            }

        }
        public Maze(int startingRow, int startingColumn, char[][] existingMaze)
        {
            if (startingColumn == 0 || startingRow == 0 || existingMaze[startingRow][startingColumn] == 'E')
            {
                throw new ApplicationException();
            }
            else if (startingColumn <= -1 || startingRow <= -1 || startingColumn >= existingMaze[0].Length || startingRow >= existingMaze.Length)
            {
                throw new IndexOutOfRangeException();
            }
            CharMaze = existingMaze;
            StartingPoint = new Point(startingRow, startingColumn);
            RowLength = existingMaze.Length;
            ColumnLength = existingMaze[0].Length;
            //CurrentStack = new Stack<Point>();
        }

        public char[][] GetMaze()
        {
            return CharMaze;
        }

        public string PrintMaze()
        {
            string maze = "";
            for (int i = 0; i < RowLength; i++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    maze += CharMaze[i][x].ToString();
                }
                maze += "\n";
            }
            return maze.TrimEnd(Environment.NewLine.ToCharArray());
        }

        public string BreadthFirstSearch()
        {
            Point endPoint = null;
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(StartingPoint);

            while(!queue.IsEmpty() && endPoint == null)
            {
                Point current = queue.Dequeue();

                CharMaze[current.Row][current.Column] = 'V';

                if (CharMaze[current.Row + 1][current.Column] != 'W' && CharMaze[current.Row + 1][current.Column] != 'V')
                {                   
                    Point path = new Point(current.Row + 1, current.Column, current);

                    if (CharMaze[current.Row + 1][current.Column] == 'E')
                    {
                        endPoint = path;
                    }
                    else
                    {
                        CharMaze[current.Row + 1][current.Column] = 'V';
                    }

                    queue.Enqueue(path);

                }
                if (CharMaze[current.Row][current.Column + 1] != 'W' && CharMaze[current.Row][current.Column + 1] != 'V')
                {
                    Point path = new Point(current.Row, current.Column + 1, current);
                    if (CharMaze[current.Row][current.Column + 1] == 'E')
                    {
                        endPoint = path;
                    }
                    else
                    {
                        CharMaze[current.Row][current.Column + 1] = 'V';
                    }

                    queue.Enqueue(path);

                }
                if (CharMaze[current.Row][current.Column - 1] != 'W' && CharMaze[current.Row][current.Column - 1] != 'V')
                {
                    Point path = new Point(current.Row, current.Column - 1, current);

                    if (CharMaze[current.Row][current.Column - 1] == 'E')
                    {
                        endPoint = path;
                    }
                    else
                    {
                        CharMaze[current.Row][current.Column - 1] = 'V';
                    }

                    queue.Enqueue(path);

                }
                if (CharMaze[current.Row - 1][current.Column] != 'W' && CharMaze[current.Row - 1][current.Column] != 'V')
                {
                    Point path = new Point(current.Row - 1, current.Column, current);
                    
                    if (CharMaze[current.Row - 1][current.Column] == 'E')
                    {
                        endPoint = path;
                    }
                    else
                    {
                        CharMaze[current.Row - 1][current.Column] = 'V';
                    }
                    queue.Enqueue(path);

                }

            }

            string stringPath = "";
            if (endPoint != null)
            {                
                stringPath = PrintPath(endPoint);

            }
            else
            {
                stringPath = "No exit found in maze!\n\n";
                stack = new Stack<Point>();
            }

            string mazePrint = PrintMaze();
            return stringPath + mazePrint;
        }

        public string PrintPath(Point EndingPoint)
        {
            Point parentEnd = EndingPoint.ParentPoint;
            Stack<Point> ReverseStack = new Stack<Point>();
            ReverseStack.Push(EndingPoint);
            string StringPath = EndingPoint + "\n";
            while (parentEnd != null)
            {
                CharMaze[parentEnd.Row][parentEnd.Column] = '.';
                ReverseStack.Push(parentEnd);

                StringPath = parentEnd + "\n" + StringPath;

                parentEnd = parentEnd.ParentPoint;
            }

            stack = ReverseStack;
            StringPath = string.Format("Path to follow from Start {0} to Exit {1} - {2} steps:\n{3}", ReverseStack.Top(), EndingPoint, stack.Size, StringPath);

            return StringPath;
        }


        public Stack<Point> GetPathToFollow()
        {
            if (stack == null)
            {
                throw new ApplicationException();
            }

            return stack.Clone();
        }

    }
}
