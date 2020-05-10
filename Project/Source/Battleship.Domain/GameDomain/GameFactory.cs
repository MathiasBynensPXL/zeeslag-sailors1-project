using System;
using Battleship.Domain.GameDomain.Contracts;
using Battleship.Domain.PlayerDomain.Contracts;
using Battleship.Domain.GridDomain;
using Battleship.Domain.PlayerDomain;

namespace Battleship.Domain.GameDomain
{
    public class GameFactory : IGameFactory
    {

        public IGame CreateNewSinglePlayerGame(GameSettings settings, User user)
        {
            GameSettings gameSettings = new GameSettings();
            HumanPlayer player = new HumanPlayer(user, settings);
            ComputerPlayer computer = new ComputerPlayer(settings, new RandomShootingStrategy(settings, player.Grid));
            return this.CreateNewTwoPlayerGame(settings, player, computer);
        }

        public IGame CreateNewTwoPlayerGame(GameSettings settings, IPlayer player1, IPlayer player2)
        {
            return new Game(settings, player1, player2);
        }
    }
}