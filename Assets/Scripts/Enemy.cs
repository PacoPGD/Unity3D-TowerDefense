using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int x; //X coordinate in the board of this enemy
	private int z; //Z coordinate in the board of this enemy

	private Vector2 position;

	Graph myGraph;

	// Use this for initialization
	void Start () {
		routeCalculation ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	static public void routeCalculation(){

	}

	public void setPosition(Vector2 value){
		position = value;
	}
	
}
