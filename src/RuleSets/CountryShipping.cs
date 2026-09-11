static class CountryShipping
{
	const string FILE_PATH = "src/RuleSets/countries.ssv";

	// Lazy initialization so that we don't need to re-fetch countries from file several times per runtime
	static Dictionary<string, decimal>? _kvps = null;
	static Dictionary<string, decimal> CountryPairs => _kvps ??= Initialize();

	public static bool TryGetPrice(string countryName, out decimal price)
		=> CountryPairs.TryGetValue(countryName, out price);

	public static string[] AllCountries => [.. CountryPairs.Keys];

	static Dictionary<string, decimal> Initialize()
	{
		Dictionary<string, decimal> r = new(StringComparer.OrdinalIgnoreCase);

		foreach (string row in File.ReadAllLines(FILE_PATH))
		{
			if (row.Split(';').Length != 2)
				continue;

			string[] values = row.Split(';');

			if (decimal.TryParse(values[1], out decimal price))
				r.TryAdd(values[0], price);
		}

		return r;
	}
}