using System;
using Battleship.Domain.GameDomain.Contracts;
using Battleship.Domain.GridDomain;
using Battleship.Domain.PlayerDomain.Contracts;

namespace Battleship.Domain.GameDomain
{
    public class Game : IGame
    {
        public Guid Id { get; }
        public GameSettings Settings { get; }

        public IPlayer Player1 { get; }
        public IPlayer Player2 { get; }
        public bool IsStarted { get; private set; }

        internal Game(GameSettings settings, IPlayer player1, IPlayer player2)
        {
            this.Settings = settings;
            this.Player1 = player1;
            this.Player2 = player2;
            this.Id = Guid.NewGuid();
        }

        public Result Start()
        {
            throw new NotImplementedException("Start method of Game class is not implemented");
        }

        public ShotResult ShootAtOpponent(Guid shooterPlayerId, GridCoordinate coordinate)
        {
            throw new NotImplementedException("ShootAtOpponent method of Game class is not implemented");
        }

        public IPlayer GetPlayerById(Guid playerId)
        {
            if (playerId == this.Player1.Id)
            {
                return this.Player1;
            } 
            else
            {
                return this.Player2;
            }
           
        }

        public IPlayer GetOpponent(IPlayer player)
        {
            if (player == this.Player1) 
            {
                return this.Player2;
            } 
            else 
            {
                return this.Player1;
            }
        }
    }
}