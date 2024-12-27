using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data.Common;

public class ExcelLoader : MonoBehaviour
{


	void Load()
	{
		
	}


	void Start()
	{
        //     SingleTone<GameManager>.Instance.MakeItemList(i, save_data);
		// }      
		List<Dictionary<string, object>> name_data = CSVReader.Read("item_name");
		List<Dictionary<string, object>> data = CSVReader.Read("item_recipe");

		
		for (int i = 0; i < data.Count; i++)
		{
			Dictionary<int, int> item_data = new Dictionary<int, int>();
			foreach (var item in data[i])
			{
				var item_name = item.Key;
				
				int id = int.Parse(name_data[0][item_name].ToString());
				item_data[id] = int.Parse(item.Value.ToString());
			}
			SingleTone<GameManager>.Instance.MakeItemList(i, item_data);
		}
	}
}
