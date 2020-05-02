using System;

namespace Battleship.Domain.GridDomain
{
    public class GridCoordinate
    {
        public int Row { get; }
        public int Column { get; }

        public GridCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static GridCoordinate CreateRandom(int gridSize)
        {
            Random random = new Random();
            int column = random.Next(0, gridSize);
            int row = random.Next(0, gridSize);
            return new GridCoordinate(row, column);
        }

        public bool IsOutOfBounds(int gridSize)
        {
            if (Row < 0 || Row >= gridSize || Column >= gridSize || Column < 0)
            {
                return true;
            } return false;
        }

        public GridCoordinate GetNeighbor(Direction direction)
        {
            return new GridCoordinate(Row + direction.YStep, Column + direction.XStep);
        }

        public GridCoordinate GetOtherEnd(Direction direction, int distance)
        {
            return new GridCoordinate(Row + direction.YStep * distance, Column + direction.XStep * distance);
        }

        #region overrides
        //DO NOT TOUCH THIS METHODS IN THIS REGION!

        public override string ToString()
        {
            return $"({Row},{Column})";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GridCoordinate);
        }

        protected bool Equals(GridCoordinate other)
        {
            if (ReferenceEquals(other, null)) return false;
            return Row == other.Row && Column == other.Column;
        }

        public static bool operator ==(GridCoordinate a, GridCoordinate b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(GridCoordinate a, GridCoordinate b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        #endregion
    }

}