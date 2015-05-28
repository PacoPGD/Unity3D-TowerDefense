using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : MonoBehaviour {

	//Static variables
	//The status in the grid
	private static int GOAL = 0; //Crystal
	private static int FREE = 1; //Free square
	private static int BLOCK= 2; //Square with obstacle

	//The action of the enemy
	private static int CALCULATING = 3; //Calculating route
	private static int MOVING = 4; //Moving

	//Public variables
	public float Velocity; //velocity in game of enemy
	public int MaxLife; //Max life of enemy
	public GameObject LifePortion; //Object for representing a lifePortion
	public float LifeBarSize; //Size of lifeBar

	//Private variables
	private int life;//Actual life
	private int x; //X coordinate in the board of this enemy
	private int z; //Z coordinate in the board of this enemy
	private Stack<int> routeX = new Stack<int>();//X coordinate in the board of destiny
	private Stack<int> routeZ = new Stack<int>();//Z coordinate in the board of destiny
	private int action=CALCULATING;//Action of the enemy
	private float nextCalculating;//Time between calculations
	private float timeRecalculating;//Time of next calculate


	void Start () 
	{
		nextCalculating = 1;
		life = MaxLife;
		initLifeBar ();
	}


	void Update () 
	{
		//Action depens of action variable
		if (action == CALCULATING) 
		{
			timeRecalculating=Time.time+nextCalculating;
			routeCalculation ();
			action = MOVING;
		} 
		else if (action == MOVING) 
		{
			move ();
		}

		//whenever time passes we are in CALCULATING status
		if (Time.time >= timeRecalculating)
		{
			action = CALCULATING;
		}
		
		paintLife (); 
		checkDie (); 
	}


	/**************************************
	 * This is the BFS algorithm, this function calcule the final node and the best route
	 * 
	 ***************************************/

	public void routeCalculation(){
		Node [,] myNode;
		Queue<Node> tail = new Queue<Node>(); //Queue for check squares
		Node checking; //The checking node
		Node neighbor; //Neighbor of checking node
		Node goal=null; //Node of destiny
		double distanceBetweenSquare;// Distance between 2 squares

		//Save the gridStatus in myNode variable
		myNode = new Node[gridStatus.xSize, gridStatus.zSize];
		
		for (int i=0; i<gridStatus.xSize; i++) 
		{
			for(int j=0; j<gridStatus.zSize; j++)
			{
				if(gridStatus.myStatus[i,j]==gridStatus.Status.Crystal)
					myNode[i,j] = new Node(GOAL,i,j);
				else if(gridStatus.myStatus[i,j]==gridStatus.Status.Free)
					myNode[i,j] = new Node(FREE,i,j);
				else
					myNode[i,j] = new Node(BLOCK,i,j);

			}
		}
		
		
		//Add the father element in the queue
		tail.Enqueue (myNode [x, z]);
		myNode [x, z].distance = 0;
		
		while (tail.Count!=0) {
			checking = tail.Dequeue ();
	
			//Check the neighbors of checking node
			for(int i=-1;i<=1;i++)
			{
				for(int j=-1;j<=1;j++)
				{
					if(inBoard (checking.x+i,checking.z+j))
					{
						//Distances between squares
						if(i==0 && j==0)
							distanceBetweenSquare = 1.0f; //Next to square
						else
							distanceBetweenSquare = 1.41f; //Diagonal


						neighbor = myNode[checking.x+i,checking.z+j];

						if((neighbor.distance)>(checking.distance+distanceBetweenSquare))
						{
							if(neighbor.myStatus == FREE)
							{
									neighbor.distance = checking.distance+distanceBetweenSquare;
									neighbor.previous = checking;
									tail.Enqueue (neighbor);
							}
							
							else if (neighbor.myStatus==GOAL)
							{
									neighbor.distance=checking.distance+distanceBetweenSquare;
									neighbor.previous = checking;
									goal = neighbor;
							}
						}
					}
				}
			}
			
		}

		writeRoute (goal);
	}

	//Clean the route to a node
	public void cleanRoute() 
	{
		routeX.Clear ();
		routeZ.Clear ();
	}

	//Write the route to a node
	public void writeRoute(Node destiny)
	{
		Node aux;
		aux = destiny;

		cleanRoute ();

		if (destiny != null) 
		{
			while (aux.previous!=null) 
			{
				routeX.Push (aux.x);
				routeZ.Push (aux.z);
				aux = aux.previous;
			}
		}

	}
	

	//MOVING
	//Visit the squares of route
	public void move()
	{
		int xDestiny;
		int zDestiny;
		if (routeX.Count != 0) 
		{
			xDestiny = routeX.Peek ();
			zDestiny = routeZ.Peek ();
			goSquare (xDestiny, zDestiny);

			if (x == xDestiny && z == zDestiny)
			{
				routeX.Pop ();
				routeZ.Pop ();
			}
		} 
	}

	//Go to a specific square
	public void goSquare(int squareX, int squareZ)
	{
		if (x < squareX)
			transform.Translate(new Vector3 (1, 0, 0) * Time.deltaTime * Velocity);
		if (x > squareX)
			transform.Translate(new Vector3 (-1, 0, 0) * Time.deltaTime * Velocity);
		if (z < squareZ)
			transform.Translate(new Vector3 (0, 0, 1) * Time.deltaTime * Velocity);
		if (z > squareZ)
			transform.Translate(new Vector3 (0, 0, -1) * Time.deltaTime * Velocity);
	}

	//BATTLE
	public void ApplyDamage(int damage)
	{
		life = life - damage;
	}

	public void checkDie()
	{
		if (life <= 0) 
		{
			Destroy (LifePortion);
			Destroy (gameObject);
		}
	}

	//LIFEBAR
	public void initLifeBar()
	{
		LifePortion = (GameObject)Instantiate(LifePortion);
		LifePortion.GetComponent<Renderer>().material.color = Color.red;
	
	}

	public void paintLife()
	{
		LifePortion.transform.position = (transform.position + new Vector3(1,5,0));

		LifePortion.transform.localScale= new Vector3((life*LifeBarSize)/MaxLife,1,1);
	}
	


	//AUXILIARS
	public void setX(int value)
	{
		x = value;
	}

	public void setZ(int value)
	{
		z = value;
	}

	//Check if the position exists on the board
	public bool inBoard(int x, int z)
	{
		if (z >= 0 && z < gridStatus.zSize && x >= 0 && x < gridStatus.xSize) 
			return true;
		else
			return false;
	}

	//Print the status of the board
	public void logBoard(Node[,] myNode)
	{
		for (int i=0; i<gridStatus.xSize; i++) 
		{
			for(int j=0; j<gridStatus.zSize; j++)
			{
				Debug.Log ("Node "+i+" "+j+" "+myNode[i,j].myStatus);
			}
		}
	}
}
