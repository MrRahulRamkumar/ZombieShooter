using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour 
{
	public Canvas mainMenu;
	public Canvas exitMenu;
	public Canvas highScoreMenu;

	public Button yesButton;
	public Button noButton;
	public Button exitButton;
	public Button playButton;
	public Button highScoreButton;

	// Use this for initialization
	void Start ()
	{
		exitMenu.enabled = false;	
		highScoreMenu.enabled = false;
	}

	public void EnableExitMenu()
	{
		exitMenu.enabled = true;
		playButton.enabled = false;
		exitButton.enabled = false;
		highScoreButton.enabled = false;
	}
	public void DisableExitMenu()
	{
		exitMenu.enabled = false;
		playButton.enabled = true;
		exitButton.enabled = true;
		highScoreButton.enabled = true;
	}

	public void EnableHighScoreMenu()
	{
		highScoreMenu.enabled = true;
		mainMenu.enabled = false;

		playButton.enabled = false;
		exitButton.enabled = false;
		highScoreButton.enabled = false;
	}
	public void DisableHighScoreMenu()
	{
		highScoreMenu.enabled = false;
		mainMenu.enabled = true;

		playButton.enabled = true;
		exitButton.enabled = true;
		highScoreButton.enabled = true;
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void StartGame()
	{
		SceneManager.LoadScene ("Hangar");
	}

	public void ShowHighScore()
	{
		
	}

}
