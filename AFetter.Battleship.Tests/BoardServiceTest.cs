using AFetter.Battleship.Domain.Entities;
using AFetter.Battleship.Domain.Enums;
using AFetter.Battleship.Domain.Interfaces;
using AFetter.Battleship.Service;
using Moq;
using NUnit.Framework;

namespace AFetter.Battleship.Tests
{
    public class BoardServiceTest
    {
        private BoardService _boardService;
        private Player player;
        private Board board;
        private Mock<IRulesService> _rulesService;

        [SetUp]
        public void Setup()
        {
            _rulesService = new Mock<IRulesService>();

            _rulesService.Setup(s =>
                s.HasBoardPermission(It.IsAny<Board>(), It.IsAny<Player>()))
                .Returns(true);

            _rulesService.Setup(s =>
                s.ValidateVesselPositionOnBoard(
                    It.IsAny<Board>(), It.IsAny<Vessel>()))
                .Returns(true);

            _boardService = new BoardService(_rulesService.Object);
            player = new Player { Name = "AFetter" };
            board = new Board(player);

        }

        

        [Test]
        public void Add_Multiple_Vessel_At_Same_Position_Must_Return_Error()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);
            _boardService.AddVessel(board, vessel, player);

            var vessel2 = new CarriersVessel();
            vessel2.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);

            var result = _boardService.AddVessel(board, vessel2, player);

            Assert.IsTrue(result.HasError);
        }

        [Test]
        public void Add_Vessel_At_Corret_Position_Must_Return_Ok()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);

            var result = _boardService.AddVessel(board, vessel, player);

            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void Add_Multiple_Vessel_At_Corret_Position_Must_Return_Ok()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);

            var vessel2 = new CarriersVessel();
            vessel2.SetPostion(new Coordinate(5, 5), OrientationType.Vertical);

            var result = _boardService.AddVessel(board, vessel2, player);

            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void Take_An_Attack_Miss()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);

            _boardService.AddVessel(board, vessel, player);
            var result = _boardService.TakeAnAttack(board, new Coordinate(10, 10));

            Assert.IsFalse(result);
        }

        [Test]
        public void Take_An_Attack_Hit()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);

            _boardService.AddVessel(board, vessel, player);
            var result = _boardService.TakeAnAttack(board, new Coordinate(1, 1));

            Assert.IsTrue(result);
        }

        [Test]
        public void Is_GameOver_False()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);
            _boardService.AddVessel(board, vessel, player);

            var result =_boardService.IsGameOver(board);

            Assert.IsFalse(result);
        }


        [Test]
        public void Is_GameOver_True()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);
            vessel.Hits = vessel.Size;

            _boardService.AddVessel(board, vessel, player);
            var result = _boardService.IsGameOver(board);

            Assert.IsTrue(result);
        }

        [Test]
        public void FullGame_Sink_All_Vessels()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);

            _boardService.AddVessel(board, vessel, player);

            _boardService.TakeAnAttack(board, new Coordinate(1, 1));
            _boardService.TakeAnAttack(board, new Coordinate(2, 1));
            _boardService.TakeAnAttack(board, new Coordinate(3, 1));
            _boardService.TakeAnAttack(board, new Coordinate(4, 1));
            _boardService.TakeAnAttack(board, new Coordinate(5, 1));

            Assert.IsTrue(vessel.IsSink);

            var result = _boardService.IsGameOver(board);

            Assert.IsTrue(result);
        }

        [Test]
        public void FullGame_Sink_One_Of_Two_Vessels()
        {
            var vessel = new CarriersVessel();
            vessel.SetPostion(new Coordinate(1, 1), OrientationType.Vertical);

            _boardService.AddVessel(board, vessel, player);

            var vessel2 = new CarriersVessel();
            vessel2.SetPostion(new Coordinate(6, 5), OrientationType.Horizontal);

            _boardService.AddVessel(board, vessel2, player);

            _boardService.TakeAnAttack(board, new Coordinate(1, 1));
            _boardService.TakeAnAttack(board, new Coordinate(2, 1));
            _boardService.TakeAnAttack(board, new Coordinate(3, 1));
            _boardService.TakeAnAttack(board, new Coordinate(4, 1));
            _boardService.TakeAnAttack(board, new Coordinate(5, 1));

            Assert.AreEqual(5, vessel.Hits);

            Assert.IsTrue(vessel.IsSink);

            Assert.IsFalse(vessel2.IsSink);

            var result = _boardService.IsGameOver(board);

            Assert.IsFalse(result);
        }

    }
}
