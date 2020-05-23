using System;
using Battleship.Business.Models.Contracts;
using Battleship.Business.Services.Contracts;
using Battleship.Domain;
using Battleship.Domain.FleetDomain;
using Battleship.Domain.GameDomain;
using Battleship.Domain.GameDomain.Contracts;
using Battleship.Domain.GridDomain;
using Battleship.Domain.PlayerDomain.Contracts;

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
            IGame game = _gameRepository.GetById(gameId);
            if (game.GetPlayerById(playerId) != null)
            {
                return game.Start();
            } else
            {
                return Result.CreateFailureResult("Is not a valid player!");
            }
        }

        public IGameInfo GetGameInfoForPlayer(Guid gameId, Guid playerId)
        {
            IGame game = _gameRepository.GetById(gameId);
            return _gameInfoFactory.CreateFromGame(game, playerId);
        }

        public Result PositionShipOnGrid(Guid gameId, Guid playerId, ShipKind shipKind, GridCoordinate[] segmentCoordinates)
        {
            IGame game = _gameRepository.GetById(gameId);
            IPlayer speler = game.GetPlayerById(playerId);
            return speler.Fleet.TryMoveShipTo(shipKind, segmentCoordinates, speler.Grid );
            
        }

        public ShotResult ShootAtOpponent(Guid gameId, Guid shooterPlayerId, GridCoordinate coordinate)
        {
            IGame game = _gameRepository.GetById(gameId);
            return game.ShootAtOpponent(shooterPlayerId, coordinate);
        }
    }
}