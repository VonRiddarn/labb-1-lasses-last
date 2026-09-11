using System.ComponentModel.Design;

struct WeightCost(decimal standard, decimal heavy)
{
	public decimal Standard = standard;
	public decimal Heavy = heavy;
	public decimal Total = standard + heavy;
}