using System;
using Battleship.Business.Models.Contracts;
using Battleship.Domain.FleetDomain;
using Battleship.Domain.FleetDomain.Contracts;
using Battleship.Domain.GridDomain;

namespace Battleship.Business.Models
{
    public class ShipInfo : IShipInfo
    {
        public GridCoordinate[] Coordinates { get; }

        public ShipKind Kind { get; }

        public bool HasSunk { get;}

        public ShipInfo(IShip ship)
        {
            this.Kind = ship.Kind;
            this.HasSunk = ship.HasSunk;
            if (ship.Squares != null)
            { 
                this.Coordinates = new GridCoordinate[ship.Squares.Length];
                if (ship.Squares.Length != 0)
                {
                    for (int i = 0; i < ship.Kind.Size; i++)
                    {

                        this.Coordinates[i] = ship.Squares[i].Coordinate;
                    }
                }
            }
        }
    }
}