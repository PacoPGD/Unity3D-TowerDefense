using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float speed = 100;

	int damage = 5;

	private float distance;

	void Start(){
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void Update ()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		distance += Time.deltaTime * speed;
	}
	
	void OnTriggerEnter(Collider other){
	
		if(other.gameObject.GetComponent<Enemy>())
		{
			other.gameObject.GetComponent<Enemy>().ApplyDamage(damage);
			Destroy(gameObject);
		}
	}


}
