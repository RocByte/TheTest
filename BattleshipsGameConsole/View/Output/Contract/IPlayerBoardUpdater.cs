using BattleshipsGame.Core.Contract;

namespace BattleshipsGameConsole.View.Output.Contract;

public interface IPlayerBoardUpdater
{
    PlayerBoard Board { get; }
    
    void InitializeBoardState();
    void SetState(Cell cell, ShotResult shotResult);
}