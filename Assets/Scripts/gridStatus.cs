using UnityEngine;
using System.Collections;

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

	public Status [,] personalStatus;

	public gridStatus(){
		personalStatus = new Status[xSize, zSize];
	}


	public void copyStatus(){
		for (int i=0; i<xSize; i++) {
			for(int j=0; j<zSize;j++){
				personalStatus[i,j]=myStatus[i,j];
			}
		}
	}

	public bool compareStatus(){
		for (int i=0; i<xSize; i++) {
			for(int j=0; j<zSize;j++){
				if(personalStatus[i,j]!=myStatus[i,j]){
					return false;
				}
			}
		}
		return true;
	}

}
