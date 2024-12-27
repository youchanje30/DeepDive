using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

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
				int id = int.Parse((string)name_data[0][item.Key]);
				// item_data[id] = data[i][id].CloneViaSerialization;
			}
		}
	}
}
