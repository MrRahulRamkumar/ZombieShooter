using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GameObject [] zombie;
	public Transform [] SpawnPoint;
	public GUISkin scoreCard;
	public static int score = 0;
	public Texture crosshairTexture;

	Timer timer;
	int round = 1;
	int initScore = 0;

	float nextTimeToSpawn;
	public int maxZombies = 5;
	public static int currentZombies = 0;
	public int spawnedZombies = 0;

	void Start()
	{
		score = initScore;
		timer = GetComponent<Timer> ();
	}

	// Update is called once per frame
	void Update () 
	{
		int t;
		if (spawnedZombies < maxZombies && Time.time >= nextTimeToSpawn) 
		{
			t = Random.Range (0, 2);
			if (t == 0) 
			{
				Instantiate (zombie [0], SpawnPoint [0].position, Quaternion.identity);
			}
			else 
			{
				Instantiate (zombie [1], SpawnPoint [1].position, Quaternion.identity);
			}

			nextTimeToSpawn = Time.time + 4f;
			currentZombies++;
			spawnedZombies++;
		}
		if (currentZombies == 0 && spawnedZombies == maxZombies && timer.running == false) 
		{
			timer.StartTimer (15);
			timer.running = true;
		}
		if (timer.timeToWait == 1 && timer.running)
		{
			spawnedZombies = 0;
			maxZombies += 10;
			round++;
			timer.running = false;
		}
	}

	void OnGUI()
	{
		GUI.skin = scoreCard;
		GUIStyle style = scoreCard.customStyles [0];

		GUI.Label (new Rect (40, Screen.height - 70, 100, 60), "HEALTH:");
		GUI.Label (new Rect (100, Screen.height - 70, 100, 60), "" + PlayerControl.health, style);

		GUI.Label (new Rect (40, Screen.height - 50, 100, 60), "SCORE:");
		GUI.Label (new Rect (100, Screen.height - 50, 100, 60), "" + score, style);

		GUI.Label (new Rect (40, Screen.height - 30 ,100, 60), "ROUND:");
		GUI.Label (new Rect (100, Screen.height - 30,100, 60), "" + round, style);

		GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height), crosshairTexture);

	}
}
