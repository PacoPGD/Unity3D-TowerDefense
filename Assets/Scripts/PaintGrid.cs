using UnityEngine;
using System.Collections;

public class PaintGrid : MonoBehaviour {

	private float altitude = 0.1f;

	private int xBoardSize = 20; //Number of boxes in X
	private int yBoardSize = 20; //Number of boxes in Y
	private int boxSize = 10; //Size of one box

	int i,j;

	void Start(){


	}

	void Update(){
	
	}

	void OnPostRender(){
		for (i=0; i<xBoardSize; i++){
			for (j=0; j<yBoardSize; j++){
				int xPos = i*boxSize;
				int zPos = j*boxSize;

				GL.Begin(GL.LINES);
				GL.Color(Color.red);
				GL.Vertex3(xPos, altitude, zPos);
				GL.Vertex3(xPos+boxSize, altitude, zPos);
				GL.Vertex3(xPos+boxSize, altitude, zPos);
				GL.Vertex3(xPos+boxSize, altitude, zPos+boxSize);
				GL.Vertex3(xPos+boxSize, altitude, zPos+boxSize);
				GL.Vertex3(xPos, altitude, zPos+boxSize);
				GL.Vertex3(xPos, altitude, zPos+boxSize);
				GL.Vertex3(xPos, altitude, zPos);
				GL.End();
			}
		}
	}

}