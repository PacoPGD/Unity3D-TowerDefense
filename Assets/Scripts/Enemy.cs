using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : MonoBehaviour {

	public float velocity;

	public int maxLife;

	public GameObject lifePortion;

	private GameObject [] lifeBar;
	

	private int life;

	private static int GOAL = 0;
	private static int FREE = 1;
	private static int BLOCK= 2;

	private int x; //X coordinate in the board of this enemy
	private int z; //Z coordinate in the board of this enemy
	
	private Stack<int> routeX = new Stack<int>();//X coordinate in the board of destiny
	private Stack<int> routeZ = new Stack<int>();//Z coordinate in the board of destiny

	private static int CALCULATING = 3;
	private static int MOVING = 4;

	private int action=CALCULATING;

	private float timeRecalculating;
	private float nextCalculating =1;

	// Use this for initialization
	void Start () {
		life = maxLife;
		lifeBar = new GameObject[maxLife];
		initLifeBar ();
	}

	// Update is called once per frame
	void Update () {
		if (action == CALCULATING) {
			timeRecalculating=Time.time+nextCalculating;
			routeCalculation ();
			action = MOVING;
		} 
		else if (action == MOVING) {
			move ();
		}

		if (Time.time >= timeRecalculating)
		{
			action = CALCULATING;
		}
		
		paintLife ();
		checkDie ();

	}



	public void routeCalculation(){
		Node [,] myNode;
		Queue<Node> tail = new Queue<Node>();
		Node checking;
		Node neighbor;
		Node goal=null;
		double distanceBetweenSquare;
		gridStatus myGridStatus = new gridStatus();

		myGridStatus.copyStatus ();
		myNode = new Node[gridStatus.xSize, gridStatus.zSize];
		
		for (int i=0; i<gridStatus.xSize; i++) {
			for(int j=0; j<gridStatus.zSize; j++){
				if(myGridStatus.personalStatus[i,j]==gridStatus.Status.Crystal)
					myNode[i,j] = new Node(GOAL,i,j);
				else if(myGridStatus.personalStatus[i,j]==gridStatus.Status.Free)
					myNode[i,j] = new Node(FREE,i,j);
				else{
					myNode[i,j] = new Node(BLOCK,i,j);
				}
			}
		}
		
		
		//Add the father element 
		tail.Enqueue (myNode [x, z]);
		myNode [x, z].distance = 0;
		
		while (tail.Count!=0) {
			checking = tail.Dequeue ();

			
			for(int i=-1;i<=1;i++){
				for(int j=-1;j<=1;j++){
					if(inBoard (checking.x+i,checking.z+j)){
						if(i==0 && j==0){
							distanceBetweenSquare = 1.0f;
						}
						else{
							distanceBetweenSquare = 1.41f;
						}

						neighbor = myNode[checking.x+i,checking.z+j];

						if((neighbor.distance)>(checking.distance+distanceBetweenSquare))
						{
							if(neighbor.myStatus == FREE){
									neighbor.distance = checking.distance+distanceBetweenSquare;
									neighbor.previous = checking;
									tail.Enqueue (neighbor);
							}
							
							else if (neighbor.myStatus==GOAL){
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


	public void cleanRoute() {
		routeX.Clear ();
		routeZ.Clear ();
	}

	public void writeRoute(Node destiny){
		Node aux;
		aux = destiny;

		cleanRoute ();

		if (destiny != null) {
			while (aux.previous!=null) {
				routeX.Push (aux.x);
				routeZ.Push (aux.z);
				aux = aux.previous;
			}
		}

	}
	

	//MOVING
	public void move(){
		int xDestiny;
		int zDestiny;
		if (routeX.Count != 0) {
			xDestiny = routeX.Peek ();
			zDestiny = routeZ.Peek ();
			goSquare (xDestiny, zDestiny);

			if (x == xDestiny && z == zDestiny) {
				routeX.Pop ();
				routeZ.Pop ();
			}
		} 
	}

	public void goSquare(int squareX, int squareZ){
		if (x < squareX)
			transform.Translate(new Vector3 (1, 0, 0) * Time.deltaTime * velocity);
		if (x > squareX)
			transform.Translate(new Vector3 (-1, 0, 0) * Time.deltaTime * velocity);
		if (z < squareZ)
			transform.Translate(new Vector3 (0, 0, 1) * Time.deltaTime * velocity);
		if (z > squareZ)
			transform.Translate(new Vector3 (0, 0, -1) * Time.deltaTime * velocity);
	}

	//BATTLE
	public void ApplyDamage(int damage){
		life = life - damage;
	}

	public void checkDie(){
		if(life<=0)
			Destroy(gameObject);
	}

	//LIFEBAR
	public void initLifeBar(){
		for (int i=0; i<maxLife; i++) {
			lifeBar[i] = (GameObject)Instantiate(lifePortion);
			lifeBar[i].GetComponent<Renderer>().material.color = Color.red;
			lifeBar[i].transform.position = (transform.position+ new Vector3(i-5,5,0));
		}
	}

	public void paintLife(){
		for (int i=0; i<life; i++) {
			lifeBar[i].transform.position = (transform.position+ new Vector3(i-5,5,0));
		}
		for (int i=life; i<maxLife; i++) {
			Destroy(lifeBar[i].gameObject);
		}
	}
	


	//AUXILIARS
	public void setX(int value){
		x = value;
	}

	public void setZ(int value){
		z = value;
	}

	public bool inBoard(int x, int z){
		if (z >= 0 && z < gridStatus.zSize && x >= 0 && x < gridStatus.xSize) 
			return true;
		else
			return false;
	}

	public void logBoard(Node[,] myNode){
		for (int i=0; i<gridStatus.xSize; i++) {
			for(int j=0; j<gridStatus.zSize; j++){
				Debug.Log ("Node "+i+" "+j+" "+myNode[i,j].myStatus);
			}
		}
	}
}
