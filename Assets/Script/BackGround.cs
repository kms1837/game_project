using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {
	private bool gameState;
	/*
	 true  - 정상 
	 false - 정지
	 */

	public void gameStop()
	{
		gameState = true;
	}

	public void gamePlay()
	{
		gameState = false;
	}

	void Start () {
		gameState = false;
	}

	void Update () {
	
	}

	void OnGUI(){
		if(gameState){
			GUI.depth = 0;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		}
	}

}
