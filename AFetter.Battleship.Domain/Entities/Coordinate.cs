namespace AFetter.Battleship.Domain.Entities
{
    public class Coordinate
    {
        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Column { get; set; }
        public int Row { get; set; }
    }
}
