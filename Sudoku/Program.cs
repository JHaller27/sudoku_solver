using System;

namespace Sudoku
{
    class Program
    {
        private class MockBoard : Board
        {
            private readonly Random _random;
            private int _nextValue;

            public MockBoard() : base()
            {
                _random = new Random();
                _nextValue = 9;
            }

            public override bool HasValue(int row, int col)
            {
                if (_random.Next(0, 20) >= 10) return false;
                _nextValue = _random.Next(0, 8);
                return true;
            }

            public override int GetValue(int row, int col)
            {
                return _nextValue;
            }

            public override bool IsValid(int row, int col, int value)
            {
                return _random.Next(0, 20) < 10;
            }
        }

        static void Main(string[] args)
        {
            Board board = new MockBoard();
            IView view = new ConsoleView();
            view.Display(board);
        }
    }
}