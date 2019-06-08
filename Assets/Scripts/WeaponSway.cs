using UnityEngine;
using System.Collections;

public class WeaponSway : MonoBehaviour {

	public float amount,smooth;
	Vector3 originalPosition;
	Vector3 finalPosition;


	// Use this for initialization
	void Start () {
		originalPosition = transform.localPosition;
	
	}
	
	// Update is called once per frame
	void Update () {
		float MovementX = -Input.GetAxis ("Mouse X") * amount;
		float MovementY = -Input.GetAxis ("Mouse Y") * amount;
		MovementX = Mathf.Clamp (MovementX,-amount,amount);
		MovementY = Mathf.Clamp (MovementY,-amount,amount);

		finalPosition = new Vector3 (MovementX,MovementY,0);

		transform.localPosition = Vector3.Lerp (transform.localPosition,originalPosition+finalPosition,Time.deltaTime * smooth);
		
	
	}
}
