using UnityEngine;
using System.Collections;

public class Node {
	private static int INFINITE = 999;
	//private static int GOAL = 0;
	private static int FREE = 1;
	//private static int CHECKED= 2;

	public int x;
	public int z;

	public int myStatus = FREE;
	public int distance = INFINITE;
	public Node previous=null;

	public Node(int status, int valueX, int valueZ){
		myStatus = status;
		x = valueX;
		z = valueZ;
	}

}
