using BattleshipsGame.Core.Contract;

namespace BattleshipsGameConsole.Player.Contract;

public interface IPlayerInputProvider
{
    Cell? TryGetTargetCell();
}