using StockSimulator.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSimulator.UI
{
    public class ConsoleInterface
    {
        private readonly Portfolio _portfolio;
        private readonly SimulationEngine _simulationEngine;
        private CancellationTokenSource _cancellationTokenSource;

        public ConsoleInterface(Portfolio portfolio, SimulationEngine simulationEngine)
        {
            _portfolio = portfolio;
            _simulationEngine = simulationEngine;
        }

        public async Task StartAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var simulationTask = _simulationEngine.StartAsync(_cancellationTokenSource.Token);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Stock Simulator ===");
                Console.WriteLine("1. View Portfolio");
                Console.WriteLine("2. Buy Stock");
                Console.WriteLine("3. Sell Stock");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ViewPortfolio();
                        break;
                    case "2":
                        BuyStock();
                        break;
                    case "3":
                        SellStock();
                        break;
                    case "4":
                        _cancellationTokenSource.Cancel();
                        await simulationTask;
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ViewPortfolio()
        {
            Console.Clear();
            Console.WriteLine("=== Portfolio ===");
            Console.WriteLine($"Cash: {_portfolio.Cash:C}");
            Console.WriteLine("Stocks:");
            foreach (var stock in _portfolio.Stocks)
            {
                Console.WriteLine($"- {stock.Symbol}: {stock.Quantity} shares @ {stock.Price:C} each");
            }
            Console.WriteLine($"Total Value: {_portfolio.GetTotalValue():C}");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        private void BuyStock()
        {
            Console.Clear();
            Console.WriteLine("=== Buy Stock ===");
            Console.Write("Enter stock symbol: ");
            var symbol = Console.ReadLine()?.ToUpper();
            Console.Write("Enter price: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Invalid price. Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }
            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out var quantity))
            {
                Console.WriteLine("Invalid quantity. Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            try
            {
                _portfolio.BuyStock(symbol, price, quantity, _simulationEngine);
                Console.WriteLine("Stock purchased successfully! Press any key to return to the menu...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to return to the menu...");
            }
            Console.ReadKey();
        }

        private void SellStock()
        {
            Console.Clear();
            Console.WriteLine("=== Sell Stock ===");
            Console.Write("Enter stock symbol: ");
            var symbol = Console.ReadLine()?.ToUpper();
            Console.Write("Enter quantity: ");
            if (!int.TryParse(Console.ReadLine(), out var quantity))
            {
                Console.WriteLine("Invalid quantity. Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            try
            {
                _portfolio.SellStock(symbol, quantity);
                Console.WriteLine("Stock sold successfully! Press any key to return to the menu...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Press any key to return to the menu...");
            }
            Console.ReadKey();
        }
    }
}