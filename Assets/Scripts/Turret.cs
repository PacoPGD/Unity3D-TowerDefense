using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public GameObject projectile;

	Transform target;
	//Transform turretControl;


	float reloadTime=1;
	//float turnSpeed=5;
	float firePauseTime =0.25f;

	float damage;


	private double nextFireTime;
	private float nextMoveTime;


	//private Quaternion desiredRotation;


	
	void Update ()
	{
		if (target)
		{
			if (Time.time >= nextMoveTime)
			{
				CalculateAimPosition(target.position);
				//turretControl.rotation = Quaternion.Lerp(turretControl.rotation, desiredRotation, Time.deltaTime * turnSpeed);
			}
			
			if (Time.time >= nextFireTime)
			{
				FireProjectile();
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<Enemy> ()) {
			nextFireTime = Time.time + (reloadTime * 0.5);
			target = other.gameObject.transform;
		}
	}
		
	void OnTriggerExit(Collider other){
		if (other.gameObject.transform==target) {
			target = null;
		}
		if (other.gameObject.GetComponent<Projectile>()) {
			Destroy(other.gameObject);
		}
	}
		
	void CalculateAimPosition(Vector3 targetPos)
	{
		//Vector3 aimPoint = new Vector3 (targetPos.x, target.position.y, target.position.z);
		//desiredRotation = Quaternion.LookRotation(aimPoint);
	}

	void FireProjectile()
	{
		GameObject aux;
		aux = projectile;

		nextFireTime = Time.time + reloadTime;
		nextMoveTime = Time.time + firePauseTime;

		aux = (GameObject)Instantiate (projectile);
		aux.transform.position = transform.position;
	}

}
		