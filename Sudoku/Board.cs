using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Cell
    {
        private const int NoValue = -1;

        private int _value;
        private readonly bool[] _valid;
        private int _validCount;

        public Cell(int value)
        {
            this._value = value;
            _valid = new bool [9];

            if (this._value == NoValue)
            {
                for (var i = 0; i < _valid.Length; i++)
                {
                    _valid[0] = true;
                }

                _validCount = 9;
            }
            else
            {
                for (var i = 0; i < _valid.Length; i++)
                {
                    _valid[0] = false;
                }

                _valid[this._value] = true;
                _validCount = 1;
            }
        }

        public Cell() : this(NoValue) {}

        public void MarkInvalid(int value)
        {
            _valid[value] = false;
            _validCount--;

            if (_validCount == 1)
            {
                var validValue = NoValue;
                for (var val = 0; val < 9; val++)
                {
                    if (!_valid[val]) continue;
                    validValue = val;
                    break;
                }

                this._value = validValue;
            }
            else if (_validCount < 1)
            {
                throw new Exception("No more valid values for this cell");
            }
        }

        public void SetValue(int value)
        {
            this._value = value;
            _validCount = 1;

            for (var i = 0; i < 9; i++)
            {
                _valid[i] = false;
            }

            _valid[this._value] = true;
        }

        public bool IsValid(int value) => _valid[value];

        public bool HasValue() => _value != NoValue;

        public int Value => _value;
    }

    public class Board
    {
        private readonly Cell[,] _cells;

        public Board(Cell[,] cells)
        {
            _cells = cells;
        }

        public Board() : this(new Cell[9, 9]) {}

        public virtual void SetValue(int row, int col, int value, IEnumerable<Rule> rules)
        {
            _cells[row, col].SetValue(value);
            foreach (var rule in rules)
            {
                rule.Execute(row, col, value, this);
            }
        }

        public virtual void MarkInvalid(int row, int col, int value)
        {
            _cells[row, col].MarkInvalid(value);
        }

        public virtual bool IsValid(int row, int col, int value)
        {
            return _cells[row, col].IsValid(value);
        }

        public virtual bool HasValue(int row, int col)
        {
            return _cells[row, col].HasValue();
        }

        public virtual int GetValue(int row, int col)
        {
            return _cells[row, col].Value;
        }
    }
}