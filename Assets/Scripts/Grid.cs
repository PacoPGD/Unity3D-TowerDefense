using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour 
{

	public GameObject square; //A plane GameObject, represent a square
	public int xSize=10;
	public int zSize=10;


	private int width = 10; //width of a square
	private int height = 10; //height of a square

	private GameObject [,] grid = new GameObject[xSize,zSize];  //set of squares representing the board

	void Awake()
	{
	
		for (int x = 0; x < xSize; x++) 
		{
			for(int z = 0; z < zSize; z++)
			{
				GameObject gridPlane = (GameObject)Instantiate (square);
				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x +x*width,
					gridPlane.transform.position.y, gridPlane.transform.position.z + z*height);  
				grid[x,z] = gridPlane;
			}
		}

	}

	// Use this for initialization
	void Start () 
	{


	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int x = 0; x < xSize; x++) 
		{
			for(int z = 0; z < zSize; z++)
			{
				if(Input.mousePosition.x>grid[x,z].transform.position.x)
					grid[x,z].GetComponent<Renderer>().material.color = Color.red;
				else
					grid[x,z].GetComponent<Renderer>().material.color = Color.white;
			}
		}
	}
}
