using System;

namespace PacketSniffer
{
	class Program
	{
		static void Main(string[] args)
		{
			Options options = new Options();
			
			options.Begin();

            Console.WriteLine("Press any key to exit...");

			Console.ReadKey();
        }
	}
}
