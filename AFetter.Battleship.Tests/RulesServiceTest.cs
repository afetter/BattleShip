using AFetter.Battleship.Domain.Entities;
using AFetter.Battleship.Domain.Enums;
using AFetter.Battleship.Service;
using NUnit.Framework;

namespace AFetter.Battleship.Tests
{
    public class RulesServiceTest
    {
        private RulesService _rulesService;
        private Player player;
        private Board board;
        [SetUp]
        public void Setup()
        {
            _rulesService = new RulesService();

            player = new Player { Name = "AFetter" };

            board = new Board(player);
        }

        [Test]
        public void Add_Vessel_At_Last_Column_Horizontally_Must_Return_Error()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 10), OrientationType.Horizontal);

            var result = _rulesService.ValidateVesselPositionOnBoard(board, vessel);

            Assert.IsFalse(result);
        }

        [Test]
        public void Add_Vessel_At_Last_Row_Vertically_Must_Return_Error()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(10, 1), OrientationType.Vertical);

            var result = _rulesService.ValidateVesselPositionOnBoard(board, vessel);

            Assert.IsFalse(result);
        }

        [Test]
        public void Access_Board_Permission_Denied()
        {
            var result = _rulesService.HasBoardPermission(board, player);

            Assert.IsTrue(result);
        }

        [Test]
        public void Access_Different_Board_Permission_Denied()
        {
            var result = _rulesService.HasBoardPermission(board, new Player());

            Assert.IsFalse(result);
        }
    }
}
