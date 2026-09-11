using System.Text;
using ConsoleAtHome;

class SOrderSingle : IScene
{
	public void Enter() { }

	public void Exit() { }

	public ExitContext Run()
	{

		var order = OrderBuilder.RunWizard("== Lasses last 1.0 || ENSKILD FRAKT ==");
		string receipt = ReceiptManager.GetReceipt(order);
		ReceiptManager.SaveToFile(order);

		Console.WriteLine(receipt);

		return new(null);
	}
}