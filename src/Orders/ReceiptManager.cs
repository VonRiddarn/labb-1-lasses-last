using System.Text;

static class ReceiptManager
{
	const string INDEXER_PATH = "orders/.INDEXER";

	const string ORDERS_DIRECTORY = "orders";
	const string ORDER_PREFIX = "PK-";

	static readonly StringBuilder _sb = new();

	static int GetIndex()
	{
		try
		{
			if (int.TryParse(File.ReadAllText(INDEXER_PATH), out int i))
				return i;

			return -1;
		}
		catch
		{
			return -1;
		}
	}

	public static string CreateReceipt(Order order)
	{
		bool validCountry = CountryShipping.TryGetPrice(order.Country, out decimal shippingCost);

		_sb.Clear();
		_sb.Append("FRAKTKVITTO\n");
		_sb.AppendLine("-----------------------------");
		_sb.AppendLine("Avsändare: Lasses Last AB");
		_sb.AppendLine($"Mottagare: {order.Recipient}");
		_sb.AppendLine($"Land: {order.Country} ({(validCountry ? $"{shippingCost:F2} SEK" : "OKÄND")})");
		_sb.AppendLine($"Vikt: {order.WeightInKg} kg");
		_sb.AppendLine($"Varuvärde: {order.AssetValue:F2} SEK");
		_sb.AppendLine($"Medlem: {(order.IsMember ? "Ja" : "Nej")}");
		_sb.AppendLine($"Försäkrad: {(order.IsInsured ? "Ja" : "Nej")}");

		decimal baseCost = validCountry ? shippingCost : PriceRules.BaseCost;
		_sb.AppendLine($"\nGrundavgift:\t\t{baseCost}\t\tSEK");

		var wc = PriceRules.GetWeightCost(order.WeightInKg, order.IsMember);
		decimal ic = PriceRules.GetInsuranceCost(order.AssetValue);

		if (wc.Total > 0)
			_sb.AppendLine($"Viktavgift");
		if (wc.Standard > 0)
			_sb.AppendLine($"⨽ Standard:\t\t{wc.Standard:F2}\t\tSEK");
		if (wc.Heavy > 0)
			_sb.AppendLine($"⨽ Tungviktstillägg:\t{wc.Heavy:F2}\t\tSEK");

		if (ic > 0)
			_sb.AppendLine($"Försäkringsavgift:\t{ic:F2}\t\tSEK");
		_sb.AppendLine("-----------------------------");
		_sb.AppendLine($"Total:\t\t\t{(baseCost + wc.Total + ic):F2}\t\tSEK");

		return _sb.ToString();
	}

	public static bool SaveToFile(Order order)
	{
		int index = GetIndex();
		if (index == -1)
			return false;

		string receipt = CreateReceipt(order);

		try
		{
			File.WriteAllText($"{ORDERS_DIRECTORY}/{ORDER_PREFIX}{index++}", receipt);
			File.WriteAllText(INDEXER_PATH, index.ToString());
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static string GetReceiptFromFile(string searchPattern)
	{

		if (searchPattern.StartsWith(ORDER_PREFIX, ignoreCase: true, null))
			searchPattern = searchPattern[ORDER_PREFIX.Length..];

		string filePath = Path.Combine(ORDERS_DIRECTORY, $"{ORDER_PREFIX}{searchPattern}");

		try
		{
			if (File.Exists(filePath))
				return File.ReadAllText(filePath);

			return string.Empty;
		}
		catch
		{
			return string.Empty;
		}
	}
}