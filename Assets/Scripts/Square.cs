using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	private int posX;
	private int posY;

	private float altitude = 0.1f;

	// Use this for initialization
	void Start (int X, int Y) {
		posX = X;
		posY = Y;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnPostRender(){
		GL.Begin(GL.LINES);
		GL.Color(Color.red);
		GL.Vertex3(0, altitude, 0);
		GL.Vertex3(10, altitude, 0);
		GL.Vertex3(10, altitude, 0);
		GL.Vertex3(10, altitude, 10);
		GL.Vertex3(10, altitude, 10);
		GL.Vertex3(0, altitude, 10);
		GL.Vertex3(0, altitude, 10);
		GL.Vertex3(0, altitude, 0);
		GL.End();
	}
}
