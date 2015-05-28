using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float Speed;
	public int Damage;


	void Start()
	{
		gameObject.GetComponent<Renderer>().material.color = Color.blue;
	}

	void Update ()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * Speed);
	}
	

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<Enemy>())
		{
			other.gameObject.GetComponent<Enemy>().ApplyDamage(Damage);
			Destroy(gameObject);
		}
	}


}
