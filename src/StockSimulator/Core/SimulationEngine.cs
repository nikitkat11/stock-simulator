namespace StockSimulator.Core;

public class SimulationEngine
{
    private readonly List<Stock> _stocks;
    private readonly Random _rng = new();
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public IReadOnlyList<Stock> Stocks => _stocks.AsReadOnly();

    public SimulationEngine()
    {
        // Initialize with sample stocks
        // TODO: Load from config file/API

        _stocks = new List<Stock>
        {
            new Stock { Symbol = "AAPL", Price = 182.50m },
            new Stock { Symbol = "MSFT", Price = 412.10m },
            new Stock { Symbol = "GOOGL", Price = 2800.00m },
            new Stock { Symbol = "AMZN", Price = 3400.00m },
            new Stock { Symbol = "TSLA", Price = 700.00m },
            new Stock { Symbol = "NFLX", Price = 550.00m },
            new Stock { Symbol = "FB", Price = 350.00m },
            new Stock { Symbol = "NVDA", Price = 220.00m }
        };
    }

    /// <summary>
    /// Starts the price simulation loop.
    /// TODO: Add market open/close times
    /// </summary>
    public async Task StartAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            // Update each stock's price
            _stocks.ForEach(s => s.UpdatePrice(_rng));

            // Trigger "market events" occasionally
            if (_rng.Next(0, 100) < 10) // 10% chance of a market event
            {
                TriggerMarketEvent();
            }

            await Task.Delay(2000, ct); // Update every 2 seconds
        }
    }

    public void DisplayStocks()
    {
        Console.WriteLine("Stock Updates:");
        Console.WriteLine("--------------------------------------------------");
        foreach (var stock in _stocks)
        {
            Console.WriteLine($"Symbol: {stock.Symbol}, Price: {stock.Price:C}, Change: {stock.ChangePercent:F2}%");
        }
        Console.WriteLine("--------------------------------------------------");
    }

    public void AddStock(Stock stock)
    {
        if (!_stocks.Any(s => s.Symbol == stock.Symbol))
        {
            _stocks.Add(stock);
        }
    }

    private void TriggerMarketEvent()
    {
        var eventType = _rng.Next(0, 3);
        switch (eventType)
        {
            case 0:
                Console.WriteLine("Market Crash! All stocks drop by 10%.");
                _stocks.ForEach(s => s.Price *= 0.9m);
                break;
            case 1:
                Console.WriteLine("Market Boom! All stocks rise by 10%.");
                _stocks.ForEach(s => s.Price *= 1.1m);
                break;
            case 2:
                Console.WriteLine("Random news event! Random stock price change.");
                var randomStock = _stocks[_rng.Next(_stocks.Count)];
                randomStock.UpdatePrice(_rng);
                break;
        }
    }
}