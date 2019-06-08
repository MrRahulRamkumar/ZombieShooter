using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
	public int timeToWait;
	public Text timer;
	public Text nextWave;

	public bool running;

	// Use this for initialization
	public void StartTimer(int timeToWait)
	{
		this.timeToWait = timeToWait;
		StartCoroutine (oneSecond());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timeToWait == 0) {
			timer.text = "";
			nextWave.text = "";
			StopAllCoroutines ();
		} else if (running) {
			timer.text = (timeToWait.ToString ());
			nextWave.text = "Next wave in...";
		} else {
			timer.text = "";
			nextWave.text = "";
		}
	}
	IEnumerator oneSecond()
	{
		while (true)
		{
			yield return new WaitForSeconds (1f);
			timeToWait--;		
		}
	}
	 
}
