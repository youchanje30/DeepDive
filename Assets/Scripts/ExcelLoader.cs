using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class ExcelLoader : MonoBehaviour
{
	public TextAsset txt;
	int colSize, rowSize;

    public String Data;


	void Load()
	{
		
	}


	void Start()
	{
		Load();
		String currentText = Data.Substring(0, Data.Length-1);
		String[] line = currentText.Split(" ");
        foreach (var item in line)
        {
            Debug.Log(line);
        }
		colSize = line.Length;
		rowSize = line[0].Split(",").Length;
		
		for (int i=0; i<colSize; ++i)
		{
			string[] row = line[i].Split(",");
            Dictionary<string, string> save_data = new Dictionary<string, string>();
			int j = 0;
            while(j<row.Length)
            {
                save_data[row[j]] = row[j+1];
				// Debug.Log(int.Parse(row[j]));
                j += 2;
            }
            
            SingleTone<GameManager>.Instance.MakeItemList(i, save_data);
		}        
	}
}
