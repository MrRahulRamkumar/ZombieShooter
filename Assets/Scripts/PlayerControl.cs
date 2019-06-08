using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerControl : MonoBehaviour
{
	public static float health = 100;
	int initHealth = 100;

	public CharacterController characterController;
	public Animator anim;
	public GameObject[] weapons;
	public Texture damageTexture;
	public static int index = 0;
	FirstPersonController firstPersonController;
	WeaponSway weaponSway;
	GunScript gunScript;

	void Start ()
	{
		gunScript = GetComponentInChildren<GunScript> ();
		firstPersonController = GetComponent<FirstPersonController> ();
		weaponSway = GetComponentInChildren<WeaponSway> ();
		health = initHealth;
		SwitchWeapon (0);
	}

	// Update is called once per frame
	void Update () 
	{
		if (GunScript.isReloading) 
		{
			return;
		}	
		if (Input.GetKey (KeyCode.Alpha1)) 
		{
			index = 0;
			SwitchWeapon (0);
			gunScript = GetComponentInChildren<GunScript> ();
		}
		if (Input.GetKey (KeyCode.Alpha2)) 
		{
			index = 1;
			SwitchWeapon (1);
			gunScript = GetComponentInChildren<GunScript> ();
		}
	}
	public void TakeDamage(float damage)
	{
		if(health > 0)
			health -= damage;
		
		if (health <= 0) 
			StartCoroutine (Dead());		
	}

	IEnumerator Dead()
	{
		characterController.enabled = false;
		firstPersonController.enabled = false;
		weaponSway.enabled = false;
		gunScript.enabled = false;

		anim.SetBool ("Dead",true);
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene ("Dead");

	}
	void SwitchWeapon(int index) 
	{
		for (int i=0; i < weapons.Length; i++)   
		{
			if (i == index)
			{
				weapons[i].gameObject.SetActive(true);
			} 
			else
			{ 
				weapons[i].gameObject.SetActive(false);
			}
		}
	}
	void OnGUI()
	{
		Rect rect = new Rect((Screen.width - damageTexture.width)/2,(Screen.height - damageTexture.height)/2,damageTexture.width,damageTexture.height);
		if(health <= 25)
			GUI.DrawTexture (rect, damageTexture);
	}
}
