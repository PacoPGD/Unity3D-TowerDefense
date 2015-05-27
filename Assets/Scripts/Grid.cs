using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour 
{		
	public GameObject square; //A plane GameObject, represent a square
	public int xSize;// Number of squares in the x axis
	public int zSize;// Number of squares in the z axis
	public int crystals; //Number of crystals in game
	public float enemyTimeGenerate; //Number of seconds between each enemy generation

	private int width = 10; //width of a square
	private int height = 10; //height of a square

	private GameObject [,] grid;  //set of squares representing the board

	//Load the squares in the grid and load the status of squares in the gridstatus
	void Awake()
	{
		grid = new GameObject[xSize,zSize];

		gridStatus.myStatus = new gridStatus.Status[xSize, zSize];
		gridStatus.xSize = xSize;
		gridStatus.zSize = zSize;

		for (int x = 0; x < xSize; x++) 
		{
			for(int z = 0; z < zSize; z++)
			{
				gridStatus.myStatus[x,z]=gridStatus.Status.Free;

				GameObject gridPlane = (GameObject)Instantiate (square);
				gridPlane.transform.position = new Vector3(gridPlane.transform.position.x +x*width,
					gridPlane.transform.position.y, gridPlane.transform.position.z + z*height);  
				grid[x,z] = gridPlane;
				grid [x,z].GetComponent<Square>().setX(x);
				grid [x,z].GetComponent<Square>().setZ(z);

				//Define the center of the screen and send this center to cameramovement script 
				if(x==xSize/2 && z==zSize/2){
					GetComponent<CameraMovement>().setPivotPoint (grid[x,z].transform);
				}

				//The squares enemies generator
				if(z==zSize-1)
					gridStatus.myStatus[x,z]=gridStatus.Status.Generator;
			}
		}

	}

	//Paint lines to separate squares
	void OnPostRender(){
		for (int x=0; x<xSize; x++){
			for (int z=0; z<zSize; z++){
				float xPos = grid[x,z].transform.position.x-width/2;
				float yPos = grid[x,z].transform.position.y+0.01f;
				float zPos = grid[x,z].transform.position.z-height/2;

				GL.Begin(GL.LINES);
				GL.Color(Color.black);
				GL.Vertex3(xPos, yPos, zPos);
				GL.Vertex3(xPos+width, yPos, zPos);
				GL.Vertex3(xPos+width, yPos, zPos);
				GL.Vertex3(xPos+width, yPos, zPos+height);
				GL.Vertex3(xPos+width, yPos, zPos+height);
				GL.Vertex3(xPos, yPos, zPos+height);
				GL.Vertex3(xPos, yPos, zPos+height);
				GL.Vertex3(xPos, yPos, zPos);
				GL.End();
			}
		}

	}


	void Start () 
	{
		int x = 0;

		//Generate the crystals
		while(x<crystals)
		{
			if(grid [Random.Range (0,xSize-1),Random.Range (1,zSize/3)].GetComponent<Square>().generateCrystal())
				x++;
		}
	}

	void Update () 
	{
		//Enemy generation
		if (Time.time >= enemyTimeGenerate)
		{
			enemyTimeGenerate = Time.time+enemyTimeGenerate;
			grid[(int)Random.Range (0,xSize-1),zSize-1].GetComponent<Square>().generateEnemy();
		}
	}
	
}

