﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int x; //X coordinate in the board of this enemy
	private int z; //Z coordinate in the board of this enemy

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

	public void setX(int value){
		x = value;
	}

	public void setZ(int value){
		z = value;
	}
	
}
