using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float moveSpeed = 4.0f;	// Speed of camera following mouse
	public float zoomSpeed = 4.0f; // Speed of camera zoom
	public float rotSpeed = 0.3f; // Speed of camera rotation


	private Transform pivotPoint;	//this should be the location the camera tumbles around
	public bool naturalMotion = true;	 //this determines whether a left swipe will make the camera tumble clockwise or anticlockwise around the object
	
	private GameObject camParent;	//this will be the rotating parent to which the camera is attached. Rotating this object will have the effect of making the camera a specified location.
	private Vector2 oldInputPosition; 

	void Start ()
	{
		camParent = new GameObject ("camParent");	 //create a new gameObject
		camParent.transform.position = pivotPoint.position;		//place the new gameObject at pivotPoint location
		transform.parent = camParent.transform;		//make this camera a child of the new gameObject
		camParent.transform.parent = transform;	//make the new gameobject a child of the original camera parent if it had one

	}


	void LateUpdate () {
		MouseFollow ();
		CameraZoom ();
		CameraRotation ();
	}


	void MouseFollow(){
		//HORIZONTAL
		if(Input.mousePosition.x<0)
			transform.position=transform.position-new Vector3(moveSpeed,0,0);
		if(Input.mousePosition.x>Screen.width)
			transform.position=transform.position+new Vector3(moveSpeed,0,0);

		//VERTICAL
		if(Input.mousePosition.y<0)
			transform.position=transform.position-new Vector3(0,0,moveSpeed);
		if(Input.mousePosition.y>Screen.height)
			transform.position=transform.position+new Vector3(0,0,moveSpeed);
	}

	void CameraZoom(){
		//ZOOM IN
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			transform.position =transform.position+ new Vector3(0,-zoomSpeed,zoomSpeed);
		}

		//ZOOM OUT
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			transform.position =transform.position+ new Vector3(0,zoomSpeed,-zoomSpeed);
		}
	}

	void CameraRotation(){
		if (Input.GetMouseButtonDown(1))
		{
			oldInputPosition = Input.mousePosition;
		}
		if (Input.GetMouseButton(1))
		{
			float xDif = Input.mousePosition.x - oldInputPosition.x;
			if(!naturalMotion){xDif *= -1;}
			if(xDif != 0){camParent.transform.Rotate(Vector3.up * xDif * rotSpeed);}
			oldInputPosition = Input.mousePosition;
		}

	}

	public void setPivotPoint(Transform value){
		pivotPoint = value;
	}
}
