using System;
using System.Collections.Generic;

namespace Amazoom
{
    public class PathFinding
    {
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;


        private WarehouseGrid<TileNode> grid;
        private List<TileNode> toSearchList;
        private List<TileNode> searchedList;
        private List<TileNode> tempSearchedList; //for tiles that were temporaily unwalkable
        private bool reachedEnd;
        public PathFinding(WarehouseGrid<TileNode> warehouseLayout)
        {

            grid = warehouseLayout;
            reachedEnd = false;
        }

        public WarehouseGrid<TileNode> GetGrid()
        {
            return grid;
        }

        public List<TileNode> FindPath(int startX, int startY, int endX, int endY)
        {

            TileNode startNode = grid.GetGridObject(startX, startY);
            TileNode endNode = grid.GetGridObject(endX, endY);
            reachedEnd = startNode == endNode;
            if (!endNode.isFree && !reachedEnd)
            {
                List<TileNode> neighbourList = GetNeighbourList(endNode);


                foreach (TileNode neighbourNode in neighbourList)
                {
                    if (neighbourNode.isFree && neighbourNode.isWalkable)
                    {
                        endNode = neighbourNode;

                    }
                }
            }

            if (startNode == null || endNode == null)
            {
                // Invalid Path
                return null;
            }

            toSearchList = new List<TileNode> { startNode };
            searchedList = new List<TileNode>();
            tempSearchedList = new List<TileNode>();



            for (int x = 0; x < grid.GetNumCols(); x++)
            {
                for (int y = 0; y < grid.GetNumRows(); y++)
                {
                    TileNode pathNode = grid.GetGridObject(x, y);
                    if (pathNode == null)
                    {
                        //may occur for loading dock line
                        continue;
                    }
                    pathNode.gCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.previousNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (toSearchList.Count > 0)
            {
                TileNode currentNode = GetLowestFCostNode(toSearchList);
                if (currentNode == endNode)
                {
                    //reached final Node
                    reachedEnd = true;
                    return CalculatePath(endNode);
                }
                toSearchList.Remove(currentNode);
                searchedList.Add(currentNode);

                List<TileNode> neighbourList = GetNeighbourList(currentNode);


                foreach (TileNode neighbourNode in neighbourList)
                {

                    if (neighbourNode == null)
                    {
                        continue;
                    }
                    if (searchedList.Contains(neighbourNode))
                    {
                        continue;
                    }
                    if (!neighbourNode.isWalkable)
                    {
                        searchedList.Add(neighbourNode);
                        continue;
                    }
                    if (!neighbourNode.isFree)
                    {
                        tempSearchedList.Add(neighbourNode);
                        searchedList.Add(neighbourNode);
                        continue;
                    }
                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    //do we have a faster path than what was calculated before now htat we moved to a new position
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.previousNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!toSearchList.Contains(neighbourNode))
                        {
                            toSearchList.Add(neighbourNode);

                        }
                    }

                }

                foreach (TileNode tempNode in tempSearchedList)
                {
                    if (searchedList.Contains(tempNode))
                    {
                        searchedList.Remove(tempNode);
                    }
                    tempNode.isFree = true;
                }
                tempSearchedList.Clear();

            }
            //out of nodes on the openList
            return null;
        }
        public TileNode GetNode(int x, int y)
        {
            return grid.GetGridObject(x, y);
        }
        private List<TileNode> CalculatePath(TileNode endNode)
        {
            List<TileNode> path = new List<TileNode>();
            path.Add(endNode);
            TileNode currentNode = endNode;
            while (currentNode.previousNode != null)
            {
                path.Add(currentNode.previousNode);
                currentNode = currentNode.previousNode;
            }
            path.Reverse();
            return path;
        }
        public int CalculateDistanceCost(TileNode a, TileNode b)
        {
            int xDistance = Math.Abs(a.xLocation - b.xLocation);
            int yDistance = Math.Abs(a.yLocation - b.yLocation);
            int remaining = Math.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Math.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;

        }

        private List<TileNode> GetNeighbourList(TileNode currentNode)
        {
            List<TileNode> neighbourList = new List<TileNode>();

            if (currentNode.xLocation - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.xLocation - 1, currentNode.yLocation));
                if (currentNode.yLocation - 1 >= 0)
                {
                    neighbourList.Add(GetNode(currentNode.xLocation - 1, currentNode.yLocation - 1));
                }
                if (currentNode.yLocation + 1 < grid.GetNumRows())
                {
                    neighbourList.Add(GetNode(currentNode.xLocation - 1, currentNode.yLocation + 1));
                }

            }

            if (currentNode.xLocation + 1 < grid.GetNumCols())
            {
                neighbourList.Add(GetNode(currentNode.xLocation + 1, currentNode.yLocation));
                if (currentNode.yLocation - 1 >= 0)
                {
                    neighbourList.Add(GetNode(currentNode.xLocation + 1, currentNode.yLocation - 1));
                }
                if (currentNode.yLocation + 1 < grid.GetNumRows())
                {
                    neighbourList.Add(GetNode(currentNode.xLocation + 1, currentNode.yLocation + 1));
                }

            }
            if (currentNode.yLocation - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.xLocation, currentNode.yLocation - 1));
            }
            if (currentNode.yLocation + 1 < grid.GetNumRows())
            {
                neighbourList.Add(GetNode(currentNode.xLocation, currentNode.yLocation + 1));
            }

            return neighbourList;
        }

        private TileNode GetLowestFCostNode(List<TileNode> pathNodeList)
        {
            TileNode lowestFCostNode = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                {
                    lowestFCostNode = pathNodeList[i];
                }
            }
            return lowestFCostNode;
        }

    }
}
