using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour 
{		
	public GameObject square; //A plane GameObject, represent a square
	public int xSize = 10;
	public int zSize = 10;
	public int crystals = 1;
	public float enemyTimeGenerate = 5;

	private int width = 10; //width of a square
	private int height = 10; //height of a square

	private GameObject [,] grid;  //set of squares representing the board


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

				if(z==zSize-1)
					gridStatus.myStatus[x,z]=gridStatus.Status.Generator;
			}
		}

	}

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



	// Use this for initialization
	void Start () 
	{
		int x = 0;

		while(x<crystals)
		{
			if(grid [Random.Range (0,xSize-1),Random.Range (0,zSize/3)].GetComponent<Square>().generateCrystal())
				x++;
		}


		//grid[19,19].GetComponent<Square>().generateEnemy();
		//grid[10,19].GetComponent<Square>().generateEnemy();
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time >= enemyTimeGenerate)
		{
			enemyTimeGenerate = Time.time+enemyTimeGenerate;
			grid[19,19].GetComponent<Square>().generateEnemy();
		}
	}
	
}

