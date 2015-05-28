using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {
	
	public GameObject Crystal; //A crystal GameObject, represent a crystal
	public GameObject BlueCannonTurret; // blueCannonTurret GameObject, represent a Blue Cannon Turret
	public GameObject RedLaserTurret; // redLaserTurret GameObject, represent a Red Laser Turret
	public GameObject EnemyNormal; // enemyNormal GameObject, represent a Normal Enemy
	public GameObject EnemySwift; // enemySwift GameObject, represent a Normal Enemy
	public GameObject EnemyArmoured; // enemyArmoured GameObject, represent a Normal Enemy

	private int x;
	private int z;
	
	void Start()
	{
		if(gridStatus.myStatus[x,z]!=gridStatus.Status.Generator)
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseDown() 
	{
		if (gridStatus.myStatus[x,z] == gridStatus.Status.Free) 
		{
			if(gridStatus.towerSelection==1)
			{
				BlueCannonTurret = (GameObject)Instantiate (BlueCannonTurret);
				BlueCannonTurret.transform.position += transform.position;
				gridStatus.myStatus[x,z] = gridStatus.Status.Tower;
			}
			else
			{
				RedLaserTurret = (GameObject)Instantiate (RedLaserTurret);
				RedLaserTurret.transform.position += transform.position;
				gridStatus.myStatus[x,z] = gridStatus.Status.Tower;
			}
		}

	}
	
	void OnMouseOver() 
	{
		if(gridStatus.myStatus[x,z]==gridStatus.Status.Free)
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseExit() 
	{
		if(gridStatus.myStatus[x,z]==gridStatus.Status.Generator)
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		else
			gameObject.GetComponent<Renderer>().material.color = Color.white;
	}


	//Generate a crystal in this square
	public bool generateCrystal()
	{
		if (gridStatus.myStatus[x,z]==gridStatus.Status.Free)
		{
			Crystal = (GameObject)Instantiate (Crystal);
			Crystal.transform.position += transform.position;
			gridStatus.myStatus[x,z]=gridStatus.Status.Crystal;
			return true;
		} 
		else
			return false;
	}

	//Generate a enemy in this square
	public void generateEnemy()
	{
		int selectEnemy;
		if (gridStatus.myStatus [x, z] == gridStatus.Status.Generator) 
		{
			selectEnemy=(int)Random.Range (0,3);
			if(selectEnemy==0)
				generateNormalEnemy ();
			else if(selectEnemy==1)
				generateSwiftEnemy ();
			else
				generateArmouredEnemy ();
		}
	}

	public void generateNormalEnemy()
	{
		EnemyNormal = (GameObject)Instantiate (EnemyNormal);
		EnemyNormal.transform.position = transform.position;
	}

	public void generateSwiftEnemy()
	{
		EnemySwift = (GameObject)Instantiate (EnemySwift);
		EnemySwift.transform.position = transform.position;
	}

	public void generateArmouredEnemy()
	{
		EnemyArmoured = (GameObject)Instantiate (EnemyArmoured);
		EnemyArmoured.transform.position = transform.position;
	}

	public void setX(int value)
	{
		x = value;
	}
	
	public void setZ(int value)
	{
		z = value;
	}

	//Send the x and Z to the enemy
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy> ())
		{
			other.gameObject.GetComponent<Enemy> ().setX (x);
			other.gameObject.GetComponent<Enemy> ().setZ (z);
		}
	}

}
