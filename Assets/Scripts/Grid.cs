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

	private float nextGenerate;
	//Load the squares in the grid and load the status of squares in the gridstatus
	void Awake()
	{
		grid = new GameObject[xSize,zSize];

		//Prepare the static gridStatus
		gridStatus.myStatus = new gridStatus.Status[xSize, zSize];
		gridStatus.xSize = xSize;
		gridStatus.zSize = zSize;
		gridStatus.towerSelection = 1;

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
	void OnPostRender()
	{
		for (int x=0; x<xSize; x++)
		{
			for (int z=0; z<zSize; z++)
			{
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

	void OnGUI(){
		string blue;
		string red;

		if (gridStatus.towerSelection == 1) 
		{
			blue = "Blue Cannon Turret\n ACTIVE";
			red = "Red Laser Turret";
		}
		else 
		{
			blue = "Blue Cannon Turret";
			red = "Red Laser Turret\n ACTIVE";
		}

		GUI.Box (new Rect (0, 0, 200, 50), "press 1 or 2 to select the turret");
		GUI.Box (new Rect (0, 50, 200, 50), blue);
		GUI.Box (new Rect (0, 100, 200, 50), red);
	
		
	}



	void Start () 
	{
		nextGenerate = Time.time;
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
		selectTower ();
		//Enemy generation
		if (Time.time >= nextGenerate)
		{
			nextGenerate = Time.time+enemyTimeGenerate;
			grid[(int)Random.Range (0,xSize-1),zSize-1].GetComponent<Square>().generateEnemy();
		}
	}

	void selectTower(){
		if(Input.GetKeyDown (KeyCode.Alpha1))
		   gridStatus.towerSelection=1;
		if(Input.GetKeyDown (KeyCode.Alpha2))
		   gridStatus.towerSelection=2;
	}
}

