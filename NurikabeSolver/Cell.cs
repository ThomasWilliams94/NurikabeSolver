using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurikabeSolver
{
    class Cell
    {
        Grid.CellType itsCellType;
        Grid.CellClassification itsCellClassification;

        int itsXPos;
        int itsYPos;

        public Cell(int xPos, int yPos, Grid.CellClassification cellClass, Grid.CellType cellType)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.CellClassification = cellClass;
            this.CellType = cellType;
        }

        public int XPos
        {
            get
            {
                return itsXPos;
            }
            set
            {
                itsXPos = value;
            }
        }

        public int YPos
        {
            get
            {
                return itsYPos;
            }
            set
            {
                itsYPos = value;
            }
        }

        public Grid.CellType CellType
        {
            get
            {
                return itsCellType;
            }
            set
            {
                itsCellType = value;
            }
        }

        public Grid.CellClassification CellClassification
        {
            get
            {
                return itsCellClassification;
            }
            set
            {
                itsCellClassification = value;
            }
        }
    }
}
