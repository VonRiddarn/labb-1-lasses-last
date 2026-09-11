static class PriceRules
{
	static class Weight
	{
		public const decimal INCLUDED_STANDARD = 2;
		public const decimal INCLUDED_MEMBER = 5;
		public const decimal HEAVY_TRESHOLD = 20;
	}

	static class Cost
	{
		public const decimal BASE_PRICE = 49;
		public const decimal PER_KG = 10;
		public const decimal HEAVY_PER_KG = 30;
	}

	// ----- ----- -----
	// LÅT STÅ!!!!!
	// 49kr grundavgift
	// EJ MEDLEM: 2 kg ingår
	// Medlem: 5 kg ingår
	// Vikt: 10kr / kg > inkluderad vikt
	// 20+ kg = 30kr / kg > 20kg (OCH VANLIG VIKT!)
	// Försäkring = 1% item value (annars 0)
	// ----- ----- -----

	public static decimal BaseCost => Cost.BASE_PRICE;

	public static WeightCost GetWeightCost(decimal weightInKg, bool isMember) => new(
		(isMember ?
			(weightInKg > Weight.INCLUDED_MEMBER ? weightInKg - Weight.INCLUDED_MEMBER : 0) :
			(weightInKg > Weight.INCLUDED_STANDARD ? weightInKg - Weight.INCLUDED_STANDARD : 0)) * Cost.PER_KG,
		(weightInKg > Weight.HEAVY_TRESHOLD ? weightInKg - Weight.HEAVY_TRESHOLD : 0) * Cost.HEAVY_PER_KG
	);

	public static decimal GetInsuranceCost(decimal assetValue) => assetValue * 0.01m;
}