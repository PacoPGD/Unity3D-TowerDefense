using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float speed = 150;

	int damage = 2;


	void Start(){
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void Update ()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.GetComponent<Enemy>())
		{
			other.gameObject.GetComponent<Enemy>().ApplyDamage(damage);
			Destroy(gameObject);
		}
	}

	public void selectTarget(Transform target){

	}

}
