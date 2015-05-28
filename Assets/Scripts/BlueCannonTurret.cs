using UnityEngine;
using System.Collections;

public class BlueCannonTurret : MonoBehaviour {


	public GameObject projectile;
	public float reloadTime=1;


	private Transform target;
	private Transform turretControl;
	private float turnSpeed=10;
	private float firePauseTime =0.05f;
	private double nextFireTime;
	private float nextMoveTime;


	void Start()
	{
		turretControl = transform.GetChild (0);
	}

	
	void Update ()
	{
		if (target)
		{
			if (Time.time >= nextMoveTime)
			{
				turretControl.rotation = Quaternion.Lerp(turretControl.rotation, CalculateAimPosition (), Time.deltaTime * turnSpeed);
			}
			
			if (Time.time >= nextFireTime)
			{
				FireProjectile();
			}
		}
	}
	

	void OnTriggerStay(Collider other)
	{
		if (target == null)
		{
			if (other.gameObject.GetComponent<Enemy> ()) 
			{
				nextFireTime = Time.time + (reloadTime * 0.5);
				target = other.gameObject.transform;
			}
		}
	}
		
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.transform==target) 
			target = null;
	}
		
	Quaternion CalculateAimPosition()
	{
		Vector3 aimPoint = new Vector3 (target.position.x, target.position.y, target.position.z);
		Vector3 originPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		Quaternion desiredRotation = Quaternion.LookRotation(aimPoint-originPoint);
		return desiredRotation;
	}

	void FireProjectile()
	{
		nextFireTime = Time.time + reloadTime;
		nextMoveTime = Time.time + firePauseTime;

		Instantiate (projectile,turretControl.position,turretControl.rotation);
	}
	

}
		