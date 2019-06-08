using UnityEngine;
using System.Collections;

public class AmmoDropScript : MonoBehaviour
{

	Vector3 distance;
	int ammo = 5;

	GameObject player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = this.transform.position - player.transform.position;

		if (distance.magnitude < 2) 
		{
			GunScript.maxBullets += ammo;
			ammo = 0;
		}
		if (ammo == 0)
		{
			Destroy (this.gameObject);
		}
	}
	public void remove()
	{
		Destroy (this.gameObject, 5f);
	}
}
