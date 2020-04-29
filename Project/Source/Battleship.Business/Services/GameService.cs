using System;
using Battleship.Business.Models.Contracts;
using Battleship.Business.Services.Contracts;
using Battleship.Domain;
using Battleship.Domain.FleetDomain;
using Battleship.Domain.GameDomain;
using Battleship.Domain.GameDomain.Contracts;
using Battleship.Domain.GridDomain;

namespace Battleship.Business.Services
{
    
    public class GameService : IGameService
    {

        private IGameInfoFactory _gameInfoFactory;
        private IGameFactory _gameFactory;
        private IGameRepository _gameRepository;

        public GameService(
            IGameFactory gameFactory,
            IGameRepository gameRepository, 
            IGameInfoFactory gameInfoFactory)
        {
            _gameInfoFactory = gameInfoFactory;
            _gameFactory = gameFactory;
            _gameRepository = gameRepository;
        }

        public IGameInfo CreateGameForUser(GameSettings settings, User user)
        {
            IGame game = _gameFactory.CreateNewSinglePlayerGame(settings, user);
            IGame repository = _gameRepository.Add(game);
            return _gameInfoFactory.CreateFromGame(game, user.Id);
        }

        public Result StartGame(Guid gameId, Guid playerId)
        {
            throw new NotImplementedException("StartGame method of GameService class is not implemented");
        }

        public IGameInfo GetGameInfoForPlayer(Guid gameId, Guid playerId)
        {

            throw new NotImplementedException("GetGameInfoForPlayer method of GameService class is not implemented");
        }

        public Result PositionShipOnGrid(Guid gameId, Guid playerId, ShipKind shipKind, GridCoordinate[] segmentCoordinates)
        {
            throw new NotImplementedException("PositionShipOnGrid method of GameService class is not implemented");
        }

        public ShotResult ShootAtOpponent(Guid gameId, Guid shooterPlayerId, GridCoordinate coordinate)
        {
            throw new NotImplementedException("ShootAtOpponent method of GameService class is not implemented");
        }
    }
}