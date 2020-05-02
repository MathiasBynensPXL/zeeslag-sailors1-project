using System;

namespace Battleship.Domain.GridDomain
{
    public static class GridCoordinateArrayExtensions
    {
        public static bool HasAnyOutOfBounds(this GridCoordinate[] coordinates, int gridSize)
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i].Row < 0 || coordinates[i].Row >= gridSize || coordinates[i].Column >= gridSize || coordinates[i].Column < 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool AreAligned(this GridCoordinate[] coordinates)
        {
           if (AreHorizontallyAligned(coordinates) || AreVerticallyAligned(coordinates))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static bool AreHorizontallyAligned(this GridCoordinate[] coordinates)
        {
            int row = coordinates[0].Row;
            for (int i = 1; i < coordinates.Length; i++)
            {
                if (row != coordinates[i].Row)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AreVerticallyAligned(this GridCoordinate[] coordinates)
        {
            int column = coordinates[0].Column;
            for (int i = 1; i < coordinates.Length; i++)
            {
                if (column != coordinates[i].Column)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsLinked(GridCoordinate first, GridCoordinate second)
        {
            if (Math.Abs(first.Row - second.Row) > 1 || Math.Abs(first.Column - second.Column) > 1)
            {
                return false;
            } else if (first.Row == second.Row && first.Column == second.Column)
            {
                return false;
            }
            return true;
        }

        public static bool AreLinked(this GridCoordinate[] coordinates)
        {
            for(int i = 1; i < coordinates.Length; i++)
            {
                if (!IsLinked(coordinates[i - 1], coordinates[i]))
                {
                    return false;
                } 
            }
            return true;
        }
            

        public static string Print(this GridCoordinate[] coordinates)
        {
            return $"[{string.Join<GridCoordinate>(", ", coordinates)}]";
        }
    }
}