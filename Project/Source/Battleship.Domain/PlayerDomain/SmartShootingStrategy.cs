using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using Battleship.Domain.FleetDomain;
using Battleship.Domain.FleetDomain.Contracts;
using Battleship.Domain.GameDomain;
using Battleship.Domain.GridDomain;
using Battleship.Domain.GridDomain.Contracts;
using Battleship.Domain.PlayerDomain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Domain.PlayerDomain
{
    public class SmartShootingStrategy : IShootingStrategy
    {
        private GameSettings settings;
        private IGrid opponentGrid;
        private Direction[] possibleDirections;
        private List<GridCoordinate> untouched, missed, hitted, candidates;
        private IList<IShip> ships;

        public SmartShootingStrategy(GameSettings settings, IGrid opponentGrid)
        {
            this.opponentGrid = opponentGrid;
            this.settings = settings;
            
            this.ships = new Fleet().GetAllShips();
            if (settings.AllowDeformedShips)
            {
                this.possibleDirections = Direction.AllDirections;
            }
            else
            {
                this.possibleDirections = Direction.BasicDirections;
            }
            untouched = new List<GridCoordinate>();
            foreach (IGridSquare square in opponentGrid.Squares)
            {
                untouched.Add(square.Coordinate);
            }
            hitted = new List<GridCoordinate>();
            missed = new List<GridCoordinate>();
            candidates = new List<GridCoordinate>();
        }


        private List<GridCoordinate> GetNeighbours(GridCoordinate coordinate, Direction[] possibleDirections, GridSquareStatus withStatus)
        {
            List<GridCoordinate> result = new List<GridCoordinate>();
            foreach (Direction direction in possibleDirections)
            {
                GridCoordinate otherCoordinate = coordinate.GetNeighbor(direction);
                if (!otherCoordinate.IsOutOfBounds(this.opponentGrid.Size))
                {
                    IGridSquare otherSquare = this.opponentGrid.GetSquareAt(otherCoordinate);

                    if (otherSquare.Status == withStatus)
                    {
                        result.Add(otherCoordinate);
                    }
                }
            }
            return result;
        }

        private int GetSmallestShipSize()
        {
            int result = this.opponentGrid.Size;
            foreach (IShip ship in this.ships)
            {
                if (result > ship.Kind.Size)
                {
                    result = ship.Kind.Size;
                }
            }
            return result;
        }

        private bool isPossibleShip(GridCoordinate coordinate)
        {
            int numberUntouched = 0;
            foreach (Direction direction in this.possibleDirections)
            {
                int numberUntouchedDirection = 1;
                GridCoordinate nextCoordinate = coordinate.GetNeighbor(direction);
                while (!nextCoordinate.IsOutOfBounds(this.opponentGrid.Size))
                {
                    if (opponentGrid.GetSquareAt(nextCoordinate).Status == GridSquareStatus.Untouched)
                    {
                        numberUntouchedDirection++;
                        nextCoordinate = nextCoordinate.GetNeighbor(direction);
                    } else
                    {
                        break;
                    }
                }
                nextCoordinate = coordinate.GetNeighbor(direction.Opposite);
                while (!nextCoordinate.IsOutOfBounds(this.opponentGrid.Size))
                {
                    if (opponentGrid.GetSquareAt(nextCoordinate).Status == GridSquareStatus.Untouched)
                    {
                        numberUntouchedDirection++;
                        nextCoordinate = nextCoordinate.GetNeighbor(direction.Opposite);
                    }
                    else
                    {
                        break;
                    }
                }
                if (numberUntouchedDirection > numberUntouched)
                {
                    numberUntouched = numberUntouchedDirection;
                }
            }
            
            if (numberUntouched < this.GetSmallestShipSize())
            {
                return false;
            }

            return true;
        }

        public GridCoordinate DetermineTargetCoordinate()
        {
            Random random = new Random();
            
            if (this.GetSmallestShipSize() == 2)
            {
                foreach (GridCoordinate hitCoordinate in hitted)
                {
                    List<GridCoordinate> hittedNeighbours = this.GetNeighbours(hitCoordinate, this.possibleDirections, GridSquareStatus.Hit);
                    if (hittedNeighbours.Count > 0)
                    {
                        foreach (GridCoordinate neighourCoordinate in hittedNeighbours)
                        {
                            Direction direction = Direction.FromCoordinates(hitCoordinate, neighourCoordinate);

                            List<GridCoordinate> nextHitted = this.GetNeighbours(neighourCoordinate, new Direction[] { direction }, GridSquareStatus.Hit);
                            if (nextHitted.Count == 0)
                            {
                                List<GridCoordinate> untouchedCoordinates = this.GetNeighbours(neighourCoordinate, new Direction[] { direction }, GridSquareStatus.Untouched);
                                if (untouchedCoordinates.Count > 0)
                                {
                                    return untouchedCoordinates.First();
                                }
                            }
                        }
                    }
                }
                
            }
            
            foreach (GridCoordinate candidate in candidates.ToList())
            {
                if (opponentGrid.GetSquareAt(candidate).Status != GridSquareStatus.Untouched)
                {
                    candidates.Remove(candidate);
                }
            }

            foreach (GridCoordinate untouch in untouched.ToList())
            {

                if (opponentGrid.GetSquareAt(untouch).Status != GridSquareStatus.Untouched)
                {
                    untouched.Remove(untouch);
                }
            }

            foreach (GridCoordinate candidate in candidates)
            {
                if (this.isPossibleShip(candidate))
                {
                    return candidate;
                }
            }

            List<GridCoordinate> randomUntouched = untouched.ToList();
            while (true)
            { 
                GridCoordinate candidate = randomUntouched.ElementAt(random.Next(randomUntouched.Count));
                if (this.isPossibleShip(candidate))
                {
                    return candidate;
                }
                randomUntouched.Remove(candidate);
                if (randomUntouched.Count == 0)
                {
                    break;
                }
            }

            if (candidates.Count > 0)
            {
                return candidates.First();
            }

            if (untouched.Count > 0) 
            {
                return untouched.ElementAt(random.Next(untouched.Count));
            }

            throw new ApplicationException("something went wrong");
            
        }

        public void RegisterShotResult(GridCoordinate target, ShotResult shotResult)
        { 
            
            if (shotResult.Hit) {
                hitted.Add(target);
                candidates.AddRange(this.GetNeighbours(target, this.possibleDirections, GridSquareStatus.Untouched));
            } else
            {
                missed.Add(target);
            }
            untouched.Remove(target);
            candidates.Remove(target);
            
            if (shotResult.SunkenShip != null)
            {
               
                foreach (IShip ship in this.ships.ToList())
                {
                    if (ship.Kind == shotResult.SunkenShip.Kind)
                    {
                        this.ships.Remove(ship);
                    }
                }

                
                foreach (IGridSquare square in shotResult.SunkenShip.Squares)
                {
                    List<GridCoordinate> neighbours = this.GetNeighbours(square.Coordinate, this.possibleDirections, GridSquareStatus.Untouched);
                    foreach (GridCoordinate neighbour in neighbours)
                    {
                        candidates.Remove(neighbour);
                    }               
                }
            }
        }
    }
}