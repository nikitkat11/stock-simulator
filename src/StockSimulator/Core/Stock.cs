namespace StockSimulator.Core;

public class Stock
{
    public required string Symbol { get; set; }  // e.g., "AAPL"
    public decimal Price { get; set; }
    public decimal ChangePercent { get; set; }
<<<<<<< HEAD
    public int Quantity { get; set; }
=======
>>>>>>> 2cc3c76825d0b790eacad3bdb8c2c041fbd0ef35

    public void UpdatePrice(Random rng)
    {
        // IMPLEMENT: 
        // 1. Generate a random change between -5% and +5%
        // 2. Update Price and ChangePercent
        // 3. (Advanced) Cap changes during "market hours"
        // Random price change (-5% to +5%)
<<<<<<< HEAD
        decimal change = (decimal)(rng.NextDouble() * 0.1 - 0.05);
=======
        decimal change = (decimal)(rng.NextDouble() * 0.1m - 0.05m);
>>>>>>> 2cc3c76825d0b790eacad3bdb8c2c041fbd0ef35
        Price *= 1 + change;
        ChangePercent = change * 100;
    }
}