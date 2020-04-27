using System;

namespace Battleship.Domain.GridDomain
{
    public static class GridCoordinateArrayExtensions
    {
        public static bool HasAnyOutOfBounds(this GridCoordinate[] coordinates, int gridSize)
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i].Row < 0 || coordinates[i].Row > gridSize || coordinates[i].Column > gridSize || coordinates[i].Column < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AreAligned(this GridCoordinate[] coordinates)
        {
            throw new NotImplementedException("AreAligned method of GridCoordinateArrayExtensions class is not implemented");
        }

        public static bool AreHorizontallyAligned(this GridCoordinate[] coordinates)
        {
            throw new NotImplementedException("AreHorizontallyAligned method of GridCoordinateArrayExtensions class is not implemented");
        }

        public static bool AreVerticallyAligned(this GridCoordinate[] coordinates)
        {
            throw new NotImplementedException("AreVerticallyAligned method of GridCoordinateArrayExtensions class is not implemented");
        }

        public static bool AreLinked(this GridCoordinate[] coordinates)
        {
            throw new NotImplementedException("AreLinked method of GridCoordinateArrayExtensions class is not implemented");
        }

        public static string Print(this GridCoordinate[] coordinates)
        {
            return $"[{string.Join<GridCoordinate>(", ", coordinates)}]";
        }
    }
}