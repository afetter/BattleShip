using System;
namespace AFetter.Battleship.Domain.Entities
{
    public class Player
    {
        public Guid PlayerId => Guid.NewGuid();
        public string Name { get; set; }
    }
}
