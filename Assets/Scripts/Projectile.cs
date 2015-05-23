using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float speed = 10;
	float range = 10;
	int damage = 1;

	private float distance;

	void Start(){
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void Update ()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		distance += Time.deltaTime * speed;
		
		if (distance >= range)
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other){
	
		if (other.tag == "Enemy")
		{
			Enemy enemy;
			enemy = other.gameObject.GetComponent<Enemy>();
			
			enemy.ApplyDamage(damage);
			
			Destroy(gameObject);
		}
	}


}
