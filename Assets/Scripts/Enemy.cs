using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : MonoBehaviour {

	public float velocity=0.1f;

	public int maxLife = 10;

	public int life;
	

	private static int GOAL = 0;
	private static int FREE = 1;
	private static int CHECKED= 2;
	private static int BLOCK= 3;

	private int x; //X coordinate in the board of this enemy
	private int z; //Z coordinate in the board of this enemy
	
	List<int> routeX = new List<int>();//X coordinate in the board of destiny
	List<int> routeZ = new List<int>();//Z coordinate in the board of destiny
	
	// Use this for initialization
	void Start () {

		life = maxLife;

		routeCalculation ();

	}

	// Update is called once per frame
	void Update () {
		move ();
	}

	//BFS ALGORITHM
	public void routeCalculation(){
		Node [,] myNode;
		List<Node> tail = new List<Node>();
		Node checking;

		myNode = new Node[gridStatus.xSize, gridStatus.zSize];

		for (int i=0; i<gridStatus.xSize; i++) {
			for(int j=0; j<gridStatus.zSize; j++){
				if(gridStatus.myStatus[i,j]==gridStatus.Status.Crystal)
					myNode[i,j] = new Node(GOAL,i,j);
				else if(gridStatus.myStatus[i,j]==gridStatus.Status.Free)
					myNode[i,j] = new Node(FREE,i,j);
				else
					myNode[i,j] = new Node(BLOCK,i,j);
			}
		}
		
		
		//Add the father element 
		tail.Add (myNode [x, z]);


		while (tail.Count!=0) {
			checking = tail[0];
			tail.RemoveAt (0);

			//Debug.Log ("CHECKING "+ checking.x +" "+ checking.z);

			for(int i=-1;i<=1;i++){
				for(int j=-1;j<=1;j++){
					if(inBoard (checking.x+i,checking.z+j)){
						if(myNode[checking.x+i,checking.z+j].myStatus==FREE){
							myNode[checking.x+i,checking.z+j].myStatus=CHECKED;
							myNode[checking.x+i,checking.z+j].distance=checking.distance+1;
							myNode[checking.x+i,checking.z+j].previous = checking;
							tail.Add (myNode[checking.x+i,checking.z+j]);
						}
						else if (myNode[checking.x+i,checking.z+j].myStatus==GOAL){
							myNode[checking.x+i,checking.z+j].distance=checking.distance+1;
							myNode[checking.x+i,checking.z+j].previous = checking;
							tail.Clear ();
							//Debug.Log ("SUCCESS"+(checking.x+i)+" "+(checking.z+j)  );
							writeRoute (myNode[checking.x+i,checking.z+j]);
						}
					}
				}
			}
					
		}


	}

	public void writeRoute(Node destiny){
		Node aux;
		aux = destiny;

		while (aux.previous!=null) {
			routeX.Add (aux.x);
			routeZ.Add (aux.z);
			aux = aux.previous;
		}

		routeX.Reverse ();
		routeZ.Reverse ();
	}

	//MOVING
	public void move(){
		if (routeX.Count != 0) {
			goSquare (routeX [0], routeZ [0]);

			if (x == routeX [0] && z == routeZ [0]) {
				routeX.RemoveAt (0);
				routeZ.RemoveAt (0);
			}
		} 
	}

	public void goSquare(int squareX, int squareZ){
		if (x < squareX)
			transform.position += new Vector3 (1, 0, 0);
		if (x > squareX)
			transform.position -= new Vector3 (1, 0, 0);
		if (z < squareZ)
			transform.position += new Vector3 (0, 0, 1);
		if (z > squareZ)
			transform.position -= new Vector3 (0, 0, 1);

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
}
