using System;
using System.Runtime.InteropServices.ComTypes;
using Battleship.Domain.GameDomain.Contracts;
using Battleship.Domain.GridDomain;
using Battleship.Domain.PlayerDomain;
using Battleship.Domain.PlayerDomain.Contracts;
using Microsoft.Extensions.ObjectPool;

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
            if (Player1.Fleet.IsPositionedOnGrid == false || Player2.Fleet.IsPositionedOnGrid == false)
            {
                return Result.CreateFailureResult("Not all players their fleet are positioned");
            } else
            {
                Player1.ReloadBombs();
                IsStarted = true;
                return Result.CreateSuccessResult();
            }
        }

        public ShotResult ShootAtOpponent(Guid shooterPlayerId, GridCoordinate coordinate)
        {
            if (this.IsStarted == true)
            {
                IPlayer shooter = this.GetPlayerById(shooterPlayerId);
                IPlayer victim = this.GetOpponent(shooter);

                if (shooter.HasBombsLoaded)
                {
                    ShotResult result = shooter.ShootAt(victim, coordinate);
                    if (victim is ComputerPlayer)
                    {
                        ((ComputerPlayer)victim).ShootAutomatically(shooter);
                    }
                    return result;
                }
                else
                {
                    return ShotResult.CreateMisfire("Player has no bombs loaded");
                }
            } else
            {
                return ShotResult.CreateMisfire("Game has not begun");
            }
        }

        public IPlayer GetPlayerById(Guid playerId)
        {
            if (playerId == this.Player1.Id)
            {
                return this.Player1;
            } 
            else if (this.Player2.Id == playerId)
            {
                return this.Player2;
            } else
            {
                return null;
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