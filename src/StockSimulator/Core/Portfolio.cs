namespace StockSimulator.Core;

<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;

public class Portfolio
{
    public List<Stock> Stocks { get; set; } = new List<Stock>();
    public decimal Cash { get; set; }

    public void BuyStock(string symbol, decimal price, int quantity, SimulationEngine simulationEngine)
    {
        if (symbol != null)
        {
            var stock = simulationEngine.Stocks.FirstOrDefault(s => s.Symbol == symbol);
            if (stock == null)
            {
                stock = new Stock { Symbol = symbol, Price = price };
                simulationEngine.AddStock(stock); // Add stock to SimulationEngine
            }
            var portfolioStock = Stocks.FirstOrDefault(s => s.Symbol == symbol);
            if (portfolioStock == null)
            {
                portfolioStock = stock;
                Stocks.Add(portfolioStock);
            }
            portfolioStock.Quantity += quantity;
            Cash -= price * quantity;
        }
    }

    public void SellStock(string symbol, int quantity)
    {
        var stock = Stocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock != null && stock.Quantity >= quantity)
        {
            stock.Quantity -= quantity;
            Cash += stock.Price * quantity;
        }
    }

    public decimal GetTotalValue()
    {
        return Stocks.Sum(s => s.Price * s.Quantity) + Cash;
    }

    public void UpdateStockPrices(Random rng)
    {
        foreach (var stock in Stocks)
        {
            stock.UpdatePrice(rng);
        }
    }
}
=======
    Collections.Generic;
    using System.Linq;


    public class Portfolio
    {
        public List<Stock> Stocks { get; set; } = new List<Stock>();
        public decimal Cash { get; set; }

    public void BuyStock(string symbol, decimal price, int quantity, SimulationEngine simulationEngine)
    {
        var stock = simulationEngine.Stocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock == null)
        {
            stock = new Stock { Symbol = symbol, Price = price };
            simulationEngine.AddStock(stock); // Add stock to SimulationEngine
        }
        var portfolioStock = Stocks.FirstOrDefault(s => s.Symbol == symbol);
        if (portfolioStock == null)
        {
            portfolioStock = stock;
            Stocks.Add(portfolioStock);
        }
        portfolioStock.Quantity += quantity;
        Cash -= price * quantity;
    }
    public void SellStock(string symbol, int quantity)
        {
            var stock = Stocks.FirstOrDefault(s => s.Symbol == symbol);
            if (stock != null && stock.Quantity >= quantity)
            {
                stock.Quantity -= quantity;
                Cash += stock.Price * quantity;
            }
        }
        public decimal GetTotalValue()
        {
            return Stocks.Sum(s => s.Price * s.Quantity) + Cash;
        }
    public void UpdateStockPrices(Random rng)
        {
            foreach (var stock in Stocks)
            {
                stock.UpdatePrice(rng);
            }
        }
    }
>>>>>>> 2cc3c76825d0b790eacad3bdb8c2c041fbd0ef35
