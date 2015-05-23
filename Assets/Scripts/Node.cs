using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	
	public int [,] myBoardStatus;

	public int x; //X coordinate in the board of this enemy
	public int z; //Z coordinate in the board of this enemy
	public List<Node> next = new List<Node>(); //List of next nodes
	public Node previous=null;

	private static int GOAL = 0;
	private static int FREE = 1;
	private static int CHECKED= 2;
	private static int FINALIZED= 3;
	//private static int BLOCK= 4;

	public Node(){
	}

	public Node(int valueX, int valueZ){
		x = valueX;
		z = valueZ;
	}

	public void findNeighbors(int [,] value){
		myBoardStatus = value;

		if (getNodeStatus () == GOAL) {
			Debug.Log ("success");
		} 

		else {
			if(previous != null && getNodeStatus()==FINALIZED){

			}
			else{
				setNodeStatus (CHECKED);
				Debug.Log ("check " + (x) + " " + (z));
				if(previous==null){
					if(!addNextNode ()){
						Debug.Log ("finish " + (x) + " " + (z));
						setNodeStatus (FINALIZED);
						for (int i=0; i<next.Count; i++) {
							next [i].findNeighbors (myBoardStatus);
						}	
					}
				}
				else{
					if (previous.getNodeStatus () != FINALIZED) {
						previous.findNeighbors (myBoardStatus);
					} 
					else {
						if(!addNextNode ()){
							Debug.Log ("finish " + (x) + " " + (z));
							setNodeStatus (FINALIZED);
							for (int i=0; i<next.Count; i++) {
								next [i].findNeighbors (myBoardStatus);
							}	
						}
					}
				}
			}

		}
	}

	public bool addNextNode(){
		Node aux;
		for(int i=-1;i<=1;i++){
			for(int j=-1;j<=1;j++){
				if(inBoard (x+i,z+j)){
					if (myBoardStatus [x+i,z+j] == FREE) {
						aux = new Node (x+i, z+j);
						next.Add (aux);
						aux.previous = this;
						aux.findNeighbors (myBoardStatus);
						return true;
					}
				}
			}
		}

		return false;
	}
	
	public void setNodeStatus(int value){
		myBoardStatus[x,z] = value;
	}

	public int getNodeStatus(){
		return myBoardStatus[x,z];
	}

	public bool inBoard(int x, int z){
		if (z  >= 0 && z  < gridStatus.zSize && x >= 0 && x  < gridStatus.xSize) {
			return true;
		} else {
			return false;
		}
	}

}
