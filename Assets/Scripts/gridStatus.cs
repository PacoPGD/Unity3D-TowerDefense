using UnityEngine;
using System.Collections;

//This class is the static gridStatus
public class gridStatus{
	public enum Status
	{
		Crystal, //It indicates the square have a crystal
		Free,  //It indicates if the square is free
		Tower, //It indicates the square have a tower
		Generator, // It indicates the square generate enemies
	};
	
	public static Status [,] myStatus;

	public static int xSize;
	public static int zSize;

	public static int towerSelection;

}
