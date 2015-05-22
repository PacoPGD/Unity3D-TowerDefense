using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	public Enemy myEnemy;

	public int x; //X coordinate in the board of this enemy
	public int z; //Z coordinate in the board of this enemy
	public List<Node> next = new List<Node>(); //List of next nodes
	public Node previous=null;

	public Node(Enemy value){
		myEnemy = value;
	}

	public Node(Enemy value,int valueX, int valueZ){
		myEnemy = value;
		x = valueX;
		z = valueZ;
	}

	public void findNeighbors(){
		Debug.Log ("Comprobado nodo "+x+" "+z);
		if (myEnemy.myBoardStatus [x, z] == 0) {
			Debug.Log ("exito");
		} 
		else {
			myEnemy.myBoardStatus [x, z] = 2;
			if(z-1>=0)
				if(myEnemy.myBoardStatus[x,z-1]==1)
					next.Add (new Node(myEnemy,x,z-1));
			if(z+1<gridStatus.zSize)
				if(myEnemy.myBoardStatus[x,z+1]==1)
					next.Add (new Node(myEnemy,x,z+1));
			if(x-1>=0)
				if(myEnemy.myBoardStatus[x-1,z]==1)
					next.Add (new Node(myEnemy,x-1,z));
			if(x+1<gridStatus.xSize)
			if(myEnemy.myBoardStatus[x+1,z]==1)
				next.Add (new Node(myEnemy,x+1,z));
			
			for (int i=0; i<next.Count; i++) {
				next[i].previous=this;
				next[i].findNeighbors();
			}
		}

	}

}
