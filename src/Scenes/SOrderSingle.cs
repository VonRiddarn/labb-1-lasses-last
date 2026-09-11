using System.Text;
using ConsoleAtHome;

class SOrderSingle : IScene
{
	public void Enter() { }

	public void Exit() { }

	public ExitContext Run()
	{
		string name;
		decimal kilos;
		decimal value;
		bool member;
		bool wantInsurance;

		StringBuilder accumulativeMenu = new("== Lasses last 1.0 || ENSKILD FRAKT\n");
		Console.Clear();
		Console.WriteLine(accumulativeMenu.ToString());

		// TODO: DRY this up later, obv.
		// Either through a repeatable method, 
		// or by using some dynamic wizard that iterates through properties

		name = Cah.Input.ReadLine("Namn: ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Namn: {name}\n").ToString());
		kilos = Cah.Input.ParseLine<decimal>("Vikt (kg): ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Vikt: {kilos} kg\n").ToString());
		value = Cah.Input.ParseLine<decimal>("Value (SEK): ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Value: {value} SEK\n").ToString());
		member = Cah.Input.ParseYesNo("Är du förmånsmedlem genom Lasse++ ? (y/n): ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Medlem: {(member ? "Ja" : "Nej")}\n").ToString());
		wantInsurance = Cah.Input.ParseYesNo("Vill du försäkra ditt paket? (y/n): ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Försäkra: {(wantInsurance ? "Ja" : "Nej")}\n").ToString());

		var wc = PriceRules.GetWeightCost(kilos, member);
		decimal ic = PriceRules.GetInsuranceCost(value);

		Console.WriteLine();
		Console.WriteLine("FRAKTKVITTO");
		Console.WriteLine("-----------------------------");
		Console.WriteLine("Avsändare: Lasses Last AB");
		Console.WriteLine($"Mottagare: {name}");
		Console.WriteLine($"Vikt: {kilos} kg");
		Console.WriteLine($"Varuvärde: {value} SEK");
		Console.WriteLine($"Medlem: {(member ? "Ja" : "Nej")}");
		Console.WriteLine($"Försäkring: {(wantInsurance ? "Ja" : "Nej")}\n");

		Console.WriteLine($"Grundavgift:\t\t{PriceRules.BaseCost}\t\tSEK");

		if (wc.Total > 0)
			Console.WriteLine($"Viktavgift");
		if (wc.Standard > 0)
			Console.WriteLine($"⨽ Standard:\t\t{wc.Standard}\t\tSEK");
		if (wc.Heavy > 0)
			Console.WriteLine($"⨽ Tungviktstillägg:\t{wc.Heavy}\t\tSEK");

		if (ic > 0)
			Console.WriteLine($"Försäkringsavgift:\t{ic}\t\tSEK");
		Console.WriteLine("-----------------------------");
		Console.WriteLine($"Total:\t\t\t{PriceRules.BaseCost + wc.Total + ic}\tSEK");
		Console.WriteLine();

		// This wont do jack all atm.
		Console.WriteLine("Stämmer detta? (ENTER)");
		Console.ReadLine();

		return new(null);
	}
}