using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	class Program
	{
		static void Main(string[] args)
		{
			CircularList<string> circularList = new CircularList<string>(5);

			for (int i = 0; i < 13; i++)
			{
				circularList.Add(i.ToString());
			}

			foreach (string value in circularList)
			{
				Console.WriteLine($"Value: {value}");
			}
		}
	}


}
