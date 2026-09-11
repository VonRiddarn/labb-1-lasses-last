using System.Text;
using ConsoleAtHome;

class SOrderMultiple : IScene
{
	public void Enter() { }

	public void Exit() { }

	public ExitContext Run()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("== Lasses last 1.0 || LADDA IN FRAKTER ==");

			Console.Write("Ange filnamn, eller \"tbx\" för att avsluta: ");
			string filename = Console.ReadLine() ?? string.Empty;

			if (filename.Equals("tbx", StringComparison.OrdinalIgnoreCase))
				return new(null);

			try
			{
				filename = Path.ChangeExtension(filename, null);
				string[] rows = File.ReadAllLines($"data/{filename}.ssv");

				var orders = Order.FromSSV(rows);

				List<string> receipts = [];

				foreach (Order order in orders)
				{
					receipts.Add(ReceiptManager.CreateReceipt(order));
					ReceiptManager.SaveToFile(order);
				}

				Console.WriteLine($"\nSkapade kvitton:\n{string.Join('\n', receipts)}");
			}
			catch (Exception e)
			{
				if (e is FileNotFoundException)
					Console.WriteLine($"Det finns ingen fil i katalogen data som matchar detta namnet.");
				else
					Console.WriteLine($"Kritiskt fel: {e.Message}");

			}

			Console.WriteLine("\nTryck valfri knapp för att fortsätta...");
			Console.ReadKey();
		}
	}
}