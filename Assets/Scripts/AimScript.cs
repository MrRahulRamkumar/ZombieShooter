using UnityEngine;
using System.Collections;

public class AimScript : MonoBehaviour 
{
	public Vector3 aimPosition;
	public float aimSpeed;
	Vector3 OriginalPosition;
	PlayerControl playerControl;

	void Start () 
	{
		OriginalPosition = transform.localPosition;	
	}	

	void Update ()
	{
		if (Input.GetButton ("Fire2") && PlayerControl.index == 0 && GunScript.isReloading == false) 
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition,aimPosition,Time.deltaTime * aimSpeed);
		} 
		else
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition,OriginalPosition,Time.deltaTime * aimSpeed);
		}	
	}

}
