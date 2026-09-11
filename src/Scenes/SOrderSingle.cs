using System.Text;
using ConsoleAtHome;

class SOrderSingle : IScene
{
	public void Enter() { }

	public void Exit() { }

	public ExitContext Run()
	{

		string header = "== Lasses last 1.0 || ENSKILD FRAKT ==";
		var order = OrderBuilder.RunWizard(header);
		string receipt = ReceiptManager.CreateReceipt(order);
		ReceiptManager.SaveToFile(order);

		Console.Clear();
		Console.WriteLine(header);
		Console.WriteLine(receipt);
		Console.WriteLine("\nTryck valfri knapp för att fortsätta...");

		Console.ReadKey();

		return new(null);
	}
}