class Order(string recipient, decimal weightInKg, decimal assetValue, bool isMember, bool isInsured, string country)
{
	public string Recipient { get; private set; } = recipient;
	public decimal WeightInKg { get; private set; } = weightInKg;
	public decimal AssetValue { get; private set; } = assetValue;
	public bool IsMember { get; private set; } = isMember;
	public bool IsInsured { get; private set; } = isInsured;
	public string Country { get; private set; } = country;

	public Order? FromSSV(string ssv)
	{
		if (string.IsNullOrWhiteSpace(ssv))
			return null;

		return null;
	}
}