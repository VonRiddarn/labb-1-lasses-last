using System.Text;

static class ReceiptManager
{
	static readonly StringBuilder _sb = new();

	public static string GetReceipt(Order order)
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

		_sb.AppendLine($"\nGrundavgift:\t\t{PriceRules.BaseCost}\t\tSEK");

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
		if (validCountry)
			_sb.AppendLine($"Frakt:\t\t\t{shippingCost:F2}\t\tSEK");
		_sb.AppendLine("-----------------------------");
		_sb.AppendLine($"Total:\t\t\t{PriceRules.BaseCost + wc.Total + ic + (validCountry ? shippingCost : 0)}\t\tSEK");

		return _sb.ToString();
	}

	// TODO: Add write to file and fetch from file methods.
}