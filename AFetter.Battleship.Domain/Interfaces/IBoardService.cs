using AFetter.Battleship.Domain.Entities;

namespace AFetter.Battleship.Domain.Interfaces
{
    public interface IBoardService
    {
        bool TakeAnAttack(Board board, Coordinate coordinate);
        ResponseEnvelope<bool> AddVessel(Board board, Vessel vessel, Player player);
    }
}
