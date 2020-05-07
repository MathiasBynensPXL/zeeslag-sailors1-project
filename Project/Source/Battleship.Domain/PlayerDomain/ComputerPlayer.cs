using System;
using Battleship.Domain.GameDomain;
using Battleship.Domain.GridDomain;
using Battleship.Domain.PlayerDomain.Contracts;
using Battleship.Domain.GameDomain;

namespace Battleship.Domain.PlayerDomain
{
    public class ComputerPlayer : PlayerBase
    {

        public ComputerPlayer(GameSettings settings, IShootingStrategy shootingStrategy) : base(Guid.NewGuid(), "Computer", settings)
        {

            this.Fleet.RandomlyPositionOnGrid(new Grid(settings.GridSize), settings.AllowDeformedShips);
            
        }

        public void ShootAutomatically(IPlayer opponent)
        {
            throw new NotImplementedException("ShootAutomatically method of ComputerPlayer class is not implemented");
        }
    }
}