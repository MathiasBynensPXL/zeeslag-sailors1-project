using System;
using Battleship.Business.Models.Contracts;
using Battleship.Domain.GridDomain.Contracts;

namespace Battleship.Business.Models
{
    public class GridInfoFactory : IGridInfoFactory
    {
        public IGridInfo CreateFromGrid(IGrid grid)
        {
            GridInfo gridInfo = new GridInfo();
            gridInfo.Size = grid.Size;
            GridSquareInfo[][] squareInfos = new GridSquareInfo[gridInfo.Size][];
            for (int i = 0; i < gridInfo.Size; i++)
            {
                squareInfos[i] = new GridSquareInfo[gridInfo.Size];
                for (int j = 0; j < gridInfo.Size; j++)
                {
                    squareInfos[i][j] = new GridSquareInfo(grid.Squares[i, j]);
                }
            }
            gridInfo.Squares = squareInfos;
            return gridInfo;
        }
    }
}