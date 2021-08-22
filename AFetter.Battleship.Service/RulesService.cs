using System.Linq;
using AFetter.Battleship.Domain.Entities;
using AFetter.Battleship.Domain.Interfaces;

namespace AFetter.Battleship.Service
{
    /// <summary>
    /// Responsible for all rules of the game
    /// </summary>
    public class RulesService: IRulesService
    {

        public bool ValidateVesselPositionOnBoard(Board board, Vessel vessel)
        {
            var lastColPanelCoordinates = board.Panels
                .First(n => n.Coordinates.Column == 10 && n.Coordinates.Row == 1)
                .Coordinates;

            var lastRowPanelCoordinates = board.Panels.Last().Coordinates;
            if (ValidateVesselOnColumnEdges(lastColPanelCoordinates, vessel) ||
                ValidateVesselOnRowEdges(lastRowPanelCoordinates, vessel))
            {
                return false;
            }

            return true;
        }

        private bool ValidateVesselOnColumnEdges(Coordinate coordinates,
            Vessel vessel)
        {
            if (vessel.StartingPosition.Column > coordinates.Column ||
                (vessel.StartingPosition.Column == coordinates.Column
                && vessel.Orientation == Domain.Enums.OrientationType.Horizontal) ||
                vessel.Positions.Last().Column > coordinates.Column)
            {
                return false;
            }

            return true;
        }

        private bool ValidateVesselOnRowEdges(Coordinate coordinates,
            Vessel vessel)
        {
            if (vessel.StartingPosition.Row > coordinates.Row ||
                (vessel.StartingPosition.Row == coordinates.Row
                && vessel.Orientation == Domain.Enums.OrientationType.Vertical) ||
                vessel.Positions.Last().Row > coordinates.Row)
            {
                return false;
            }

            return true;
        }

        public bool HasBoardPermission(Board board, Player player)
        {
            return board.Player == player;
        }


    }
}
