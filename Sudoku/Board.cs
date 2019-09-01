using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Cell
    {
        private const int NoValue = -1;

        private readonly bool[] _valid;
        private int _validCount;

        public int Value { get; private set; }

        private Cell(int value)
        {
            this.Value = value;
            _valid = new bool [9];

            if (this.Value == NoValue)
            {
                for (var i = 0; i < _valid.Length; i++)
                {
                    _valid[i] = true;
                }

                _validCount = 9;
            }
            else
            {
                for (var i = 0; i < _valid.Length; i++)
                {
                    _valid[0] = false;
                }

                _valid[this.Value] = true;
                _validCount = 1;
            }
        }

        public Cell() : this(NoValue) {}

        /*
         * Return new value if changed, -1 otherwise
         */
        public int MarkInvalid(int value)
        {
            if (!_valid[value] || HasValue()) return -1;

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

                this.Value = validValue;

                return Value;
            }
            else if (_validCount < 1)
            {
                throw new Exception("No more valid values for this cell");
            }

            return -1;
        }

        public void SetValue(int value)
        {
            this.Value = value;
            _validCount = 1;

            for (var i = 0; i < 9; i++)
            {
                _valid[i] = false;
            }

            _valid[this.Value] = true;
        }

        public bool IsValid(int value)
        {
            return _valid[value];
        }

        public bool HasValue() => Value != NoValue;
    }

    public class Board
    {
        private readonly IEnumerable<Rule> _rules;
        private readonly Cell[,] _cells;

        public Board(IEnumerable<Rule> rules, Cell[,] cells)
        {
            _rules = rules;
            _cells = cells;
        }

        public Board(IEnumerable<Rule> rules)
        {
            var maxSize = 9;
            _cells = new Cell[9, 9];
            for (var r = 0; r < maxSize; r++)
            {
                for (var c = 0; c < maxSize; c++)
                {
                    _cells[r, c] = new Cell();
                }
            }

            _rules = rules;
        }

        private void ExecuteRules(int row, int col, int value)
        {
            foreach (var rule in _rules)
            {
                rule.Execute(row, col, value, this);
            }
        }

        public virtual void SetValue(int row, int col, int value)
        {
            _cells[row, col].SetValue(value);
            ExecuteRules(row, col, value);
        }

        public virtual void MarkInvalid(int row, int col, int value)
        {
            var valueChanged = _cells[row, col].MarkInvalid(value);
            if (valueChanged != -1)
                ExecuteRules(row, col, valueChanged);
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