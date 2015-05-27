using UnityEngine;
using System.Collections;

public class RedLaserTurret : MonoBehaviour {

	public GameObject projectile;
	public int damage;
	public float reloadTime=1;


	private Transform target;
	private Transform turretControl;

	private float turnSpeed=10;
	private float firePauseTime =0.05f;

	private double nextFireTime;
	private float nextMoveTime;

	private GameObject laserBeam;

	void Start(){
		laserBeam = projectile;
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
			
			laserBeam.transform.rotation = turretControl.rotation;
		}
	}
	

	void OnTriggerStay(Collider other){
		if (target == null){
			if (other.gameObject.GetComponent<Enemy> ()) {
				nextFireTime = Time.time + (reloadTime * 0.5);
				target = other.gameObject.transform;
				InstanceProjectile ();
			}
		}
	}
		
	void OnTriggerExit(Collider other){
		if (other.gameObject.transform==target) {
			target = null;
			laserBeam = null;
		}
	}
		
	Quaternion CalculateAimPosition()
	{
		Vector3 aimPoint = new Vector3 (target.position.x, target.position.y, target.position.z);
		Vector3 originPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		Quaternion desiredRotation = Quaternion.LookRotation(aimPoint-originPoint);
		return desiredRotation;
	}

	void InstanceProjectile()
	{


		laserBeam = (GameObject)Instantiate (projectile,turretControl.position,turretControl.rotation);
	
	}

}
		