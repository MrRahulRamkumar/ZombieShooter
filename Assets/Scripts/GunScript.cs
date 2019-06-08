 using UnityEngine;
 using System.Collections;
using UnityEngine.UI;
 
 public class GunScript : MonoBehaviour
{
 
	public Rigidbody casing;

	public Transform eject;
	public Camera fpsCam;
	public Animator anim;

	//effects
	public GameObject impactEffect;
	public GameObject bloodEffect;
	public ParticleSystem muzzleFlash;
	public AudioSource gunSound;
	public AudioClip shoot;
	public AudioClip empty;
	public AudioClip reload;

	//HUD elements
	public RectTransform textPosition;
	public Text bulletsHUD;
	public Vector3 bulletsHUD_position;
	public Vector3 ReloadTextPosition;

	//gun properties
	public float range;
	public float damage;
	public int maxBulletsInMag;
	public static int maxBullets = 300;
	public int currentBullets;
	public float shotsPerSecond;
	public float reloadTime;



	float nextTimeToFire = 0f;
	public static bool isReloading = false;

	int initMaxBullets = 300;

	void Start()
	{
		maxBullets = initMaxBullets;
	}

	void Update() 
	{
		updateBullets ();
		if (maxBullets <= 0 && currentBullets <= 0) 
		{
			anim.SetBool ("Shoot",false);	
			if (Input.GetButton ("Fire1"))
				gunSound.PlayOneShot (empty);
			return;
		}
		if (isReloading) 
		{
			anim.SetBool ("Shoot",false);			
			return;
		}
		if (currentBullets <= 0  || Input.GetButtonDown("Reload") && currentBullets!= maxBulletsInMag) 
		{
			if (maxBullets <= 0)
				return;
			
			anim.SetBool ("Shoot",false);			
			StartCoroutine (Reload());
			return;
		}			
		
		if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) 
		{			
			ShellCasing ();	
			Fire ();
			nextTimeToFire = Time.time + 1f/shotsPerSecond;
        }
		if(Input.GetButtonUp ("Fire1"))
		{
			anim.SetBool ("Shoot",false);			
		}
    }

	void ShellCasing ()
	{
		Rigidbody clone;
		clone = (Rigidbody)Instantiate(casing, eject.position,eject.rotation);
		clone.velocity = transform.TransformDirection(Vector3.right * 3);
		Destroy (clone.gameObject,0.5f);		
	}
	void Fire()
	{
		currentBullets--;
		RaycastHit hit;
		ZombieController hitZombie;
		ZombieController1 hitZombie1;


		if (Physics.Raycast (fpsCam.transform.position,fpsCam.transform.forward, out hit, range))
		{
			hitZombie = hit.transform.GetComponent<ZombieController> ();
			hitZombie1 = hit.transform.GetComponent<ZombieController1> ();

			if (hitZombie != null && hitZombie.dead == false) 
			{
				GameObject temp = (GameObject)Instantiate (bloodEffect, hit.point, Quaternion.LookRotation (hit.normal));
				Destroy (temp, 0.5f);
				hitZombie.TakeDamage (damage);
			} 
			else if (hitZombie1 != null && hitZombie1.dead == false) 
			{
				GameObject temp = (GameObject)Instantiate (bloodEffect, hit.point, Quaternion.LookRotation (hit.normal));
				Destroy (temp, 0.5f);
				hitZombie1.TakeDamage (damage);
			}
			else
			{
				GameObject temp = (GameObject)Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
				Destroy (temp,0.5f);
			}
		}	
		//Effects
		gunSound.PlayOneShot(shoot);
		muzzleFlash.Play ();
		anim.SetBool ("Shoot",true);
	}

	IEnumerator Reload()
	{	
		int diff = maxBulletsInMag - currentBullets;
		
		if (diff > maxBullets) 
		{
			currentBullets = maxBullets;
			maxBullets = 0;
		} 
		else
		{
			currentBullets += diff;
			maxBullets -= diff;
		}
		






		isReloading = true;
		anim.SetBool ("Reload",true);
		gunSound.PlayOneShot (reload);
		yield return new WaitForSeconds (reloadTime);
		anim.SetBool ("Reload",false);
		isReloading = false;
	}

	void updateBullets()
	{
		if (isReloading) 
		{			
			textPosition.localPosition = ReloadTextPosition;
			bulletsHUD.text = "Reloading...";
		} 
		else 
		{
			textPosition.localPosition = bulletsHUD_position;
			bulletsHUD.text = currentBullets + "/" + maxBullets;
		}
	}
	void AddBullets()
	{
		maxBullets += 30;
	}
 }