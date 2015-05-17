using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float moveSpeed = 2.0f;	// Speed of camera following mouse
	public float zoomSpeed = 4.0f; // Speed of camera zoom


	// Update is called once per frame
	void Update () {

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

	}
}
