using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	public Enemy myEnemy;

	public int x; //X coordinate in the board of this enemy
	public int z; //Z coordinate in the board of this enemy
	public List<Node> next = new List<Node>(); //List of next nodes
	public Node previous=null;

	private static int GOAL = 0;
	private static int FREE = 1;
	private static int CHECKED= 2;
	private static int FINALIZED= 3;
	//private static int BLOCK= 4;

	public Node(Enemy value){
		myEnemy = value;
	}

	public Node(Enemy value,int valueX, int valueZ){
		myEnemy = value;
		x = valueX;
		z = valueZ;
	}

	public void findNeighbors(){
		Node aux;

		Debug.Log ("Comprobado nodo " + x + " " + z);
		if (getNodeStatus () == GOAL) {
			Debug.Log ("exito");
		} else {
			setNodeStatus (CHECKED);
			if (previous != null || previous.getNodeStatus () != FINALIZED) {
				previous.findNeighbors ();
			} 
			else {
				if (z - 1 >= 0) {
					if (myEnemy.myBoardStatus [x, z - 1] == FREE) {
						aux = new Node (myEnemy, x, z - 1);
						next.Add (aux);
						aux.previous = this;
						aux.findNeighbors ();
					}
				} else if (z + 1 < gridStatus.zSize) {
					if (myEnemy.myBoardStatus [x, z + 1] == FREE) {
						aux = new Node (myEnemy, x, z + 1);
						next.Add (aux);
						aux.previous = this;
						aux.findNeighbors ();
					}
				} else if (x - 1 >= 0) {
					if (myEnemy.myBoardStatus [x - 1, z] == FREE) {
						aux = new Node (myEnemy, x - 1, z);
						next.Add (aux);
						aux.previous = this;
						aux.findNeighbors ();
					}
				} else if (x + 1 < gridStatus.xSize) {
					if (myEnemy.myBoardStatus [x + 1, z] == FREE) {
						aux = new Node (myEnemy, x + 1, z);
						next.Add (aux);
						aux.previous = this;
						aux.findNeighbors ();
					}
				} else {
					setNodeStatus (FINALIZED);
					for (int i=0; i<next.Count; i++) {
						next [i].findNeighbors ();
					}	
				}
			}
		}
	}


	public void setNodeStatus(int value){
		myEnemy.myBoardStatus[x,z] = value;
	}

	public int getNodeStatus(){
		return myEnemy.myBoardStatus[x,z];
	}

	/*
	public void findNeighbors(){
		Debug.Log ("Comprobado nodo "+x+" "+z);
		if (myEnemy.myBoardStatus [x, z] == GOAL) {
			Debug.Log ("exito");
		} 
		else {
			myEnemy.myBoardStatus [x, z] = CHECKED;
			if(z-1>=0)
				if(myEnemy.myBoardStatus[x,z-1]==FREE)
					next.Add (new Node(myEnemy,x,z-1));
			if(z+1<gridStatus.zSize)
				if(myEnemy.myBoardStatus[x,z+1]==FREE)
					next.Add (new Node(myEnemy,x,z+1));
			if(x-1>=0)
				if(myEnemy.myBoardStatus[x-1,z]==FREE)
					next.Add (new Node(myEnemy,x-1,z));
			if(x+1<gridStatus.xSize)
				if(myEnemy.myBoardStatus[x+1,z]==FREE)
					next.Add (new Node(myEnemy,x+1,z));
			
			for (int i=0; i<next.Count; i++) {
				next[i].previous=this;
				next[i].findNeighbors();
			}
		}

	}*/

}
