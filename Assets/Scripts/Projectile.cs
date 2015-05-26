using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 150;

	private int damage;


	void Start(){
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void Update ()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}

	public void setDamage(int value){
		damage = value;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.GetComponent<Enemy>())
		{
			other.gameObject.GetComponent<Enemy>().ApplyDamage(damage);
			Destroy(gameObject);
		}
	}


}
