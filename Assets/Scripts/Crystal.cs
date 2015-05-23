using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<Enemy> ()) {
			Application.LoadLevel("GameOverScene"); 
		}
	}
}
