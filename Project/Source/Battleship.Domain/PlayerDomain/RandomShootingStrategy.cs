using System;
using System.Data;
using Battleship.Domain.GameDomain;
using Battleship.Domain.GridDomain;
using Battleship.Domain.GridDomain.Contracts;
using Battleship.Domain.PlayerDomain.Contracts;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.ObjectPool;

namespace Battleship.Domain.PlayerDomain
{
    public class RandomShootingStrategy : IShootingStrategy
    {
        private IGrid opponentGrid;
        public RandomShootingStrategy(GameSettings settings, IGrid opponentGrid)
        {
            this.opponentGrid = opponentGrid;
            //The GameSettings parameter will only be needed when you implement certain extra's. But you must leave it. Otherwise some tests will not compile...
        }

        public GridCoordinate DetermineTargetCoordinate()
        {
            ApplicationException applicationException = new ApplicationException("All Squares are hit!");
            Random random = new Random();
            bool isNotHit = true;
            GridCoordinate Target = new GridCoordinate(random.Next(this.opponentGrid.Size), random.Next(this.opponentGrid.Size));
            bool allSquaresHit = true;
            foreach (IGridSquare square in opponentGrid.Squares)
            {
                if (square.Status == GridSquareStatus.Untouched)
                {
                    allSquaresHit = false;
                    break;
                }
            }



            if (!allSquaresHit)
            {
                while (isNotHit)
                {
                    IGridSquare vierkantje = opponentGrid.GetSquareAt(Target);
                    isNotHit = this.opponentGrid.Squares[Target.Row, Target.Column].Status != GridSquareStatus.Untouched;
                    if (isNotHit)
                    {
                        Target = new GridCoordinate(random.Next(this.opponentGrid.Size), random.Next(this.opponentGrid.Size));
                    }
                }
                return Target;
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public void RegisterShotResult(GridCoordinate target, ShotResult shotResult)
        {
            //No need do do anything here. Smarter shooting strategies will care more about the result of a shot...
        }
    }
}