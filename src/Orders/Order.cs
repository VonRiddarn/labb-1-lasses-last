using ConsoleAtHome;

class Order(string recipient, decimal weightInKg, decimal assetValue, bool isMember, bool isInsured, string country)
{
	public string Recipient { get; private set; } = recipient;
	public decimal WeightInKg { get; private set; } = weightInKg;
	public decimal AssetValue { get; private set; } = assetValue;
	public bool IsMember { get; private set; } = isMember;
	public bool IsInsured { get; private set; } = isInsured;
	public string Country { get; private set; } = country;

	static public Order? FromSSV(string ssv)
	{
		if (string.IsNullOrWhiteSpace(ssv))
			return null;

		string[] fields = ssv.Split(';');

		// namn;vikt;värde;medlem;försäkring;land;stad
		// 0: namn | 1: vikt | 2: värde | 3: medlem | 4: försäkring | 5: land | 6: stad
		// Lowkey also just want to show I understand basic union types! :P
		if (
			fields.Length is not (6 or 7) ||
			!decimal.TryParse(fields[1], out decimal weightInKg) ||
			!decimal.TryParse(fields[2], out decimal assetValue) ||
			!Cah.Input.TryReverseParseYesNo(fields[3], out bool isMember) ||
			!Cah.Input.TryReverseParseYesNo(fields[4], out bool isInsured) ||
			!CountryShipping.AllCountries.Contains(fields[5])
		)
			return null;

		return new(fields[0], weightInKg, assetValue, isMember, isInsured, fields[5]);
	}

	static public Order[] FromSSV(string[] rows)
	{
		List<Order> r = [];

		foreach (string ssv in rows)
		{
			var o = FromSSV(ssv);

			if (o != null)
				r.Add(o);
		}

		return [.. r];
	}
}