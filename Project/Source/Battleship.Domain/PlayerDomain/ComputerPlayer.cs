using System;
using Battleship.Domain.GameDomain;
using Battleship.Domain.GridDomain;
using Battleship.Domain.PlayerDomain.Contracts;
using Battleship.Domain.GameDomain;

namespace Battleship.Domain.PlayerDomain
{
    public class ComputerPlayer : PlayerBase
    {
        private IShootingStrategy shootingStrategy;

        public ComputerPlayer(GameSettings settings, IShootingStrategy shootingStrategy) : base(Guid.NewGuid(), "Computer", settings)
        {
            this.shootingStrategy = shootingStrategy;
            this.Fleet.RandomlyPositionOnGrid(this.Grid, settings.AllowDeformedShips);
            
        }

        public void ShootAutomatically(IPlayer opponent)
        {
            GridCoordinate target = this.shootingStrategy.DetermineTargetCoordinate();
            this.shootingStrategy.RegisterShotResult(target, this.ShootAt(opponent, target));
        }
    }
}