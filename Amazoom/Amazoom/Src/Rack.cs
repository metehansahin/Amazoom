using System;
namespace Amazoom
{
    public class Rack
    {
        private int shelf_levels;
        private Shelf[] shelves;
        private const int shelfWeightCapacity = 500;
        public int rackId { get; private set; }

        // Rack constructors
        public Rack(int shelf_levels, int rackId)
        {
            this.rackId = rackId;
            this.shelf_levels = shelf_levels;
            this.shelves = new Shelf[shelf_levels];
            for(int i=0; i<shelf_levels; i++)
            {
                shelves[i] = new Shelf(i, shelfWeightCapacity, rackId* shelf_levels + i);
            }
        }
        public Rack()
        {
            this.shelf_levels = 1;
            this.shelves = new Shelf[shelf_levels];
            shelves[0] = new Shelf(0, shelfWeightCapacity,0);
        }

        // getters and setters
        // For how to use them: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo.getaccessors?view=net-6.0
        public int Shelf_levels { get => shelf_levels; set => shelf_levels = value; }
        public Shelf[] Shelves { get => shelves; set => shelves = value; }
    }
}
