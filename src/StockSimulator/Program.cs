using StockSimulator.Core;

class Program
{
    static async Task Main(string[] args)
    {
        var simulationEngine = new SimulationEngine();
        var portfolio = new Portfolio { Cash = 10000m }; // Start with $10,000
        var cts = new CancellationTokenSource();

        // Start the stock updates in the background
        _ = Task.Run(async () =>
        {
            await simulationEngine.StartAsync(cts.Token);
        });

        // Continuously display stock updates and portfolio
        while (!cts.Token.IsCancellationRequested)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Stock Simulator!");
            Console.WriteLine("Press 'M' for Menu or 'Q' to Quit.");
            Console.WriteLine();

            // Display stock updates
            simulationEngine.DisplayStocks();

            // Display portfolio
            DisplayPortfolio(portfolio);

            // Check for user input
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;
                if (key == ConsoleKey.M)
                {
                    ShowMenu(portfolio, simulationEngine, cts);
                }
                else if (key == ConsoleKey.Q)
                {
                    cts.Cancel();
                }
            }

            await Task.Delay(500); // Refresh every 500ms
        }

        Console.WriteLine("Simulation stopped. Goodbye!");
    }

    static void ShowMenu(Portfolio portfolio, SimulationEngine simulationEngine, CancellationTokenSource cts)
    {
        while (!cts.Token.IsCancellationRequested)
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Buy Stock");
            Console.WriteLine("2. Sell Stock");
            Console.WriteLine("3. View Balance");
            Console.WriteLine("4. Return to Main Screen");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    BuyStock(portfolio, simulationEngine);
                    break;
                case "2":
                    SellStock(portfolio);
                    break;
                case "3":
                    DisplayBalance(portfolio);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }

    static void DisplayPortfolio(Portfolio portfolio)
    {
        Console.WriteLine("Your Portfolio:");
        Console.WriteLine("--------------------------------------------------");
        foreach (var stock in portfolio.Stocks)
        {
            Console.WriteLine($"Symbol: {stock.Symbol}, Quantity: {stock.Quantity}, Price: {stock.Price:C}, Total: {stock.Price * stock.Quantity:C}");
        }
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Total Portfolio Value: {portfolio.GetTotalValue():C}");
    }

    static void BuyStock(Portfolio portfolio, SimulationEngine simulationEngine)
    {
        Console.Clear();
        Console.WriteLine("Available Stocks:");
        Console.WriteLine("--------------------------------------------------");
        foreach (var stock in simulationEngine.Stocks)
        {
            Console.WriteLine($"Symbol: {stock.Symbol}, Price: {stock.Price:C}");
        }
        Console.WriteLine("--------------------------------------------------");

        Console.Write("Enter the stock symbol to buy: ");
        var symbol = Console.ReadLine()?.ToUpper();
        var stockToBuy = simulationEngine.Stocks.FirstOrDefault(s => s.Symbol == symbol);

        if (stockToBuy == null)
        {
            Console.WriteLine("Invalid stock symbol.");
            return;
        }

        Console.Write("Enter the quantity to buy: ");
        if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
        {
            var totalCost = stockToBuy.Price * quantity;
            if (portfolio.Cash >= totalCost)
            {
                if (!string.IsNullOrEmpty(symbol))
                {
                    portfolio.BuyStock(symbol, stockToBuy.Price, quantity, simulationEngine);
                }
                else
                {
                    Console.WriteLine("Invalid stock symbol.");
                }
                Console.WriteLine($"Successfully bought {quantity} shares of {symbol} for {totalCost:C}.");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        else
        {
            Console.WriteLine("Invalid quantity.");
        }
    }

    static void SellStock(Portfolio portfolio)
    {
        Console.Clear();
        Console.WriteLine("Your Portfolio:");
        Console.WriteLine("--------------------------------------------------");
        foreach (var stock in portfolio.Stocks)
        {
            Console.WriteLine($"Symbol: {stock.Symbol}, Quantity: {stock.Quantity}, Price: {stock.Price:C}");
        }
        Console.WriteLine("--------------------------------------------------");

        Console.Write("Enter the stock symbol to sell: ");
        var symbol = Console.ReadLine()?.ToUpper();
        var stockToSell = portfolio.Stocks.FirstOrDefault(s => s.Symbol == symbol);

        if (stockToSell == null)
        {
            Console.WriteLine("You do not own this stock.");
            return;
        }

        Console.Write("Enter the quantity to sell: ");
        if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
        {
            if (stockToSell.Quantity >= quantity)
            {
                portfolio.SellStock(symbol, quantity);
                Console.WriteLine($"Successfully sold {quantity} shares of {symbol} for {stockToSell.Price * quantity:C}.");
            }
            else
            {
                Console.WriteLine("You do not have enough shares to sell.");
            }
        }
        else
        {
            Console.WriteLine("Invalid quantity.");
        }
    }

    static void DisplayBalance(Portfolio portfolio)
    {
        Console.Clear();
        Console.WriteLine($"Your current balance is: {portfolio.Cash:C}");
    }
}
