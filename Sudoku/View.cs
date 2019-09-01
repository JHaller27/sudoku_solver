using System;

namespace Sudoku
{
    public interface IView
    {
        void Display(Board board);
    }

    public class ConsoleView : IView
    {
        private const string VerticalDivider = "│";
        private const string HorizontalDivider = "─";
        private const string Intersection = "┼";

        private const string VerticalBlockDivider = "║";
        private const string HorizontalBlockDivider = "═";
        private const string BlockIntersection = "╬";

        private const string InvalidValue = " ";

        public ConsoleView()
        {
            Console.SetWindowSize(73, 37);
            DisplayEmptyBoard();
        }

        private static void DisplayHorizontalNormalDivider()
        {
            for (var c = 0; c < 9; c++)
            {
                if (c % 3 == 0 && c > 0)
                    Console.Write(BlockIntersection);
                else
                    Console.Write(Intersection);

                for (var i = 0; i < 3; i++)
                {
                    Console.Write(HorizontalDivider);
                    Console.Write(HorizontalDivider);
                }
                Console.Write(HorizontalDivider);
            }
            Console.WriteLine(Intersection);
        }

        private static void DisplayHorizontalBlockDivider()
        {
            for (var c = 0; c < 9; c++)
            {
                if (c % 3 == 0 && c > 0)
                    Console.Write(BlockIntersection);
                else
                    Console.Write(HorizontalBlockDivider);

                for (var i = 0; i < 3; i++)
                {
                    Console.Write(HorizontalBlockDivider);
                    Console.Write(HorizontalBlockDivider);
                }
                Console.Write(HorizontalBlockDivider);
            }
            Console.WriteLine(HorizontalBlockDivider);
        }

        private static void DisplayVerticalDividerRow()
        {
            for (var c = 0; c < 9; c++)
            {
                if (c % 3 == 0 && c > 0)
                {
                    Console.Write(VerticalBlockDivider);
                }
                else
                {
                    Console.Write(VerticalDivider);
                }
                Console.Write("       ");
            }
            Console.WriteLine(VerticalDivider);
        }

        private static void DisplayEmptyBoard()
        {
            Console.Clear();
            Console.SetWindowPosition(0, 0);

            for (var r = 0; r < 9; r++)
            {
                if (r % 3 == 0 && r > 0)
                {
                    DisplayHorizontalBlockDivider();
                }
                else
                {
                    DisplayHorizontalNormalDivider();
                }

                for (var rValid = 0; rValid < 3; rValid++)
                {
                    DisplayVerticalDividerRow();
                }
            }
            DisplayHorizontalNormalDivider();
        }

        private static void DisplayCell(Board board, int row, int col)
        {
            if (board.HasValue(row, col))
            {
                DisplayCellWithValue(board, row, col);
            }
            else
            {
                DisplayCellNoValue(board, row, col);
            }
        }

        private static void DisplayCellNoValue(Board board, int row, int col)
        {
            var startRow = 4 * row + 1;
            var startCol = 8 * col + 2;

            for (var rOff = 0; rOff < 3; rOff++)
            {
                for (var cOff = 0; cOff < 3; cOff++)
                {
                    Console.SetCursorPosition(startCol + (2 * cOff), startRow + rOff);

                    var targetValue = rOff * 3 + cOff;

                    if (!board.IsValid(row, col, targetValue))
                    {
                        Console.Write(InvalidValue);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(targetValue);
                        Console.ResetColor();
                    }
                }
            }  
        }

        private static void DisplayCellWithValue(Board board, int row, int col)
        {
            var startRow = 4 * row + 1;
            var startCol = 8 * col + 2;
            var value = board.GetValue(row, col);

            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(startCol, startRow);
            Console.Write("┌───┐");

            Console.SetCursorPosition(startCol, startRow + 1);
            Console.Write($"│ {value} │");

            Console.SetCursorPosition(startCol, startRow + 2);
            Console.Write("└───┘");

            Console.ResetColor();
        }

        public void Display(Board board)
        {
            for (var r = 0; r < 9; r++)
            {
                for (var c = 0; c < 9; c++)
                {
                    DisplayCell(board, r, c);
                }
            }

            Console.SetCursorPosition(0, 37);
            Console.Write("Press any key to exit...");
            // Console.ReadKey();
        }
    }
}