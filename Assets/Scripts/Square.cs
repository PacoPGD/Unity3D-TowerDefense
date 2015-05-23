using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {
	enum Status
	{
		Crystal, //It indicates the square have a crystal
		Free,  //It indicates if the square is free
		Tower, //It indicates the square have a tower
		Generator, // It indicates the square generate enemies
	};


	public GameObject crystal; //A crystal GameObject, represent a crystal
	public GameObject blueCannonTurret; // blueCannonTurret GameObject, represent a Blue Cannon Turret
	public GameObject enemyNormal; // enemyNormal GameObject, represent a Normal Enemy

<<<<<<< HEAD
=======
	private bool isBuildable = true; //It indicates if the square is buildable
	private Status myStatus=Status.Free;

>>>>>>> parent of 8ca0939... Static boardStatus added
	private int x;
	private int z;

	void Start(){
		if(myStatus!=Status.Generator)
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseDown() {
		if (myStatus == Status.Free) {
			blueCannonTurret = (GameObject)Instantiate (blueCannonTurret);
			blueCannonTurret.transform.position += transform.position;
			gridStatus.myStatus[x,z] = gridStatus.Status.Tower;
		}

	}


	void OnMouseOver() {
		if(myStatus==Status.Free)
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseExit() {
		if(myStatus==Status.Generator)
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.white;
	}



	public bool generateCrystal(){
		if (myStatus==Status.Free) {
			crystal = (GameObject)Instantiate (crystal);
			crystal.transform.position += transform.position;
			gridStatus.myStatus[x,z]=gridStatus.Status.Crystal;
			return true;
		} else
			return false;
	}

	public void generateEnemy(){
		if (myStatus == Status.Generator) {
			enemyNormal = (GameObject)Instantiate (enemyNormal);
			enemyNormal.transform.position = transform.position;
		}
	}

	public void setGenerator(){
		myStatus = Status.Generator;
	}

	public int setStatus(){
		if (myStatus == Status.Crystal)
			return 0;
		else if (myStatus == Status.Free)
			return 1;
		else
			return 2;
	}

	public void setX(int value){
		x = value;
	}
	
	public void setZ(int value){
		z = value;
	}

	void OnTriggerEnter(Collider other){
		other.gameObject.GetComponent<Enemy>().setX (x);
		other.gameObject.GetComponent<Enemy>().setZ (z);
	}

}
