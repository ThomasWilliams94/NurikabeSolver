using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurikabeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput;
            int width;
            int height;
            bool ok = true;

            Grid grid;

            Console.WriteLine("Welcome to the Nurikabe Solver!");
            Console.WriteLine("\nHow big is your grid?");
                
            do 
            {
                Console.Write("\nEnter the width: ");
                userInput = Console.ReadLine();
                
                //// Hardcode for now
                //userInput = "6";
                if (int.TryParse(userInput, out width) && (width > 0))
                {
                    ok = true;
                }
                else 
                {
                    Console.WriteLine("\nInvalid input.");
                    ok = false;
                }
            }
            while(!ok);

            do
            {
                Console.Write("\nEnter the height: ");
                userInput = Console.ReadLine();

                //// Hardcode for now
                //userInput = "6";
                if (int.TryParse(userInput, out height) && (height > 0))
                {
                    ok = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid input.");
                    ok = false;
                }
            }
            while (!ok);

            grid = new Grid(width, height);

            Console.WriteLine("\nGreat! Here is what your grid looks like:");
            //Console.WriteLine();

            //for (int iWidth = 0; iWidth < grid.Width; iWidth++)
            //{
            //    for (int iHeight = 0; iHeight < grid.Height; iHeight++)
            //    {
            //        Console.Write("[?] ");
            //    }
            //    Console.Write("\n\n");
            //}

            grid.WriteGridToConsole(grid);

            Console.WriteLine("Enter a list of numbers in the following format: Column Row Value (all integers).\n\nType + when you're done.");

            // Need to check values are non-negative integers, and do not go out of bounds of the array...
            do
            {
                userInput = Console.ReadLine();

                if (userInput == "+")
                {
                    break;
                }

                char[] sep = new char[1];
                sep[0] = ' ';
                string[] values = userInput.Split(sep);

                if (values.Count() != 3)
                {
                    Console.WriteLine("You did not enter three numbers, try again.");
                }
                else
                {
                    NumberCell numCell = new NumberCell();
                    // Take one away because the arrays are 0-indexed
                    numCell.Column = int.Parse(values[0]) - 1;
                    numCell.Row = int.Parse(values[1]) - 1;
                    // This is a value, so do not take away 1
                    numCell.Value = int.Parse(values[2]);

                    grid.Numbers.Add(numCell);
                }
            }
            while (userInput != "+");

            //// Add six numbers as default for now.
            //// First two numbers are 0-indexed
            //grid.Numbers.Add(new NumberCell(0, 3, 1));
            //grid.Numbers.Add(new NumberCell(1, 2, 2));
            //grid.Numbers.Add(new NumberCell(1, 4, 2));
            //grid.Numbers.Add(new NumberCell(4, 2, 3));
            //grid.Numbers.Add(new NumberCell(4, 5, 4));
            //grid.Numbers.Add(new NumberCell(5, 1, 1));

            Console.WriteLine("\nThanks! This is what your grid now looks like:\n");

            foreach (NumberCell numCell in grid.Numbers)
            {
                grid.Grid1[numCell.Row, numCell.Column] = numCell.Value.ToString();
                grid.CellTypes[numCell.Row, numCell.Column] = Grid.CellType.Number;

                grid.Cells[numCell.Row, numCell.Column].CellType = Grid.CellType.Number;
            }

            grid.WriteGridToConsole(grid);

            Console.WriteLine("\nFirst step: Fill in cells around 1s:");

            // This part of the code will fill in cells around those with a value of 1
            int iWidth;
            int iHeight;
            for (iWidth = 0; iWidth < grid.Width; iWidth++)
            {
                for (iHeight = 0; iHeight < grid.Height; iHeight++)
                {
                    if (grid.GetNumberAsString(iWidth, iHeight) == "1")
                    {
                        // Make the celltypes in the cells horizontally or vertically adjacent 
                        // to this one equal to 'River'

                        // To the left
                        if (iWidth > 0)
                        {

                            grid.CellTypes[iWidth - 1, iHeight] = Grid.CellType.River;
                            grid.Cells[iWidth - 1, iHeight].CellType = Grid.CellType.River;
                        }
                        // To the right
                        if (iWidth < grid.Width - 1)
                        {
                            grid.CellTypes[iWidth + 1, iHeight] = Grid.CellType.River;
                            grid.Cells[iWidth + 1, iHeight].CellType = Grid.CellType.River;
                        }
                        // Above
                        if (iHeight > 0)
                        {
                            grid.CellTypes[iWidth, iHeight - 1] = Grid.CellType.River;
                            grid.Cells[iWidth, iHeight - 1].CellType = Grid.CellType.River;
                        }
                        // Below
                        if (iHeight < grid.Height - 1)
                        {
                            grid.CellTypes[iWidth, iHeight + 1] = Grid.CellType.River;
                            grid.Cells[iWidth, iHeight + 1].CellType = Grid.CellType.River;
                        }
                    }

                }

            }

            grid.WriteGridToConsole(grid);

            Console.ReadLine();

            Console.WriteLine("Next, check for diagonally adjacent cells and mark their\nshared horizontal/adjacent cells as River:");

            foreach (NumberCell numCell in grid.Numbers)
            {
                // The 'working out what edge' thing could potentially go 
                // into another enum cell 'classification' when creating the grid

                // Ideally, we want a 'cell' calss with these different aspects on it
                // and then the 'GRID' can have these instead of 'CellTypes'.

                //// work out if we're on an edge 
                //bool isEdge = false;
                //bool isCorner = false;
                //string whatEdge = "";
                //string whatCorner = "";

                //if (numCell.Row == 0)
                //{                    
                //    if(numCell.Column == 0) 
                //    {
                //        isCorner = true;
                //        whatCorner = "TL";
                //    }
                //    else if(numCell.Column == grid.Width -1) {
                //        isCorner = true;
                //        whatCorner = "TR";
                //    }
                //    else 
                //    {
                //        isEdge = true;
                //        whatEdge = "T";
                //    }                   
                //}

                //if (numCell.Row == grid.Height - 1)
                //{
                //    if (numCell.Column == 0)
                //    {
                //        isCorner = true;
                //        whatCorner = "BL";
                //    }
                //    else if (numCell.Column == grid.Width - 1)
                //    {
                //        isCorner = true;
                //        whatCorner = "BR";
                //    }
                //    else
                //    {
                //        isEdge = true;
                //        whatEdge = "B";
                //    }       
                //}

                //if (numCell.Column == 0 && !isCorner) 
                //{
                //    isEdge = true;
                //    whatEdge = "L";
                //}

                //if (numCell.Column == grid.Width - 1 && !isCorner)
                //{
                //    isEdge = true;
                //    whatEdge = "R";
                //}


                // If we're somewhere in the middle of the grid, then we can do everything 
                // without worrying about it
                if (grid.Cells[numCell.Row, numCell.Column].CellClassification == Grid.CellClassification.Middle)
                {
                    if (grid.CellTypes[numCell.Row - 1, numCell.Column - 1] == Grid.CellType.Number)
                    {
                        grid.SetCellType(numCell.Row, numCell.Column - 1, Grid.CellType.River);
                        grid.SetCellType(numCell.Row - 1, numCell.Column, Grid.CellType.River);
                    }

                    if (grid.CellTypes[numCell.Row - 1, numCell.Column + 1] == Grid.CellType.Number)
                    {
                        grid.SetCellType(numCell.Row, numCell.Column + 1, Grid.CellType.River);
                        grid.SetCellType(numCell.Row - 1, numCell.Column, Grid.CellType.River);
                    }

                    if (grid.CellTypes[numCell.Row + 1, numCell.Column - 1] == Grid.CellType.Number)
                    {
                        grid.SetCellType(numCell.Row, numCell.Column - 1, Grid.CellType.River);
                        grid.SetCellType(numCell.Row + 1, numCell.Column, Grid.CellType.River);
                    }

                    if (grid.CellTypes[numCell.Row + 1, numCell.Column + 1] == Grid.CellType.Number)
                    {
                        grid.SetCellType(numCell.Row, numCell.Column + 1, Grid.CellType.River);
                        grid.SetCellType(numCell.Row + 1, numCell.Column, Grid.CellType.River);
                    }
                }

                // I don't think the bottom needs to happen, since the 'edge' of the grid will get
                // checked by the inner cells (because the above checks all diagonally adjacent cells)

                //if (isCorner)
                //{
                //    switch (whatCorner)
                //    {
                //        case "TL":
                //            if (grid.CellTypes[numCell.Row + 1, numCell.Column + 1] == Grid.CellType.Number) 
                //            {
                //                grid.SetCellType(numCell.Row, numCell.Column + 1, Grid.CellType.River);
                //                grid.SetCellType(numCell.Row + 1, numCell.Column, Grid.CellType.River);
                //            }
                //            break;
                //        case "TR":
                //            if (grid.CellTypes[numCell.Row + 1, numCell.Column - 1] == Grid.CellType.Number) 
                //            {
                //                grid.SetCellType(numCell.Row, numCell.Column - 1, Grid.CellType.River);
                //                grid.SetCellType(numCell.Row + 1, numCell.Column, Grid.CellType.River);
                //            }                            
                //            break;
                //        case "BL":
                //            if (grid.CellTypes[numCell.Row - 1, numCell.Column + 1] == Grid.CellType.Number) 
                //            {
                //                grid.SetCellType(numCell.Row, numCell.Column + 1, Grid.CellType.River);
                //                grid.SetCellType(numCell.Row - 1, numCell.Column, Grid.CellType.River);
                //            }
                //            break;
                //        case "BR":
                //            if (grid.CellTypes[numCell.Row - 1, numCell.Column - 1] == Grid.CellType.Number) 
                //            {
                //                grid.SetCellType(numCell.Row, numCell.Column - 1, Grid.CellType.River);
                //                grid.SetCellType(numCell.Row - 1, numCell.Column, Grid.CellType.River);
                //            }
                //            break;
                //        default:
                //            Console.Error.WriteLine("Error in program.");
                //            break;
                //    }
                //}

                //if (isEdge)
                //{
                //    switch (whatEdge)
                //    {
                //        case "T":
                //            break;
                //        case "B":
                //            break;
                //        case "L":
                //            break;
                //        case "R":
                //            break;
                //        default:
                //            Console.Error.WriteLine("Error in program.");
                //            break;
                //    }

            }

            grid.WriteGridToConsole(grid);
            Console.ReadLine();

            Console.WriteLine("\nNow we check for numbers that are adjacent horizontally or vertically\nwith one space between them. These cells must be river cells.");

            // First loop over rows, and the first four columns to check horizontal pairs
            int iRow;
            int iCol;
            for (iRow = 0; iRow < grid.Height; iRow++)
            {
                for (iCol = 0; iCol < grid.Width - 2; iCol++)
                {
                    if (grid.Cells[iRow, iCol].CellType == Grid.CellType.Number && grid.Cells[iRow, iCol+2].CellType == Grid.CellType.Number)
                    {
                        grid.Cells[iRow, iCol + 1].CellType = Grid.CellType.River;
                    }
                }
            }

            // Now do the same for all columns, and the first four rows to check vertical pairs
            for (iCol = 0; iCol < grid.Width; iCol++)
            {
                for (iRow = 0; iRow < grid.Height - 2; iRow++)
                {
                    if (grid.Cells[iRow, iCol].CellType == Grid.CellType.Number && grid.Cells[iRow + 2, iCol].CellType == Grid.CellType.Number)
                    {
                        grid.Cells[iRow + 1, iCol].CellType = Grid.CellType.River;
                    }
                }
            }

            grid.WriteGridToConsole(grid);

            Console.ReadLine();

            grid.CheckKnownCellsNeighbours(grid);

            grid.WriteGridToConsole(grid);

             Console.ReadLine();            
        }

        
    }
}
