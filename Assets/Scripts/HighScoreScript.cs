using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

//C:\Users\nextwave\Documents
public class HighScoreScript : MonoBehaviour 
{
	public Text GUIscores;
	
	string path = "C:\\Users\\Rahul Ramkumar\\Documents\\Unity Projects\\New Unity Project\\a.txt";
	string line;

	string [] userNames = new string[10];
	int [] userScores = new int[10];
	string [] splitStrings = new string[2];

	int counter = 1;
	int nameIndex = 0;
	int scoreIndex = 0;

	// Use this for initialization
	void Start () 
	{
		ReadHighScore ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	public void ReadHighScore() 
	{
		StreamReader reader = new StreamReader (path);
		string line;

		do 
		{
			line = reader.ReadLine ();
			if (line != null) 
			{
				splitStrings = line.Split(',');
				userNames[nameIndex] = splitStrings[0];
				userScores[scoreIndex] = int.Parse(splitStrings[1]);

				nameIndex++;
				scoreIndex++;
			}
		} while(line != null);
		reader.Close ();

		Sort ();

		for (int i = 0; i < userNames.Length; i++)
		{

			if (userNames [i] != null)
			{
				GUIscores.text = GUIscores.text + "\n" + counter.ToString () + ". " + userNames [i] + "---" + userScores[i]; 
				counter++;
			}
		}
	}

	public void Sort()
	{
		int tempInt;
		string tempString;

		for (int i = 0; i < userNames.Length; i++) 
		{
			for (int j = 0; j < userNames.Length - i - 1; j++) 
			{
				if (userScores [j] < userScores [j + 1]) 
				{
					tempInt = userScores [j];
					userScores [j] = userScores [j + 1];
					userScores [j + 1] = tempInt;

					tempString =  userNames [j];
					userNames [j] = userNames [j + 1];
					userNames [j + 1] = tempString;
				}
			}
		}
	}


}

