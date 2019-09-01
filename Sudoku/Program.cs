﻿using System;

namespace Sudoku
{
    class Program
    {
        private class MockBoard : Board
        {
            private readonly Random _random;
            private int _nextValue;

            public MockBoard() : base(null)
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
            var rules = new Rule[]
            {
                new RowInvalidateRule(),
                new ColumnInvalidateRule(),
                new BoxInvalidateRule()
            };

            IView view = new ConsoleView();

            var board = new Board(rules);
            view.Display(board);

            // Testing
            for (var i = 0; i < 7; i++)
            {
                board.SetValue(0, i, i);
                view.Display(board);
            }
            board.SetValue(8, 8, 8);
            view.Display(board);
        }
    }
}