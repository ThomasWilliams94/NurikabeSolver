using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurikabeSolver
{

    class NumberCell
    {
        int itsRow { get;set;}
        int itsColumn { get; set; }
        int itsValue { get; set; }

        // Number cells will always be island cells
        const Grid.CellType itsCellType = Grid.CellType.Number;

        public NumberCell()
        {

        }

        public NumberCell(int column, int row, int value)
        {
            this.Row = row;
            this.Column = column;
            this.Value = value;
        }

        /// <summary>
        /// Zero-indexed row value
        /// </summary>
        public int Row
        {
            get
            {
                return itsRow;
            }
            set
            {
                itsRow = value;
            }
        }

        /// <summary>
        /// Zero-indexed column value
        /// </summary>
        public int Column
        {
            get
            {
                return itsColumn;
            }
            set
            {
                itsColumn = value;
            }
        }

        /// <summary>
        /// Actual value for that cell
        /// </summary>
        public int Value
        {
            get
            {
                return itsValue;
            }
            set
            {
                itsValue = value;
            }
        }
    }
}
