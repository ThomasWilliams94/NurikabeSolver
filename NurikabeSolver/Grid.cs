using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurikabeSolver
{
    class Grid
    {
        public enum CellType
        {
            Unknown,
            River,
            Island,
            Number
        }

        public enum CellClassification
        {
            Edge,
            Corner,
            Middle
        }

        private int itsWidth;
        private int itsHeight;
        List<NumberCell> itsNumbers;
        

        // For displaying to the console
        private string[,] itsGrid;

        private Cell[,] itsCells;

        // For keeping track of what the cell types are
        private CellType[,] itsCellTypes;

        public Grid(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            
            this.itsNumbers = new List<NumberCell>();
            this.itsGrid = new string[itsWidth, itsHeight];
            this.itsCellTypes = new CellType[itsWidth, itsHeight];
            this.itsCells = new Cell[itsWidth, itsHeight];

            // Set all the values in itsGrid to ? to begin with
            int iWidth;
            int iHeight;
            for (iWidth = 0; iWidth < this.Width; iWidth++)
            {
                for (iHeight = 0; iHeight < this.Height; iHeight++)
                {
                    itsGrid[iWidth, iHeight] = " ";

                    CellClassification cellClass;

                    if (iWidth == 0 || iWidth == this.Width - 1)
                    {
                        if (iHeight == 0 || iHeight == this.Height - 1)
                        {
                            cellClass = CellClassification.Corner;
                        }
                        else
                        {
                            cellClass = CellClassification.Edge;
                        }
                    }
                    else if (iHeight == 0 || iHeight == this.Height - 1)
                    {
                        cellClass = CellClassification.Edge;
                    }
                    else
                    {
                        cellClass = CellClassification.Middle;
                    }

                    itsCells[iWidth, iHeight] = new Cell(iWidth, iHeight, cellClass, CellType.Unknown);

                    
                    itsCellTypes[iWidth, iHeight] = CellType.Unknown;

                }
            }
        }

        public List<NumberCell> Numbers
        {
            get
            {
                return itsNumbers;
            }
        }

        public Cell[,] Cells
        {
            get
            {
                return itsCells;
            }
            set
            {
                itsCells = value;
            }
        }

        public int Width
        {
            get
            {
                return itsWidth;
            }
            set
            {
                if (value > 0)
                {
                    itsWidth = value;
                }
            }
        }

        public int Height
        {
            get
            {
                return itsHeight;
            }
            set
            {
                itsHeight = value;
            }
        }

        public string[,] Grid1
        {
            get
            {
                return itsGrid;
            }
            set
            {
                itsGrid = value;
            }
        }

        public CellType[,] CellTypes
        {
            get
            {
                return itsCellTypes;
            }
            set
            {
                
                itsCellTypes = value;
            }
        }

        public CellType GetCellType(int x, int y) 
        {
            CellType value = itsCellTypes[x, y];
            return value;            
        }

        public bool SetCellType(int x, int y, CellType cellType)
        {
            if (itsCells[x,y].CellType == CellType.Unknown)
            {
                itsCells[x,y].CellType = cellType;
                return true;
            }
            else if (itsCells[x, y].CellType != cellType)
            {
                Console.Error.WriteLine("Inconsistent workings... error with program.");
                return false;
            }

            return true;
        }

        public string GetNumberAsString(int x, int y)
        {
            foreach (NumberCell numCell in itsNumbers)
            {
                if (numCell.Row == x && numCell.Column == y)
                {
                    return numCell.Value.ToString();
                }
            }

            return " ";
        }
        
        public void WriteGridToConsole(Grid theGrid)
        {
            Console.WriteLine();

            int iWidth;
            int iHeight;

            for (iWidth = 0; iWidth < theGrid.Width; iWidth++)
            {
                for (iHeight = 0; iHeight < theGrid.Height; iHeight++)
                {
                    CellType cellType = theGrid.Cells[iWidth, iHeight].CellType; // theGrid.CellTypes[iWidth, iHeight];

                    
                    switch (cellType)
                    {
                        case CellType.Unknown:
                            Console.Write("[ ] ");
                            break;                        
                        case CellType.Number:
                            Console.Write("[" + GetNumberAsString(iWidth,iHeight) + "] ");
                            break;
                        case CellType.River:
                            Console.Write("[R] ");
                            break;
                        case CellType.Island:
                            Console.Write("[I] ");
                            break;
                        default:
                            Console.Write("[?] ");
                            break;
                    }                    
                }
                Console.Write("\n\n");
            }

        }

        public void CheckKnownCellsNeighbours(Grid theGrid)
        {
            foreach (Cell cell in theGrid.Cells)
            {
                Grid.CellType cellType = cell.CellType;

                bool onlyOneCellKnown = CheckOneCellUnknown(cell.XPos, cell.YPos, theGrid);

                switch (cellType)
                {
                    case Grid.CellType.Unknown:
                        break;
                    case Grid.CellType.River:
                        if (onlyOneCellKnown)
                        {
                            // need some way of knowing which of the cells is unknown
                            // so we can do something with it. 
                            // potentially create a structure that is 'unknown cell' with its coords...

                            // Also, put a condition in the setter for the cell type that only allows
                            // "Unknown" cells' value to be changed. 
                        }
                        break;
                    case Grid.CellType.Island:
                        break;
                    case Grid.CellType.Number:
                        break;
                    default:
                        Console.Error.WriteLine("Error in program.");
                        break;
                }
            }
        }

        public bool CheckOneCellUnknown(int xPosCurrentCell, int yPosCurrentCell, Grid theGrid)
        {
            #region Corner
            if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell].CellClassification == Grid.CellClassification.Corner)
            {
                if (xPosCurrentCell == 0)
                {
                    if (yPosCurrentCell == 0)
                    {
                        // Then we're in the top left corner, so check right and below:
                        if (theGrid.Cells[xPosCurrentCell + 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        {
                            if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell + 1].CellType == Grid.CellType.Unknown)
                            {
                                // Both unknown, so return false
                                return false;
                            }
                            else
                            {
                                // We know the value of all but one of the neighbouring cells, so return true
                                return true;
                            }
                        }
                        else if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell + 1].CellType == Grid.CellType.Unknown)
                        {
                            // Then one cell is known, so return true;
                            return true;
                        }
                    }

                    if (yPosCurrentCell == theGrid.Height - 1)
                    {
                        // Then we're in the bottom left corner, so check right and above
                        if (theGrid.Cells[xPosCurrentCell + 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        {
                            if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell - 1].CellType == Grid.CellType.Unknown)
                            {
                                // Both unknown, so return false
                                return false;
                            }
                            else
                            {
                                // We know the value of all but one of the neighbouring cells, so return true
                                return true;
                            }
                        }
                        else if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell - 1].CellType == Grid.CellType.Unknown)
                        {
                            // Then one cell is known, so return true;
                            return true;
                        }
                    }
                }

                if (xPosCurrentCell == theGrid.Width - 1)
                {
                    if (yPosCurrentCell == 0)
                    {
                        // Then we're in the top right corner, so check left and below:
                        if (theGrid.Cells[xPosCurrentCell - 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        {
                            if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell + 1].CellType == Grid.CellType.Unknown)
                            {
                                // Both unknown, so return false
                                return false;
                            }
                            else
                            {
                                // We know the value of all but one of the neighbouring cells, so return true
                                return true;
                            }
                        }
                        else if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell + 1].CellType == Grid.CellType.Unknown)
                        {
                            // Then one cell is known, so return true;
                            return true;
                        }
                    }

                    if (yPosCurrentCell == theGrid.Height - 1)
                    {
                        // Then we're in the bottom right corner, so check left and above
                        if (theGrid.Cells[xPosCurrentCell - 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        {
                            if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell - 1].CellType == Grid.CellType.Unknown)
                            {
                                // Both unknown, so return false
                                return false;
                            }
                            else
                            {
                                // We know the value of all but one of the neighbouring cells, so return true
                                return true;
                            }
                        }
                        else if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell - 1].CellType == Grid.CellType.Unknown)
                        {
                            // Then one cell is known, so return true;
                            return true;
                        }
                    }
                }

            }

            #endregion

            #region Edge

            if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell].CellClassification == Grid.CellClassification.Edge)
            {
                if (xPosCurrentCell == 0)
                {
                    // This is the value we will check with at the end. If it is equal to our running
                    // total of cells that are known, then only one cell is unknown. Otherwise, something
                    // is wrong so return false
                    int target = 2;

                    // We are on the left edge, so check up, right, and down
                    bool cellAboveKnown = (theGrid.Cells[xPosCurrentCell, yPosCurrentCell - 1].CellType == Grid.CellType.Unknown)
                        ? false : true;
                    bool cellRightKnown = (theGrid.Cells[xPosCurrentCell + 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        ? false : true;
                    bool cellBelowKnown = (theGrid.Cells[xPosCurrentCell, yPosCurrentCell + 1].CellType == Grid.CellType.Unknown)
                        ? false : true;

                    int totalKnown = 0;

                    if (cellAboveKnown)
                    {
                        totalKnown++;
                    }
                    if (cellBelowKnown)
                    {
                        totalKnown++;
                    }
                    if (cellRightKnown)
                    {
                        totalKnown++;
                    }

                    if (totalKnown == target)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                if (xPosCurrentCell == theGrid.Width - 1)
                {
                    // This is the value we will check with at the end. If it is equal to our running
                    // total of cells that are known, then only one cell is unknown. Otherwise, something
                    // is wrong so return false
                    int target = 2;

                    // We are on the right edge, so check up, left, and down
                    bool cellAboveKnown = (theGrid.Cells[xPosCurrentCell, yPosCurrentCell - 1].CellType == Grid.CellType.Unknown)
                        ? false : true;
                    bool cellLeftKnown = (theGrid.Cells[xPosCurrentCell - 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        ? false : true;
                    bool cellBelowKnown = (theGrid.Cells[xPosCurrentCell, yPosCurrentCell + 1].CellType == Grid.CellType.Unknown)
                        ? false : true;

                    int totalKnown = 0;

                    if (cellAboveKnown)
                    {
                        totalKnown++;
                    }
                    if (cellBelowKnown)
                    {
                        totalKnown++;
                    }
                    if (cellLeftKnown)
                    {
                        totalKnown++;
                    }

                    if (totalKnown == target)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                if (yPosCurrentCell == 0)
                {
                    // This is the value we will check with at the end. If it is equal to our running
                    // total of cells that are known, then only one cell is unknown. Otherwise, something
                    // is wrong so return false
                    int target = 2;

                    // We are on the top edge, so check ldft, right, and down
                    bool cellLeftKnown = (theGrid.Cells[xPosCurrentCell - 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        ? false : true;
                    bool cellRightKnown = (theGrid.Cells[xPosCurrentCell + 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        ? false : true;
                    bool cellBelowKnown = (theGrid.Cells[xPosCurrentCell, yPosCurrentCell + 1].CellType == Grid.CellType.Unknown)
                        ? false : true;

                    int totalKnown = 0;

                    if (cellLeftKnown)
                    {
                        totalKnown++;
                    }
                    if (cellBelowKnown)
                    {
                        totalKnown++;
                    }
                    if (cellRightKnown)
                    {
                        totalKnown++;
                    }

                    if (totalKnown == target)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (yPosCurrentCell == theGrid.Height - 1)
                {
                    // This is the value we will check with at the end. If it is equal to our running
                    // total of cells that are known, then only one cell is unknown. Otherwise, something
                    // is wrong so return false
                    int target = 2;

                    // We are on the bottom edge, so check left, right, and up
                    bool cellAboveKnown = (theGrid.Cells[xPosCurrentCell, yPosCurrentCell - 1].CellType == Grid.CellType.Unknown)
                        ? false : true;
                    bool cellRightKnown = (theGrid.Cells[xPosCurrentCell + 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        ? false : true;
                    bool cellLeftKnown = (theGrid.Cells[xPosCurrentCell - 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                        ? false : true;

                    int totalKnown = 0;

                    if (cellAboveKnown)
                    {
                        totalKnown++;
                    }
                    if (cellLeftKnown)
                    {
                        totalKnown++;
                    }
                    if (cellRightKnown)
                    {
                        totalKnown++;
                    }

                    if (totalKnown == target)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }


            #endregion

            #region Middle

            if (theGrid.Cells[xPosCurrentCell, yPosCurrentCell].CellClassification == Grid.CellClassification.Middle)
            {
                // if we get here, then we're not an edge or a corner, so we are in the middle, and it is
                // safe to check in all directions.

                // This is the value we will check with at the end. If it is equal to our running
                // total of cells that are known, then only one cell is unknown. Otherwise, something
                // is wrong so return false
                int target = 3;

                bool cellAboveKnown = (theGrid.Cells[xPosCurrentCell, yPosCurrentCell - 1].CellType == Grid.CellType.Unknown)
                    ? false : true;
                bool cellLeftKnown = (theGrid.Cells[xPosCurrentCell - 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                    ? false : true;
                bool cellRightKnown = (theGrid.Cells[xPosCurrentCell + 1, yPosCurrentCell].CellType == Grid.CellType.Unknown)
                    ? false : true;
                bool cellBelowKnown = (theGrid.Cells[xPosCurrentCell, yPosCurrentCell + 1].CellType == Grid.CellType.Unknown)
                    ? false : true;

                int totalKnown = 0;

                if (cellAboveKnown)
                {
                    totalKnown++;
                }
                if (cellLeftKnown)
                {
                    totalKnown++;
                }
                if (cellRightKnown)
                {
                    totalKnown++;
                }
                if (cellBelowKnown)
                {
                    totalKnown++;
                }

                if (totalKnown == target)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            #endregion

            return false;
        }

    }
}
