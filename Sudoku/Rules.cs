using System;

namespace Sudoku
{
    public class Rule
    {
        /*
         * param row/col: Row/column of recently affected cell
         * param value: Value of recently affected cell
         */
        public virtual void Execute(int row, int col, int value, Board board)
        {
            throw new NotImplementedException();
        }
    }

    public class ColumnInvalidateRule : Rule
    {
        public override void Execute(int row, int col, int value, Board board)
        {
            for (var r = 0; r < 9; r++)
            {
                if (r == row) continue;

                board.MarkInvalid(r, col, value);
            }
        }
    }

    public class RowInvalidateRule : Rule
    {
        public override void Execute(int row, int col, int value, Board board)
        {
            for (var c = 0; c < 9; c++)
            {
                if (c == col) continue;

                board.MarkInvalid(row, c, value);
            }
        }
    }

    public class BoxInvalidateRule : Rule
    {
        public override void Execute(int row, int col, int value, Board board)
        {
            // Determine top-left coords of box
            var startRow = (row / 3) * 3;
            var startCol = (col / 3) * 3;

            for (var rowOff = 0; rowOff < 3; rowOff++)
            {
                for (var colOff = 0; colOff < 3; colOff++)
                {
                    var r = startRow + rowOff;
                    var c = startCol + colOff;

                    if (r == row && c == col) continue;

                    board.MarkInvalid(r, c, value);
                }
            }
        }
    }
}