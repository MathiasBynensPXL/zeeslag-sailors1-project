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
            if (!GridCoordinateArrayExtensions.AreAligned(segmentCoordinates))
            {
                return Result.CreateFailureResult("Coordinates are not aligned!");
            }
            if (!GridCoordinateArrayExtensions.AreLinked(segmentCoordinates))
            {
                return Result.CreateFailureResult("Coordinates are not linked!");
            }
            if (segmentCoordinates.Length != kind.Size)
            {
                return Result.CreateFailureResult("SegmentCoordinates do not match length of ShipKind!");
            } 
            if (segmentCoordinates.HasAnyOutOfBounds(grid.Size))
            {
                return Result.CreateFailureResult("1 or More Coordinate(s) are outside of the grid!");
            } 
            foreach (IShip ship in _Dictionary.Values)
            {
                for (int i = 0; i < segmentCoordinates.Length; i++)
                {
                    if (ship.CanBeFoundAtCoordinate(segmentCoordinates[i]))
                    {
                        return Result.CreateFailureResult("1 or more Coordinate(s) collide with another ship");
                    }
                }
            }
            foreach(IShip ship in _Dictionary.Values) {
                if (ship.Kind == kind)
                {
                    IGridSquare[] converted = ConvertGridCoordinateToGridSquare(segmentCoordinates);
                    ship.PositionOnGrid(converted);
                }
            }
            for (int i = 0; i < segmentCoordinates.Length; i++)
            {

                grid.GetSquareAt(segmentCoordinates[i]);
            }
            return Result.CreateSuccessResult();
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

        private IGridSquare[] ConvertGridCoordinateToGridSquare(GridCoordinate[] gridCoordinates)
        {
            GridSquare[] gridSquare = new GridSquare[gridCoordinates.Length];
            for (int i = 0; i < gridCoordinates.Length; i++)
            {
                gridSquare[i] = new GridSquare(gridCoordinates[i]);
            }
            return gridSquare;
        }

    }
}