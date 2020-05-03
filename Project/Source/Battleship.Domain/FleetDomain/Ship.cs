﻿using System;
using Battleship.Domain.FleetDomain.Contracts;
using Battleship.Domain.GridDomain;
using Battleship.Domain.GridDomain.Contracts;

namespace Battleship.Domain.FleetDomain
{
    public class Ship : IShip
    {
        private IGridSquare[] _Squares;
        public IGridSquare[] Squares { get { return this._Squares; } }

        public ShipKind Kind { get; }

        public bool HasSunk { get; }

        public Ship(ShipKind kind)
        {
            this.Kind = kind;
            
        }

        public void PositionOnGrid(IGridSquare[] squares)
        {
            this._Squares = squares;
            //for (int i = 0;  i < this.Squares.Length; i++)
            //{
            //    this.Squares[i].HitByBombHandler(this.Squares[i]);
            //}
        }

        public bool CanBeFoundAtCoordinate(GridCoordinate coordinate)
        {
            if (this.Squares != null)
            {
                for (int i = 0; i < this.Squares.Length; i++)
                {
                    if (this.Squares[i].Coordinate == coordinate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}