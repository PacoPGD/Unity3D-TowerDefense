using UnityEngine;
using System.Collections;

public class Node {
	private static int INFINITE = 999;
	
	public int x;
	public int z;
	public int myStatus;

	public double distance = INFINITE;
	public Node previous=null;

	public Node(int status, int valueX, int valueZ){
		myStatus = status;
		x = valueX;
		z = valueZ;
	}

}
