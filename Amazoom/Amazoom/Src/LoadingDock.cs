using System;
namespace Amazoom
{
    public class LoadingDock
    {
        private bool isFree;
        private int locationX;
        private int locationY;
        public int id { get; private set; }
        public LoadingDock()
        {
            isFree = true;
            locationX = 0;
            locationY = 0;
        }
        public LoadingDock(int xLocation, int yLocation, int id)
        {
            isFree = true;
            this.id = id;
            locationX = xLocation;
            locationY = yLocation;
        }

        public void setStatus(bool isFree)
        {
            this.isFree = isFree;
        }

        public bool getStatus()
        {
            return isFree;
        }
    }
}
