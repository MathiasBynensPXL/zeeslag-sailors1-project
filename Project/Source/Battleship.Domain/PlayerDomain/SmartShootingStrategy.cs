using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        private Grid shootingGrid;
        private List<GridCoordinate> targets = new List<GridCoordinate>();
        private List<ShotResult> shotResults = new List<ShotResult>();

        public SmartShootingStrategy(GameSettings settings, IGrid opponentGrid)
        {
            this.opponentGrid = opponentGrid;
            this.settings = settings;
            this.shootingGrid = new Grid(settings.GridSize);
        }

        public GridCoordinate DetermineTargetCoordinate()
        {
            Random random = new Random();
            
            bool allSquaresHit = true;
            foreach (IGridSquare square in opponentGrid.Squares)
            {
                if (square.Status == GridSquareStatus.Untouched)
                {
                    allSquaresHit = false;
                    break;
                }
            }
            if (allSquaresHit)
            {
                throw new ApplicationException("All Squares are hit!");
            }
            bool shouldBeRandom = true;
            if (targets.Count != 0 && shotResults[shotResults.Count - 1].Hit)
            {
                shouldBeRandom = false;
                if (targets.Count > 1 && shotResults[shotResults.Count - 2].Hit)
                {
                    Direction correctDirection = Direction.FromCoordinates(targets[targets.Count - 2], targets[targets.Count - 1]);
                    GridCoordinate next = targets[targets.Count - 1].GetNeighbor(correctDirection);
                    if (targets[targets.Count - 2] == next)
                    {
                        correctDirection = Direction.FromCoordinates(targets[targets.Count - 1], targets[targets.Count - 2]);
                        next = targets[targets.Count - 1].GetNeighbor(correctDirection);
                    }
                    if (next.IsOutOfBounds(opponentGrid.Size))
                    {
                        correctDirection = Direction.FromCoordinates(targets[targets.Count - 1], targets[targets.Count - 2]);
                        next = targets[targets.Count - 2].GetNeighbor(correctDirection);
                    }
                    if (!next.IsOutOfBounds(opponentGrid.Size))
                    {
                        return next;
                    }
                }
                else
                {
                    Direction randomDirection = Direction.CreateRandomly();
                    GridCoordinate next = targets[targets.Count - 1].GetNeighbor(randomDirection);
                    while (next.IsOutOfBounds(opponentGrid.Size)) {
                        randomDirection = Direction.CreateRandomly();
                        next = targets[targets.Count - 1].GetNeighbor(randomDirection);
                    }
                    if (!next.IsOutOfBounds(opponentGrid.Size))
                    {
                        return next;
                    }
                        
                }
            }
            
            if (targets.Count == 0 || shouldBeRandom)
            {
                bool isNotHit = true;
                GridCoordinate randomTarget = new GridCoordinate(random.Next(this.opponentGrid.Size), random.Next(this.opponentGrid.Size));
                    while (isNotHit)
                    {
                        IGridSquare vierkantje = opponentGrid.GetSquareAt(randomTarget);
                        isNotHit = this.opponentGrid.Squares[randomTarget.Row, randomTarget.Column].Status != GridSquareStatus.Untouched;
                        if (isNotHit)
                        {
                            randomTarget = new GridCoordinate(random.Next(this.opponentGrid.Size), random.Next(this.opponentGrid.Size));
                        }
                    }
                    return randomTarget;
            }
            throw new ApplicationException("something went wrong");
        }

        public void RegisterShotResult(GridCoordinate target, ShotResult shotResult)
        { 
            shotResults.Add(shotResult);
            targets.Add(target);
            if (shotResult.Hit) {
                shootingGrid.Squares[target.Column, target.Row].Status = GridSquareStatus.Hit;
            } else
            {
                shootingGrid.Squares[target.Column, target.Row].Status = GridSquareStatus.Miss;
            }
        }
    }
}