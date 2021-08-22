using System.Collections.Generic;

namespace AFetter.Battleship.Domain.Entities
{
    public class Board
    {
        public Board(Player player)
        {
            Panels = LoadPanels();
            Vessels = new List<Vessel>();
            Player = player;
        }
        public Player Player { get; set; }
        public IList<Panel> Panels { get; set; }
        public IList<Vessel> Vessels { get; set; }

        private IList<Panel> LoadPanels()
        {
            var result = new List<Panel>();
            for (int row = 1; row <= 10; row++)
            {
                for (int col = 1; col <= 10; col++)
                {
                    result.Add(new Panel
                    {
                        Coordinates = new Coordinate(row, col)
                    });
                }
            }
            return result;
        }
    }
}
