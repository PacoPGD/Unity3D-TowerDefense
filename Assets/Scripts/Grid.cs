using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	private Square board;

	void Start(){

	}

	void Update(){
	
	}

	void OnPostRender(){
		board.OnPostRender ();
	}

}