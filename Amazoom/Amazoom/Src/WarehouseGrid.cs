using System;
using System.Collections.Generic;
using System.Text;

namespace Amazoom
{
    public class WarehouseGrid<TGridObject>
    {
        private int numCols;
        private int numRows;
        private TGridObject[,] gridArray;
        public List<TGridObject> listOfRacks;
        public List<TGridObject> listOfLoadingDocks;

        public TGridObject[,] GridArray { get => gridArray; set => gridArray = value; }

        //contructor: assume same layout as in project handout
        //x%4:
        //0123
        //..........
        //..LR..LR..
        //..LR..LR..
        //..........
        //DDDD

        public WarehouseGrid(int numCols, int numRows, int numOfShelves, Func<WarehouseGrid<TGridObject>, int, int, int, int, TGridObject> createGridObject)
        {
            //tileType: 0=Isle, 1=Loading Dock, 2= left rack, 3= right rack
            int type = 0;

            this.numCols = numCols;
            this.numRows = numRows; //includes loading dock
            gridArray = new TGridObject[numCols, numRows];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 1; y < gridArray.GetLength(1) - 2; y++)
                {
                    //left rack
                    if (x % 4 == 2)
                    {
                        type = 2;
                        gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves);
                    }
                    //right rack
                    else if (x % 4 == 3)
                    {
                        //check if at last column. If true, set tile as isle instead of rack.
                        if (x == gridArray.GetLength(0) - 1)
                        {
                            type = 0;
                        }
                        else
                        {
                            type = 3;

                        }

                        gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves);
                    }
                    //isle
                    else
                    {
                        type = 0;
                        gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves);
                    }

                }
            }
            //top and bottom line
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                type = 0;
                int y = gridArray.GetLength(1) - 2;
                gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves);
                y = 0;
                gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves);
            }
            //loading docks line
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                if (x < 4)
                {
                    type = 1;
                }
                else
                {
                    type = 4;
                }
                int y = gridArray.GetLength(1) - 1;
                gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves);
            }
        }
        //constructor: import from text file (assume rectangular shape)
        // text symbole      type
        // . = isle,          0=Isle, 
        // L = left rack,     2= left rack
        // R = right rack,    3= right rack
        // D = loading dock,  1= Loading Dock
        public WarehouseGrid(string[] textFile, int numOfShelves, Func<WarehouseGrid<TGridObject>, int, int, int, int, int, TGridObject> createGridObject)
        {
            this.numCols = textFile[0].Length;
            this.numRows = textFile.Length;

            listOfRacks = new List<TGridObject>();
            listOfLoadingDocks = new List<TGridObject>();
            
            gridArray = new TGridObject[numCols, numRows];
            int type = 0;
            int y = 0; //line count
            int x = 0; // symbole count 
            foreach (string line in textFile)
            {
                x = 0;
                foreach (char symbol in line)
                {
                    switch (symbol)
                    {
                        case '.':
                            type = 0;
                            gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves, -1);
                            break;

                        case 'L':
                            type = 2;
                            gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves, listOfRacks.Count);
                            listOfRacks.Add(gridArray[x, y]);
                            break;

                        case 'R':
                            type = 3;
                            gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves, listOfRacks.Count);
                            listOfRacks.Add(gridArray[x, y]);
                            break;

                        case 'D':
                            type = 1;
                            gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves, listOfLoadingDocks.Count);
                            listOfLoadingDocks.Add(gridArray[x, y]);
                            break;
                        default:
                            type = 4;
                            gridArray[x, y] = createGridObject(this, x, y, type, numOfShelves, -1);
                            break;
                    }
                    x++;

                }
                y++;
            }
        }

        public void printGrid()
        {
            TileNode nodeToPrint;
            for (int y = 0; y < numRows; y++)
            {
                for (int x = 0; x < numCols; x++)
                {
                    nodeToPrint = (TileNode)gridArray.GetValue(x, y);
                    if (nodeToPrint == null)
                    {
                        continue;
                    }
                    Console.Write("{0}", nodeToPrint.getGridSymbol());
                }
                Console.Write("\n");
            }
        }
        public string printGridString()
        {
            string text = "";

            TileNode nodeToPrint;
            for (int y = 0; y < numRows; y++)
            {
                for (int x = 0; x < numCols; x++)
                {
                    nodeToPrint = (TileNode)gridArray.GetValue(x, y);
                    if (nodeToPrint == null)
                    {
                        continue;
                    }
                    text += nodeToPrint.getGridSymbol();
                }
                text += Environment.NewLine;
            }

            return (text);
        }

        public int GetNumCols()
        {
            return numCols;
        }

        public int GetNumRows()
        {
            return numRows;
        }

        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < numCols && y < numRows)
            {
                return gridArray[x, y];
            }
            else
            {
                return default(TGridObject);
            }
        }

        public void SetGridObject(int x, int y, TGridObject value)
        {
            if (x >= 0 && y >= 0 && x < numCols && y < numRows)
            {
                gridArray[x, y] = value;
            }
        }
    }
}

