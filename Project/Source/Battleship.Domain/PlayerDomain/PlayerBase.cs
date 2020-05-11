using System;
using Battleship.Domain.FleetDomain.Contracts;
using Battleship.Domain.GameDomain;
using Battleship.Domain.GridDomain;
using Battleship.Domain.GridDomain.Contracts;
using Battleship.Domain.PlayerDomain.Contracts;
using Battleship.Domain.FleetDomain;

namespace Battleship.Domain.PlayerDomain
{
    public abstract class PlayerBase : IPlayer
    {
        public Guid Id { get; }
        public string NickName { get; }
        public IGrid Grid { get; }
        public IFleet Fleet { get; }

        private bool _HasBombsLoaded;

        public bool HasBombsLoaded { get { return _HasBombsLoaded; } }

        protected PlayerBase(Guid id, string nickName, GameSettings gameSettings)
        {
            this.Id = id;
            this.NickName = nickName;
            this.Grid = new Grid(gameSettings.GridSize);
            this.Fleet = new Fleet();
        }

        public void ReloadBombs()
        {
            this._HasBombsLoaded = true;
        }

        public ShotResult ShootAt(IPlayer opponent, GridCoordinate coordinate)
        {
            if (HasBombsLoaded == true)
            {
                this._HasBombsLoaded = false;
                IGridSquare square = opponent.Grid.Shoot(coordinate);
                opponent.ReloadBombs();
                if (square.Status == GridSquareStatus.Hit)
                {
                    return ShotResult.CreateHit(opponent.Fleet.FindShipAtCoordinate(coordinate));
                }
                if (square.Status == GridSquareStatus.Miss)
                {
                    return ShotResult.CreateMissed();
                }
                return ShotResult.CreateMissed();
            } else
            {
                return ShotResult.CreateMisfire("You have no bombs loaded");
            }
        }
    }
}