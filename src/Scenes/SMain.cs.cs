using ConsoleAtHome;

class SMain : IScene
{
	public void Enter() { }

	public void Exit() { }

	public ExitContext Run()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("VÄLKOMMEN TILL LASSES LAST 1.0\n");
			Console.WriteLine("1) Beräkna frakt för ett paket");
			Console.WriteLine("2) Beräkna frakt för flera paket från fil");
			Console.WriteLine("3) Hitta en order");
			Console.WriteLine("4) Avsluta\n");
			string input = Cah.Input.ReadLine("Val: ");

			switch (input)
			{
				case "1":
					return new(SceneRepository.OrderSingle, true);
				case "3":
					return new(SceneRepository.FindOrder, true);
				case "4":
					return new(null);
				default:
					Console.WriteLine("Fel val!");
					break;
			}
		}
	}
}