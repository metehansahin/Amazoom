using System;
using System.Collections.Generic;
using System.Text;

namespace Amazoom
{
    public class TileNode
    {
        private WarehouseGrid<TileNode> grid;
        public int xLocation;
        public int yLocation;
        private int id;

        //for pathfinding 
        public int gCost;
        public int hCost;
        public int fCost;
        public TileNode previousNode;
        public bool isFree; //checks for robot in the way


        //for warehouse layout
        public Rack rack  {get; private set; }
        public LoadingDock loadingDock  {get; private set; }
        public int tileType { get; private set; } // 0=Isle, 1=Loading Dock, 2= left rack, 3= right rack

        private int rackSide; //0= access on left side, 1= right, -1 = unapplicable
        private int numOfShelves; //0 = if not a rack
        public bool isWalkable; //true if tileType <=1, false otherwise, set once at initialization 
        private char gridSymbol;

        private const char isleSymbol = '.';
        private const char loadingDockSymbol = 'D';
        private const char rackSymbol = '=';
        private const char blankSymbol = ' ';


        public TileNode(WarehouseGrid<TileNode> grid, int xLocation, int yLocation, int tileType, int numOfShelves, int id)
        {
            this.id = id;
            this.grid = grid;
            this.xLocation = xLocation;
            this.yLocation = yLocation;
            this.tileType = tileType;
            this.numOfShelves = numOfShelves;
            isFree = true; //no robot in the way

            this.rackSide = -1;
            //isle
            if (tileType == 0)
            {
                isWalkable = true;
                gridSymbol = isleSymbol;

            }
            //loading dock
            else if (tileType == 1)
            {
                isWalkable = true;
                gridSymbol = loadingDockSymbol;
                loadingDock = new LoadingDock(xLocation, yLocation, id) ;

            }
            //left rack
            else if (tileType == 2 || tileType == 3)
            {
                isWalkable = false;
                gridSymbol = rackSymbol;
                this.rackSide = tileType - 2; //left: 2-2=0 , right: 3-2= 1 
                rack = new Rack(numOfShelves, id);

            }
            //the wall
            else if (tileType == 4)
            {
                isWalkable = false;
                gridSymbol = blankSymbol;

            }

        }


        public char getGridSymbol()
        {
            return gridSymbol;
        }
        public void setGridSymbol(char symbol)
        {
            gridSymbol = symbol;
        }

        //PathFinding Methods
        public void CalculateFCost()
        {
            fCost = gCost + hCost;

        }

        public void SetIsWalkable(bool isWalkable)
        {

            this.isWalkable = isWalkable;

        }
        public override string ToString()
        {
            return xLocation + "," + yLocation;
        }
    }

}
