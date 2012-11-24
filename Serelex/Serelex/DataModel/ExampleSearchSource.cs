using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serelex.DataModel
{
	public static class ExampleSearchSource
	{
		static Random r = new Random();

		static List<string> exampleSearch = new List<string>()
        {
		    "fruit",
		    "vehicle",
		    "computational linguistics",
		    "machine learning",
		    "weapon",
		    "machine gun",
		    "antelope",
		    "airplane",
		    "mango",
		    "strawberry",
		    "Russia",
		    "Belgium",
		    "France",
		    "mathematics",
		    "biology",
		    "moose",
		    "racoon",
		    "dog",
		    "cat",
		    "animal",
		    "vegetable",
		    "Moscow",
		    "Stanford",
		    "Brussels",
		    "Vienna",
		    "Berlin",
		    "ferrari",
		    "porsche",
		    "lamborghini",
		    "pizza",
		    "hot dog",
		    "hamburger",
		    "soft drink",
		    "cottage cheese",
		    "local speciality"
        };

		public static string RandomSearchExample
		{
			get
			{
				int max = exampleSearch.Count - 1;
				int exmpIndex = r.Next(0, max);
				return exampleSearch[exmpIndex];
			}
		}
	}
}
