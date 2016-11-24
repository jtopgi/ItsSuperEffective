using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ItsSuperEffective
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello to the Pokemon Type Calculator");
			Console.WriteLine("Please input the type of the attacking Pokemon:");
			string atkType = Console.ReadLine();
			Console.WriteLine("Please input the type of the defending Pokemon:");
			string defType = Console.ReadLine();

			WebClient pokemonWC = new WebClient();
			string typeData = pokemonWC.DownloadString("http://pokeapi.co/api/v2/type/");
			JObject parsedTypeData = JObject.Parse(typeData);
			int numberoftypes = Convert.ToInt32(parsedTypeData["count"]);
			string[] typeDatabase = new string[numberoftypes];
			string[] nameTypeDatabase = new string[numberoftypes];

			for (int i = 0; i < numberoftypes; i++)
			{
				typeDatabase[i] = Convert.ToString(parsedTypeData["results"][i]["url"]);
				nameTypeDatabase[i] = Convert.ToString(parsedTypeData["results"][i]["name"]);
			}

			int indexOfAtkType = Array.IndexOf(nameTypeDatabase, atkType);
			string atkTypeDB = pokemonWC.DownloadString(typeDatabase[indexOfAtkType]);
			JObject parsedAtkTypeDB = JObject.Parse(atkTypeDB);
					
			string halfDmg = Convert.ToString(parsedAtkTypeDB["damage_relations"]["half_damage_to"]);
			string noDmg = Convert.ToString(parsedAtkTypeDB["damage_relations"]["no_damage_to"]);
			string doubleDmg = Convert.ToString(parsedAtkTypeDB["damage_relations"]["double_damage_to"]);

			if (halfDmg.Contains(defType))
			{
				Console.WriteLine("1/2 damage");
			}
			else if (noDmg.Contains(defType))
			{
				Console.WriteLine("no damage");
			}
			else if (doubleDmg.Contains(defType))
			{
				Console.WriteLine("double damage");
			}
			else
			{
				Console.WriteLine("Invalid Defense Type");
			}

		}
	}
}
