using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public GameObject projectile; 

	Transform target;
	Transform turretControl;
	Transform [] muzzlePositions;

	float reloadTime=1;

	int damage;

	public float speed = 3;

	private double nextFireTime;


	private Vector3 targetPos;


	void Update ()
	{
		if (target)
		{
			if (Time.time >= nextFireTime)
			{
				FireProjectile();
			}
		}
		

	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.GetComponent<Enemy>())
		{
			nextFireTime = Time.time + (reloadTime * 0.5);
			target = other.gameObject.transform;
		}
	}
	
	void OnTriggerExit(Collider other){
	
		if (other.gameObject.transform == target)
		{
			target = null;
		}
	}

	void FireProjectile()
	{
		nextFireTime = Time.time + reloadTime;

		projectile = (GameObject)Instantiate (projectile);
		projectile.transform.position += transform.position;
	}
	
			
			
			
}
		