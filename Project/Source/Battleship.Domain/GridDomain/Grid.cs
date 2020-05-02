using System;
using Battleship.Domain.GridDomain.Contracts;

namespace Battleship.Domain.GridDomain
{
    public class Grid : IGrid
    {
        public IGridSquare[,] Squares { get; }

        public int Size { get; }

        public Grid(int size)
        {
            this.Squares = new IGridSquare[size, size];
            this.Size = size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.Squares[i, j] = new GridSquare(new GridCoordinate(i, j));
                }
            }
        }

        public IGridSquare GetSquareAt(GridCoordinate coordinate)
        {
            return this.Squares[coordinate.Row, coordinate.Column];
        }

        public IGridSquare Shoot(GridCoordinate coordinate)
        {
            throw new NotImplementedException("Shoot method of Grid class is not implemented");
        }
    }
}