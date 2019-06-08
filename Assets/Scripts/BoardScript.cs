using UnityEngine;
using System.Collections;

public class BoardScript : MonoBehaviour 
{
	public int boards, previousBoards;
	public Animator[] boardAnim;
	public GameObject[] board;
	public AudioClip repairSound;
	public AudioClip bangSound;
	GameObject player;

	Vector3 distance;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		boardAnim = GetComponentsInChildren<Animator> ();
		for (int i = 0; i < 6; i++)
		{
			boardAnim[i].Play ("boardAnimation" + (i+1).ToString());
		}
		boards = 6;

	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = this.transform.position - player.transform.position;

		if (Input.GetKey (KeyCode.F) && distance.magnitude < 2)
		{
			AddBoard ();
		}
	}

	void AddBoard()
	{
		if (boards < 6)
		{
			board[boards].SetActive(true);
			boardAnim[boards].Play ("boardAnimation" + (boards+1).ToString()); 
			boards += 1;
			GetComponent<AudioSource>().PlayOneShot(repairSound, 1.0f / GetComponent<AudioSource>().volume);
			Invoke ("SlamSound", 1f);
		}
	}

	public void RemoveBoard()
	{
		if(boards > 0)
		{
			//board[boards-1].SendMessage("DisableBoard",SendMessageOptions.RequireReceiver);
			board[boards-1].SetActive(false);
			boards -= 1;
			
			if(boards == 0)
				GetComponent<Renderer>().enabled = false;

		}
	}

	void SlamSound()
	{
		GetComponent<AudioSource>().PlayOneShot (bangSound, 1.0f / GetComponent<AudioSource>().volume);
	}
}








