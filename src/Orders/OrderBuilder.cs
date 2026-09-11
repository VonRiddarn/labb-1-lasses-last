using System.Text;
using ConsoleAtHome;

static class OrderBuilder
{
	public static Order RunWizard(string header)
	{
		StringBuilder accumulativeMenu = new($"{header}\n");
		Console.Clear();
		Console.WriteLine(accumulativeMenu.ToString());

		string name = Cah.Input.ReadLine("Namn: ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Namn: {name}\n").ToString());

		string country = Cah.Input.ParseCustom("Vilket land ska du frakta till? ", CountryShipping.AllCountries, showAlternatives: true);
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Land: {country}\n").ToString());

		decimal kilos = Cah.Input.ParseLine<decimal>("Vikt (kg): ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Vikt: {kilos} kg\n").ToString());

		decimal value = Cah.Input.ParseLine<decimal>("Value (SEK): ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Value: {value} SEK\n").ToString());

		bool member = Cah.Input.ParseYesNo("Är du förmånsmedlem genom Lasse++ ? (y/n): ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Medlem: {(member ? "Ja" : "Nej")}\n").ToString());

		bool wantInsurance = Cah.Input.ParseYesNo("Vill du försäkra ditt paket? (y/n): ");
		Console.Clear();
		Console.Write(accumulativeMenu.Append($"Försäkra: {(wantInsurance ? "Ja" : "Nej")}\n").ToString());

		return new(name, kilos, value, member, wantInsurance, country);
	}

	// TODO: Create accumulative menu object
}