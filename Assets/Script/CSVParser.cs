using UnityEngine;
using System.Collections;
using System.IO;

public class CSVParser : MonoBehaviour {
	private ArrayList parsingList;

	public ArrayList ParsingList
	{
		get{ return parsingList;}
	}

	public CSVParser(string fileFath) {
		//read csv file
		string[] line  = File.ReadAllLines(fileFath);
		char[] cutline = { ',', '\n' };

		parsingList = new ArrayList();

		for(int i = 0; i < line.Length; i++) {
			string[] split = line[i].Split(cutline);
			for(int j=0; j<split.Length; j++) {
				split[j] = split[j].Trim();
			}
			parsingList.Add (split);
		}
	}
}
