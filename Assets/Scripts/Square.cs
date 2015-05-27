using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {
	
	public GameObject crystal; //A crystal GameObject, represent a crystal
	public GameObject blueCannonTurret; // blueCannonTurret GameObject, represent a Blue Cannon Turret
	public GameObject redLaserTurret; // redLaserTurret GameObject, represent a Red Laser Turret
	public GameObject enemyNormal; // enemyNormal GameObject, represent a Normal Enemy
	public GameObject enemySwift; // enemySwift GameObject, represent a Normal Enemy
	public GameObject enemyArmoured; // enemyArmoured GameObject, represent a Normal Enemy

	private int x;
	private int z;
	
	void Start(){
		if(gridStatus.myStatus[x,z]!=gridStatus.Status.Generator)
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseDown() {
		if (gridStatus.myStatus[x,z] == gridStatus.Status.Free) {
			if(gridStatus.towerSelection==1){
				blueCannonTurret = (GameObject)Instantiate (blueCannonTurret);
				blueCannonTurret.transform.position += transform.position;
				gridStatus.myStatus[x,z] = gridStatus.Status.Tower;
			}
			else{
				redLaserTurret = (GameObject)Instantiate (redLaserTurret);
				redLaserTurret.transform.position += transform.position;
				gridStatus.myStatus[x,z] = gridStatus.Status.Tower;
			}
		}

	}
	
	void OnMouseOver() {
		if(gridStatus.myStatus[x,z]==gridStatus.Status.Free)
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseExit() {
		if(gridStatus.myStatus[x,z]==gridStatus.Status.Generator)
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.white;
	}


	//Generate a crystal in this square
	public bool generateCrystal(){
		if (gridStatus.myStatus[x,z]==gridStatus.Status.Free) {
			crystal = (GameObject)Instantiate (crystal);
			crystal.transform.position += transform.position;
			gridStatus.myStatus[x,z]=gridStatus.Status.Crystal;
			return true;
		} else
			return false;
	}

	//Generate a enemy in this square
	public void generateEnemy(){
		int selectEnemy;
		if (gridStatus.myStatus [x, z] == gridStatus.Status.Generator) {
			selectEnemy=(int)Random.Range (0,3);
			if(selectEnemy==0)
				generateNormalEnemy ();
			else if(selectEnemy==1)
				generateSwiftEnemy ();
			else
				generateArmouredEnemy ();
		}
	}

	public void generateNormalEnemy(){
		enemyNormal = (GameObject)Instantiate (enemyNormal);
		enemyNormal.transform.position = transform.position;
	}

	public void generateSwiftEnemy(){
		enemySwift = (GameObject)Instantiate (enemySwift);
		enemySwift.transform.position = transform.position;
	}

	public void generateArmouredEnemy(){
		enemyArmoured = (GameObject)Instantiate (enemyArmoured);
		enemyArmoured.transform.position = transform.position;
	}

	public void setX(int value){
		x = value;
	}
	
	public void setZ(int value){
		z = value;
	}

	//Send the x and Z to the enemy
	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<Enemy> ()) {
			other.gameObject.GetComponent<Enemy> ().setX (x);
			other.gameObject.GetComponent<Enemy> ().setZ (z);
		}
	}

}
