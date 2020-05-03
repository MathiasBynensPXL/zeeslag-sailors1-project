using System;
using System.Collections.Generic;
using Battleship.Domain.FleetDomain.Contracts;
using Battleship.Domain.GridDomain;
using Battleship.Domain.GridDomain.Contracts;

namespace Battleship.Domain.FleetDomain
{
    public class Fleet : IFleet
    {

        private Dictionary<ShipKind, IShip> _Dictionary = new Dictionary<ShipKind, IShip>();
        
        public Fleet()
        {
            _Dictionary.Add(ShipKind.Carrier, new Ship(ShipKind.Carrier));
            _Dictionary.Add(ShipKind.Battleship, new Ship(ShipKind.Battleship));
            _Dictionary.Add(ShipKind.Destroyer, new Ship(ShipKind.Destroyer));
            _Dictionary.Add(ShipKind.Submarine, new Ship(ShipKind.Submarine));
            _Dictionary.Add(ShipKind.PatrolBoat, new Ship(ShipKind.PatrolBoat));
        }

        public bool IsPositionedOnGrid { get; }

        public Result TryMoveShipTo(ShipKind kind, GridCoordinate[] segmentCoordinates, IGrid grid)
        {
            throw new NotImplementedException("TryMoveShipTo method of Fleet class is not implemented");
        }

        public void RandomlyPositionOnGrid(IGrid grid, bool allowDeformedShips = false)
        {
            throw new NotImplementedException("RandomlyPositionOnGrid method of Fleet class is not implemented");
        }

        public IShip FindShipAtCoordinate(GridCoordinate coordinate)
        {
            throw new NotImplementedException("FindShipAtCoordinate method of Fleet class is not implemented");
        }

        public IList<IShip> GetAllShips()
        {
            List<IShip> ships = new List<IShip>();
            ships.AddRange(_Dictionary.Values);
            return ships;
        }

        public IList<IShip> GetSunkenShips()
        {
            throw new NotImplementedException("GetSunkenShips method of Fleet class is not implemented");
        }
    }
}