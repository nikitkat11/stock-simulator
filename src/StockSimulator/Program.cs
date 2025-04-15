using StockSimulator.Core;
using StockSimulator.Data;
using StockSimulator.UI;

var filePath = "portfolio.json";
var jsonRepository = new JsonRepository(filePath);

// Load the portfolio
var portfolio = jsonRepository.LoadPortfolio();
var simulationEngine = new SimulationEngine();
var consoleInterface = new ConsoleInterface(portfolio, simulationEngine);

// Start the console interface
await consoleInterface.StartAsync();

// Save the portfolio on exit
jsonRepository.SavePortfolio(portfolio);
