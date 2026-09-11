using System.Text;

static class ReceiptManager
{
	static readonly StringBuilder _sb = new();

	public static string GetReceipt(Order order)
	{
		_sb.Clear();
		_sb.Append("FRAKTKVITTO\n");
		_sb.AppendLine("-----------------------------");
		_sb.AppendLine("Avsändare: Lasses Last AB");
		_sb.AppendLine($"Mottagare: {order.Recipient}");
		_sb.AppendLine($"Land: {order.Country}");
		_sb.AppendLine($"Vikt: {order.WeightInKg} kg");
		_sb.AppendLine($"Varuvärde: {order.AssetValue} SEK");
		_sb.AppendLine($"Medlem: {(order.IsMember ? "Ja" : "Nej")}");
		_sb.AppendLine($"Försäkrad: {(order.IsInsured ? "Ja" : "Nej")}");

		_sb.AppendLine($"\nGrundavgift:\t\t{PriceRules.BaseCost}\t\tSEK");

		var wc = PriceRules.GetWeightCost(order.WeightInKg, order.IsMember);
		decimal ic = PriceRules.GetInsuranceCost(order.AssetValue);

		if (wc.Total > 0)
			_sb.AppendLine($"Viktavgift");
		if (wc.Standard > 0)
			_sb.AppendLine($"⨽ Standard:\t\t{wc.Standard}\t\tSEK");
		if (wc.Heavy > 0)
			_sb.AppendLine($"⨽ Tungviktstillägg:\t{wc.Heavy}\t\tSEK");

		if (ic > 0)
			_sb.AppendLine($"Försäkringsavgift:\t{ic}\t\tSEK");
		_sb.AppendLine("-----------------------------");
		_sb.AppendLine($"Total:\t\t\t{PriceRules.BaseCost + wc.Total + ic}\tSEK");

		return _sb.ToString();
	}

	// TODO: Add write to file and fetch from file methods.
}