using AFetter.Battleship.Domain.Entities;

namespace AFetter.Battleship.Domain.Interfaces
{
    public interface IRulesService
    {
        bool HasBoardPermission(Board board, Player player);
        bool ValidateVesselPositionOnBoard(Board board, Vessel vessel);
    }
}
