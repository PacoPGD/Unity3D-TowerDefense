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
		if (getNodeStatus () == GOAL) {
			Debug.Log ("exito");
		} 

		else {
			setNodeStatus (CHECKED);
			Debug.Log ("Chequeado " + (x) + " " + (z));
			if(previous==null){
				if(!addNextNode ()){
					setNodeStatus (FINALIZED);
					for (int i=0; i<next.Count; i++) {
						next [i].findNeighbors ();
					}	
				}
			}
			else{
				if (previous.getNodeStatus () != FINALIZED) {
					previous.findNeighbors ();
				} 
				else {
					if(!addNextNode ()){
						setNodeStatus (FINALIZED);
						for (int i=0; i<next.Count; i++) {
							next [i].findNeighbors ();
						}	
					}
				}
			}


		}
	}

	public bool addNextNode(){
		Node aux;
		for(int i=-1;i<=+1;i++){
			for(int j=-1;j<=+1;j++){
				if(inBoard (x+i,z+j)){
					if (myEnemy.myBoardStatus [x+i,z+j] == FREE) {
						aux = new Node (myEnemy, x+i, z+j);
						next.Add (aux);
						aux.previous = this;
						aux.findNeighbors ();
						return true;
					}
				}
			}
		}

		return false;
		/*
		if (z - 1 >= 0) {
			if (myEnemy.myBoardStatus [x, z - 1] == FREE) {
				
				Debug.Log ("Añado nodo " + x + " " + (z-1));
				aux = new Node (myEnemy, x, z - 1);
				next.Add (aux);
				aux.previous = this;
				aux.findNeighbors ();
				return true;
			}
		} 
		else if (z + 1 < gridStatus.zSize) {
			if (myEnemy.myBoardStatus [x, z + 1] == FREE) {
				Debug.Log ("Añado nodo " + x + " " + (z+1));
				aux = new Node (myEnemy, x, z + 1);
				next.Add (aux);
				aux.previous = this;
				aux.findNeighbors ();
				return true;
			}
		} 
		else if (x - 1 >= 0) {
			if (myEnemy.myBoardStatus [x - 1, z] == FREE) {
				Debug.Log ("Añado nodo " + (x-1) + " " + z);
				aux = new Node (myEnemy, x - 1, z);
				next.Add (aux);
				aux.previous = this;
				aux.findNeighbors ();
				return true;
			}
		} 
		else if (x + 1 < gridStatus.xSize) {
			if (myEnemy.myBoardStatus [x + 1, z] == FREE) {
				Debug.Log ("Añado nodo " + (x+1) + " " + z);
				aux = new Node (myEnemy, x + 1, z);
				next.Add (aux);
				aux.previous = this;
				aux.findNeighbors ();
				return true;
			}
		} 
		return false;*/

	}
	
	public void setNodeStatus(int value){
		myEnemy.myBoardStatus[x,z] = value;
	}

	public int getNodeStatus(){
		return myEnemy.myBoardStatus[x,z];
	}

	public bool inBoard(int x, int z){
		if (z - 1 >= 0 && z + 1 < gridStatus.zSize && x - 1 >= 0 && x + 1 < gridStatus.xSize) {
			return true;
		} else {
			return false;
		}
	}

}
