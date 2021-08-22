using System.Collections.Generic;

namespace AFetter.Battleship.Domain.Entities
{
    public class CarriersVessel: Vessel
    {
        public CarriersVessel()
        {
            Size = 5;
            Type = Enums.VesselType.Carriers;
            Positions = new List<Coordinate>(Size);
        }
    }
}
