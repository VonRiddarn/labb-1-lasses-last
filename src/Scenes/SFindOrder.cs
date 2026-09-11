using System.Text;
using ConsoleAtHome;

class SFindOrder : IScene
{
	public void Enter() { }

	public void Exit() { }

	public ExitContext Run()
	{

		while (true)
		{


			Console.Clear();
			Console.WriteLine("== Lasses last 1.0 || FRAKT FRÅN FIL ==");

			Console.Write("Ange ordernummer, eller \"tbx\" för att avsluta: ");
			string searchTerm = Console.ReadLine() ?? string.Empty;

			if (searchTerm.Equals("tbx", StringComparison.OrdinalIgnoreCase))
				return new(null);

			string receipt = ReceiptManager.GetReceiptFromFile(searchTerm);

			Console.WriteLine(string.IsNullOrWhiteSpace(receipt) ? "Inget kvitto hittades." : receipt);
			Console.WriteLine();

			Console.WriteLine("\nTryck valfri knapp för att fortsätta...");

			Console.ReadKey();
		}
	}
}