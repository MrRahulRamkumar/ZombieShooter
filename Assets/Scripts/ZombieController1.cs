using UnityEngine;
using System.Collections;

public class ZombieController1 : MonoBehaviour 
{
	UnityEngine.AI.NavMeshAgent nav;
	GameObject player;
	GameObject spawnWall;
	Vector3 distance;
	Vector3 spawnWallDisatance;
	Animator anim;
	PlayerControl playerControl;
	CapsuleCollider zombieCollider;
	GameObject ammo;

	//Audio
	public AudioSource zombieSound;
	public AudioClip[] sounds;

	public bool dead = false;


	public float health;
	public float damage;
	float nextTimeToAttack;
	float nextTimeToAttackWall;
	float damageInterval = 1f;
	int boards;
	BoardScript boardScript;

	void Start () 
	{
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag ("Player");	
		playerControl = player.GetComponent<PlayerControl> ();
		zombieCollider = GetComponent<CapsuleCollider> ();
		spawnWall = GameObject.FindGameObjectWithTag("SpawnWall1");
		boardScript = spawnWall.GetComponent<BoardScript> ();
		boards = boardScript.boards;
		ammo = GameObject.FindGameObjectWithTag ("Ammo");
	}

	// Update is called once per frame
	void Update ()
	{
		boards = boardScript.boards;
		ZombieAI ();
	}

	void ZombieAI()
	{
		nav.SetDestination (player.transform.position);	
		distance = this.transform.position - player.transform.position;
		spawnWallDisatance = this.transform.position - spawnWall.transform.position;

		if (spawnWallDisatance.magnitude <= 1 && boards > 0) 
		{
			AttackWall ();
		} 

		else if(boards == 0 && dead == false)
		{
			nav.Resume ();
		}

		if (distance.magnitude < 3)
		{
			zombieSound.PlayOneShot (sounds[Random.Range(0,sounds.Length)]);
		}

		if(distance.magnitude < 2)
		{			
			Attack ();
		}
		else if(spawnWallDisatance.magnitude >= 1)
		{
			anim.SetBool("Attack",false);				
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Dead ();
		}
	}

	void Dead()
	{
		dead = true;
		zombieCollider.isTrigger = true;
		nav.Stop ();	
		anim.SetBool ("Dead",true);
		GameManager.score += 50;
		GameManager.currentZombies--;
		Destroy (this.gameObject,3f);
		GameObject tempAmmo = (GameObject) Instantiate (ammo,this.transform.position,Quaternion.identity);
		tempAmmo.GetComponent<AmmoDropScript> ().remove ();
	}

	void Attack()
	{
		if (Time.time >= nextTimeToAttack) 
		{
			anim.SetBool ("Attack",true);
			nextTimeToAttack = Time.time + damageInterval;
			playerControl.TakeDamage (damage);
			zombieSound.PlayOneShot (sounds[Random.Range(0,sounds.Length)]);		
		}
	}

	void AttackWall()
	{
		nav.Stop();
		if (Time.time >= nextTimeToAttack)
		{
			zombieSound.PlayOneShot (sounds[Random.Range(0,sounds.Length)]);
			anim.SetBool ("Attack",true);
			boardScript.RemoveBoard ();
			nextTimeToAttack = Time.time + damageInterval;
		}
	}

}
