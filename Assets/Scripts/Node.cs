using UnityEngine;
using System.Collections;

public class Node {
	public int x;
	public int z;
	public int myStatus;

	public double distance = Mathf.Infinity;
	public Node previous=null;

	public Node(int status, int valueX, int valueZ){
		myStatus = status;
		x = valueX;
		z = valueZ;
	}

}
