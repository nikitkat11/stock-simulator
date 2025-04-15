using StockSimulator.Core;
using System.Text.Json;

namespace StockSimulator.Data
{
    public class JsonRepository
    {
        private readonly string _filePath;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Saves the portfolio data to a JSON file.
        /// </summary>
        public void SavePortfolio(Portfolio portfolio)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(portfolio, options);
            File.WriteAllText(_filePath, json);
        }

        /// <summary>
        /// Loads the portfolio data from a JSON file.
        /// </summary>
        public Portfolio LoadPortfolio()
        {
            if (!File.Exists(_filePath))
            {
                return new Portfolio { Cash = 10000 }; // Default portfolio with initial cash
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<Portfolio>(json) ?? new Portfolio { Cash = 10000 };
        }
    }
}