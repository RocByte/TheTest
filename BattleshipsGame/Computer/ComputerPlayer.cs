using BattleshipsGame.Computer.Contract;
using BattleshipsGame.Core;

namespace BattleshipsGame.Computer;

public class ComputerPlayer
{
    private readonly ShipPlacementRules shipPlacementRules;
    private readonly IRandomShipGenerator randomShipGenerator;
    private readonly IList<int> initialShipSizes;
    private readonly int maxShipGenerationAttempts;

    public ComputerPlayer(
        IRandomShipGenerator randomShipGenerator,
        ShipPlacementRules shipPlacementRules,
        IList<int> initialShipSizes,
        int maxShipGenerationAttempts)
    {
        this.shipPlacementRules = shipPlacementRules;
        this.randomShipGenerator = randomShipGenerator;
        this.initialShipSizes = initialShipSizes;
        this.maxShipGenerationAttempts = maxShipGenerationAttempts;
    }

    public void PlaceShips(ShipRegister shipRegister)
    {
        foreach (var shipSize in initialShipSizes)
        {
            PlaceShip(shipSize, shipRegister);
        }
    }
    
    private void PlaceShip(int shipLenght, ShipRegister shipRegister)
    {
        var shipAdded = false;
        var currentIterationNumber = 0;
        do
        {
            ThrowExceptionIfToManyIterations(shipLenght, ++currentIterationNumber);
            
            var randomShip = randomShipGenerator.GetRandomShip(
                maxFirstCellIndex: shipPlacementRules.SquareGridSize - 1,
                shipLenght);

            if (shipPlacementRules.CanAddShip(randomShip, shipRegister.AllShips))
            {
                shipRegister.AddShip(randomShip);
                shipAdded = true;
            }
            
        } while (shipAdded == false);
    }

    private void ThrowExceptionIfToManyIterations(
        int shipLenght,
        int currentIterationNumber)
    {
        if (currentIterationNumber > maxShipGenerationAttempts)
        {
            throw new ArgumentOutOfRangeException(
                nameof(shipLenght),
                $"Couldn't place ship of lenght {shipLenght} using {maxShipGenerationAttempts} iterations");
        }
    }
}