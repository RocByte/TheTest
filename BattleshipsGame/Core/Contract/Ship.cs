namespace BattleshipsGame.Core.Contract;

public class Ship
{
    private int health;
    
    public IList<Cell> Cells { get; }

    public bool Alive => health > 0;
    
    public Ship(IList<Cell> cells)
    {
        health = cells.Count;
        Cells = cells;
    }
    
    public void RegisterHit()
    {
        if (health > 0)
            health--;
    }
}