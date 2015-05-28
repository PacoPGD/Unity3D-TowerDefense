using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]

public class LaserBeam : MonoBehaviour {

	public int DamagePerSecond;

	private LineRenderer line;
	private float nextDamageTime;
	private Collider enemyCollider;
	
	// Use this for initialization
	void Start () 
	{
		nextDamageTime = Time.time;
		line = GetComponent<LineRenderer>();
		line.SetWidth(1, 1);
		line.SetColors (Color.red,Color.red);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 myAngle = transform.rotation.eulerAngles;

		Ray ray = new Ray(transform.position, Quaternion.AngleAxis(myAngle.y, transform.up) *Vector3.forward);

		RaycastHit hit;
		
		line.SetPosition(0, ray.origin);
		
		if(Physics.Raycast(ray, out hit, 200))
		{
			line.SetPosition(1, hit.point);
		}
		else
			line.SetPosition(1, ray.GetPoint(200));

		if (hit.collider == enemyCollider) 
		{
			if (Time.time >= nextDamageTime) 
			{
				try 
				{ 
					hit.collider.gameObject.GetComponent<Enemy> ().ApplyDamage (DamagePerSecond);
					nextDamageTime = Time.time + 1;
				} 
				catch 
				{
					Destroy (gameObject);
				}
			}
		}



	}

	public void receiveEnemyCollider(Collider value)
	{
		enemyCollider = value;
	}

}
