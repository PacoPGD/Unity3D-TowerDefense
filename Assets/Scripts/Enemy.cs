using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float velocity=0.2f;

	public int [,] myBoardStatus;

	private int x; //X coordinate in the board of this enemy
	private int z; //Z coordinate in the board of this enemy

	private Node myNode;

	// Use this for initialization
	void Start () {
		myNode = new Node(this);
		myBoardStatus = new int[gridStatus.xSize, gridStatus.zSize];
		loadBoardStatus ();
		routeCalculation ();
	}

	// Update is called once per frame
	void Update () {
		goSquare (5, 5);
	}

	public void routeCalculation(){
		myNode.x = x;
		myNode.z = z;

		myNode.findNeighbors ();
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
					myBoardStatus[i,j]=0;
				else if(gridStatus.myStatus[i,j]==gridStatus.Status.Free)
					myBoardStatus[i,j]=1;
				else
					myBoardStatus[i,j]=2;
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
