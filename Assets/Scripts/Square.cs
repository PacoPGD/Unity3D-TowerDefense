using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {
	enum Status
	{
		Free,  //It indicates if the square is free
		Crystal, //It indicates the square have a crystal
		Tower, //It indicates the square have a tower
		Generator, // It indicates the square generate enemies
	};


	public GameObject crystal; //A crystal GameObject, represent a crystal

	private bool isBuildable = true; //It indicates if the square is buildable
	
	private Status myStatus=Status.Free;
	private int x; //x coordinate in the board
	private int z; //z coordinate in the board
	
	void OnMouseEnter() {
		if(myStatus==Status.Free)
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseExit() {
		if(myStatus!=Status.Generator)
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void Start(){
		if(myStatus!=Status.Generator)
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	public bool generateCrystal(){
		if (myStatus==Status.Free) {
			crystal = (GameObject)Instantiate (crystal);
			crystal.transform.position += transform.position;
			myStatus=Status.Crystal;
			return true;
		} else
			return false;
	}

	public void setGenerator(){
		myStatus = Status.Generator;
	}


	public int getX(){
		return x;
	}
	
	public void setX(int value){
		x = value;
	}
	public int getZ(){
		return z;
	}
	
	public void setZ(int value){
		z = value;
	}

}
