﻿using System;
using Battleship.Business.Models.Contracts;
using Battleship.Domain.GameDomain.Contracts;
using Battleship.Domain.PlayerDomain.Contracts;
using System.Collections.Generic;
using Battleship.Domain.FleetDomain.Contracts;
using Battleship.Domain.FleetDomain;
using Battleship.Business.Models;

namespace Battleship.Business.Models
{
    public class GameInfoFactory : IGameInfoFactory
    {
        IGridInfoFactory _gridInfoFactory;
        IShipInfoFactory _shipInfoFactory;
        public GameInfoFactory(IGridInfoFactory gridInfoFactory, IShipInfoFactory shipInfoFactory)
        {
            this._gridInfoFactory = gridInfoFactory;
            this._shipInfoFactory = shipInfoFactory;
        }

        public IGameInfo CreateFromGame(IGame game, Guid playerId)
        {
            GameInfo gameInfo = new GameInfo();
            gameInfo.Id = game.Id;
            IPlayer player = game.GetPlayerById(playerId);
            IPlayer oppenent = game.GetOpponent(player);
 
            gameInfo.HasBombsLoaded = player.HasBombsLoaded;
            gameInfo.OwnGrid = _gridInfoFactory.CreateFromGrid(player.Grid);
            gameInfo.OpponentGrid = _gridInfoFactory.CreateFromGrid(oppenent.Grid);
            gameInfo.OwnShips = _shipInfoFactory.CreateMultipleFromFleet(player.Fleet);
            gameInfo.SunkenOpponentShips = _shipInfoFactory.CreateMultipleFromSunkenShipsOfFleet(oppenent.Fleet);
            gameInfo.IsReadyToStart = game.IsStarted;
            return gameInfo;
        }


        
        
    }
}