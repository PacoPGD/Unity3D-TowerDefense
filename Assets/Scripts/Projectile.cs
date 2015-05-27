using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 150;

	public int damage=10;


	void Start(){
		gameObject.GetComponent<Renderer>().material.color = Color.blue;
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


}
