using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]

public class LaserBeam : MonoBehaviour {
	
	private LineRenderer line;
	private int length;
	public int damage=1;
	
	
	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
		line.SetWidth(1, 1);
		line.SetColors (Color.red,Color.red);
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 myAngle = transform.rotation.eulerAngles;

		Ray ray = new Ray(transform.position, Quaternion.AngleAxis(myAngle.y, transform.up) *Vector3.forward);

		RaycastHit hit;
		
		line.SetPosition(0, ray.origin);
		
		if(Physics.Raycast(ray, out hit, 100))
		{
			line.SetPosition(1, hit.point);
		}
		else
			line.SetPosition(1, ray.GetPoint(100));
	
	}
	
}
