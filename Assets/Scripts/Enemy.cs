using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Enemy : MonoBehaviour {

	public float velocity=0.2f;

	public int maxLife = 10;

	public int life;

	public int [,] myBoardStatus;

	private static int GOAL = 0;
	private static int FREE = 1;
	private static int CHECKED= 2;
	private static int BLOCK= 3;

	private int x; //X coordinate in the board of this enemy
	private int z; //Z coordinate in the board of this enemy

	private Node [,] myNode;

	// Use this for initialization
	void Start () {
		life = maxLife;

		myBoardStatus = new int[gridStatus.xSize, gridStatus.zSize];
		myNode = new Node[gridStatus.xSize, gridStatus.zSize];
		loadBoardStatus ();
		routeCalculation ();

	}

	// Update is called once per frame
	void Update () {
		goSquare (2, 2);
	}

	public void routeCalculation(){
		List<Node> tail = new List<Node>();
		Node checking;

		for (int i =0; i<gridStatus.xSize; i++) {
			for(int j=0; j<gridStatus.zSize;j++){
				myNode[i,j] = new Node(FREE,i,j);
			}
		}

		//Add the father element 
		tail.Add (myNode [x, z]);
		myNode [x, z].myStatus = CHECKED;

		while (tail.Count!=0) {
			checking = tail[0];
			tail.RemoveAt (0);
			//Debug.Log ("Checking "+ (checking.x) +" "+(checking.z));

			for(int i=-1;i<=1;i++){
				for(int j=-1;j<=1;j++){
					if(inBoard (checking.x+i,checking.z+j)){
						if(myNode[checking.x+i,checking.z+j].myStatus==FREE){
							myNode[checking.x+i,checking.z+j].myStatus=CHECKED;
							myNode[checking.x+i,checking.z+j].distance=checking.distance+1;
							myNode[checking.x+i,checking.z+j].previous = checking;
							tail.Add (myNode[checking.x+i,checking.z+j]);
							
							//Debug.Log ("Add "+ (checking.x+i) +" "+(checking.z+j));
						}
					}
				}
			}
					
		}


	}
	

	public bool inBoard(int x, int z){
		if (z >= 0 && z < gridStatus.zSize && x >= 0 && x < gridStatus.xSize) 
			return true;
	    else
			return false;
	}


	public void goSquare(int squareX, int squareZ){
		if (x < squareX)
			transform.position += new Vector3 (velocity, 0, 0);
		if (x > squareX)
			transform.position -= new Vector3 (velocity, 0, 0);
		if (z < squareZ)
			transform.position += new Vector3 (0, 0, velocity);
		if (z > squareZ)
			transform.position -= new Vector3 (0, 0, velocity);
	}


	public void loadBoardStatus(){
		for (int i=0; i<gridStatus.xSize; i++) {
			for(int j=0; j<gridStatus.zSize; j++){
				if(gridStatus.myStatus[i,j]==gridStatus.Status.Crystal)
					myBoardStatus[i,j]=GOAL;
				else if(gridStatus.myStatus[i,j]==gridStatus.Status.Free)
					myBoardStatus[i,j]=FREE;
				else
					myBoardStatus[i,j]=BLOCK;
			}
		}
	}



	public void setX(int value){
		x = value;
	}

	public void setZ(int value){
		z = value;
	}

}
