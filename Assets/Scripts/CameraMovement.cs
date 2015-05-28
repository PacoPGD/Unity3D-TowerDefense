using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float MoveSpeed;	// Speed of camera following mouse
	public float ZoomSpeed; // Speed of camera zoom
	public float RotSpeed; // Speed of camera rotation
	public bool NaturalMotion = true;	 //this determines whether a left swipe will make the camera tumble clockwise or anticlockwise around the object


	private Transform pivotPoint;	//this should be the location the camera tumbles around
	private GameObject camParent;	//this will be the rotating parent to which the camera is attached. Rotating this object will have the effect of making the camera a specified location.
	private Vector2 oldInputPosition; 

	void Start ()
	{
		camParent = new GameObject ("camParent");	 //create a new gameObject
		camParent.transform.position = pivotPoint.position;		//place the new gameObject at pivotPoint location
		transform.parent = camParent.transform;		//make this camera a child of the new gameObject
		camParent.transform.parent = transform;	//make the new gameobject a child of the original camera parent if it had one

	}


	void LateUpdate () 
	{
		MouseFollow ();
		CameraZoom ();
		CameraRotation ();
	}


	void MouseFollow()
	{
		float Angle = camParent.transform.localEulerAngles.y;
		float cos = Mathf.Cos (Angle*Mathf.PI/180.0f);
		float sin = Mathf.Sin (Angle*Mathf.PI/180.0f);

		//HORIZONTAL
		if(Input.mousePosition.x<0)
			transform.position=transform.position+new Vector3(-MoveSpeed*cos,0,MoveSpeed*sin);
		if(Input.mousePosition.x>Screen.width)
			transform.position=transform.position+new Vector3(MoveSpeed*cos,0,-MoveSpeed*sin);

		//VERTICAL
		if(Input.mousePosition.y<0)
			transform.position=transform.position+new Vector3(-MoveSpeed*sin,0,-MoveSpeed*cos);
		if(Input.mousePosition.y>Screen.height)
			transform.position=transform.position+new Vector3(MoveSpeed*sin,0,MoveSpeed*cos);
	}

	void CameraZoom()
	{
		//ZOOM IN
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
			transform.position =transform.position+ new Vector3(0,-ZoomSpeed,ZoomSpeed);


		//ZOOM OUT
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) 
			transform.position =transform.position+ new Vector3(0,ZoomSpeed,-ZoomSpeed);

	}

	void CameraRotation()
	{
		if (Input.GetMouseButtonDown(1))
			oldInputPosition = Input.mousePosition;

		if (Input.GetMouseButton(1))
		{
			float xDif = Input.mousePosition.x - oldInputPosition.x;
			if(!NaturalMotion){xDif *= -1;}
			if(xDif != 0){camParent.transform.Rotate(Vector3.up * xDif * RotSpeed);}
			oldInputPosition = Input.mousePosition;
		}

	}

	public void SetPivotPoint(Transform value)
	{
		pivotPoint = value;
	}
}
