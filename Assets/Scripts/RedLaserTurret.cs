using UnityEngine;
using System.Collections;

public class RedLaserTurret : MonoBehaviour {

	public GameObject Projectile;
	
	private Transform target;
	private Transform turretControl;
	private float turnSpeed=10;



	private GameObject laserBeam;

	void Start()
	{
		turretControl = transform.GetChild (0);
	}

	
	void Update ()
	{
		if (target) 
		{
			turretControl.rotation = Quaternion.Lerp (turretControl.rotation, CalculateAimPosition (), Time.deltaTime * turnSpeed);

			laserBeam.transform.rotation = turretControl.rotation;
		} 
		else 
			Destroy (laserBeam);

	}
	

	void OnTriggerStay(Collider other)
	{
		if (target == null) 
		{
			if (other.gameObject.GetComponent<Enemy> ())
			{
				Destroy (laserBeam);
				target = other.gameObject.transform;
				InstanceProjectile (other);
			}
		}

	}
		
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy> ()) 
		{
			if (other.gameObject.transform == target) 
			{
				target = null;
				Destroy (laserBeam);
			}
		}
	}
		
	Quaternion CalculateAimPosition()
	{
		Vector3 aimPoint = new Vector3 (target.position.x, target.position.y, target.position.z);
		Vector3 originPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		Quaternion desiredRotation = Quaternion.LookRotation(aimPoint-originPoint);
		return desiredRotation;
	}

	void InstanceProjectile(Collider other)
	{
		laserBeam = (GameObject)Instantiate (Projectile,turretControl.position,turretControl.rotation);
		laserBeam.gameObject.GetComponent<LaserBeam> ().receiveEnemyCollider (other);
	}


}
		