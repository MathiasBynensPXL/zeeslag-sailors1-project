using System;
using System.Collections.Generic;
using Battleship.Business.Models.Contracts;
using Battleship.Domain.FleetDomain.Contracts;

namespace Battleship.Business.Models
{
    public class ShipInfoFactory : IShipInfoFactory 
    {
        public IList<IShipInfo> CreateMultipleFromFleet(IFleet fleet)
        {
            IList<IShip> ships = fleet.GetAllShips();
            List<IShipInfo> shipInfo = new List<IShipInfo>();
            if (ships != null)
            {
                for (int i = 0; i < ships.Count; i++)
                {
                    shipInfo.Add(new ShipInfo(ships[i]));
                }
            }
            return shipInfo;

        }

        public IList<IShipInfo> CreateMultipleFromSunkenShipsOfFleet(IFleet fleet)
        {
            IList<IShip> ships = fleet.GetSunkenShips();
            List<IShipInfo> shipInfo = new List<IShipInfo>();
            if (ships != null)
            {
                for (int i = 0; i < ships.Count; i++)
                {
                    shipInfo.Add(new ShipInfo(ships[i]));
                }
            }
            return shipInfo;
        }
    }
}