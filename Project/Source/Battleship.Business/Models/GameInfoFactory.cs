using System;
using Battleship.Business.Models.Contracts;
using Battleship.Domain.GameDomain.Contracts;
using Battleship.Domain.PlayerDomain.Contracts;
using System.Collections.Generic;
using Battleship.Domain.FleetDomain.Contracts;

namespace Battleship.Business.Models
{
    public class GameInfoFactory : IGameInfoFactory
    {
        public GameInfoFactory(IGridInfoFactory gridInfoFactory, IShipInfoFactory shipInfoFactory)
        {

        }

        public IGameInfo CreateFromGame(IGame game, Guid playerId)
        {
            GameInfo gameInfo = new GameInfo();
            gameInfo.Id = game.Id;
            IPlayer player;
            IPlayer oppenent;
            if (playerId == game.Player1.Id)
            {
                player = game.Player1;
                oppenent = game.Player2;
            } else if (playerId == game.Player2.Id)
            {
                player = game.Player2;
                oppenent = game.Player1;
            } else
            {
                return gameInfo;
            }
            gameInfo.HasBombsLoaded = player.HasBombsLoaded;
            gameInfo.OwnGrid = ConvertGridToGridInfo(player);
            gameInfo.OpponentGrid = ConvertGridToGridInfo(oppenent);
            gameInfo.OwnShips = ConvertShipToShipInfo(player.Fleet.GetAllShips());  //nog
            gameInfo.SunkenOpponentShips = ConvertShipToShipInfo(oppenent.Fleet.GetSunkenShips()); //nog
            gameInfo.IsReadyToStart = game.IsStarted;
            return gameInfo;
        }

        private GridInfo ConvertGridToGridInfo(IPlayer player)
        {
            GridInfo gridInfo = new GridInfo();
            gridInfo.Size = player.Grid.Size;
            GridSquareInfo[][] squareInfos = new GridSquareInfo[gridInfo.Size][];
            for (int i = 0; i < gridInfo.Size; i++)
            {
                squareInfos[i] = new GridSquareInfo[gridInfo.Size];
                for (int j = 0; j < gridInfo.Size; j++)
                {
                    squareInfos[i][j] = new GridSquareInfo(player.Grid.Squares[i, j]);
                }
            }
            gridInfo.Squares = squareInfos;
            return gridInfo;
        }

        private IList<IShipInfo> ConvertShipToShipInfo(IList<IShip> ships) 
        {
            List<IShipInfo> shipInfo = new List<IShipInfo>();
            foreach (IShip boat in ships)
            {
                shipInfo.Add(new ShipInfo(boat));
            }
            return shipInfo;
        }
        
    }
}