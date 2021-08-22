using System.Collections.Generic;
using System.Linq;
using AFetter.Battleship.Domain;
using AFetter.Battleship.Domain.Entities;
using AFetter.Battleship.Domain.Interfaces;

namespace AFetter.Battleship.Service
{
    /// <summary>
    /// Responsible for action on Board game
    /// </summary>
    public class BoardService : IBoardService
    {
        private readonly IRulesService _rulesService;

        public BoardService(IRulesService rulesService)
        {
            _rulesService = rulesService;
        }

        public ResponseEnvelope<bool> AddVessel(Board board, Vessel vessel,
            Player player)
        {

            if (!_rulesService.HasBoardPermission(board,player))
            {
                return new ResponseEnvelope<bool>().AddError("Access denied");
            }

            if (!_rulesService.ValidateVesselPositionOnBoard(board, vessel))
            {
                return new ResponseEnvelope<bool>().AddError("Invalid position");
            }

            var panelToOccupy = GetPanelsBasedOnVesselPosition(board, vessel);

            if (panelToOccupy.Any(n => n.IsOccupade))
            {
                return new ResponseEnvelope<bool>().AddError("Position not available");
            }

            panelToOccupy.ToList().ForEach(n => n.IsOccupade = true);
            board.Vessels.Add(vessel);

            return new ResponseEnvelope<bool>(result: true);

        }

        public bool TakeAnAttack(Board board, Coordinate coordinate)
        {
            var attack = board.Vessels.FirstOrDefault(v =>
                v.Positions.Any(p => p.Column == coordinate.Column &&
                p.Row == coordinate.Row));

            if (attack is null)
            {
                return false;
            }
            
            attack.Hits += 1;
            return true;
            
        }

        public bool IsGameOver(Board board)
        {
            return board.Vessels.All(n => n.IsSink);
        }

        private IEnumerable<Panel> GetPanelsBasedOnVesselPosition(Board board, Vessel vessel)
        {
            return board.Panels.Where(n =>
                vessel.Positions.Any(v => v.Column == n.Coordinates.Column
                && v.Row == n.Coordinates.Row));
        }

    }
}
