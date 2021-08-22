using System;
using System.Collections.Generic;
using AFetter.Battleship.Domain.Enums;

namespace AFetter.Battleship.Domain.Entities
{
    public abstract class Vessel
    {
        public Guid Id => Guid.NewGuid();
        public VesselType Type { get; set; }
        public int Size { get; set; }
        public OrientationType Orientation { get; set; }
        
        public IList<Coordinate> Positions { get; internal set; }
        public Coordinate StartingPosition { get; set; }
        public int Hits { get; set; }

        public bool IsSink => Hits == Size;

        public void SetPostion(Coordinate startingPosition, OrientationType orientation)
        {
            Orientation = orientation;
            Positions = new List<Coordinate>(Size);
            StartingPosition = startingPosition;
            if (orientation == OrientationType.Vertical)
            {
                for (int i = 0; i < Size; i++)
                {
                    Positions.Add(new Coordinate(startingPosition.Row + i, startingPosition.Column));
                }
            }
            else if (orientation == OrientationType.Horizontal)
            {
                for (int i = 0; i < Size; i++)
                {
                    Positions.Add(new Coordinate(startingPosition.Row, startingPosition.Column + i));
                }
            }
        }

    }
}
