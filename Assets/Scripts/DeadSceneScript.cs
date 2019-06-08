using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;


public class DeadSceneScript : MonoBehaviour
{
	//string path = "C:\\Users\\nextwave\\Documents\\New Unity Project\\a.txt";
	StreamWriter writer = new StreamWriter("C:\\Users\\rahul\\Documents\\Unity Projects\\ZombieShooter",true);
	public Text score;
	public InputField name;
	public Button retryButton;
	public Button mainMenuButton;
	public Canvas buttonHolder;
	public Canvas inputFieldHolder;

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;

		score.text = "Score: " + GameManager.score;
		buttonHolder.enabled = false;
		retryButton.enabled = false;
		mainMenuButton.enabled = false;		
	}

	public void WriteToFile()
	{		
		buttonHolder.enabled = true;
		retryButton.enabled = true;
		mainMenuButton.enabled = true;
		score.enabled = false;
		inputFieldHolder.enabled = false;

		string line = name.text + "," + GameManager.score;
		writer.WriteLine (line);
		writer.Close();
	}

	public void Retry()
	{
		SceneManager.LoadScene ("Hangar");
	}
	public void MainMenu()
	{
		SceneManager.LoadScene ("Menu");
	}



}


