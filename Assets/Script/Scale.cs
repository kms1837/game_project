using UnityEngine;
using System.Collections;

public class Scale : MonoBehaviour {
	private bool menuOpenSwitch;
	private BackGround background;
	public Texture2D[] uiTexture;
	
	void Start ()
	{
		menuOpenSwitch = false;

		GameObject backgroundObject = GameObject.Find("Background");
		background = (BackGround)backgroundObject.GetComponent("BackGround");
	}

	void OnMouseDown()
	{	
		if(!menuOpenSwitch){
			background.gameStop();
			menuOpenSwitch = true;
		}else{
			background.gamePlay();
			menuOpenSwitch = false;
		}
	}

	void OnMouseOver()
	{
		gameObject.renderer.material.color = Color.blue;
	}
	
	void OnMouseExit ()
	{
		gameObject.renderer.material.color = Color.white;
	}

	Rect getScreenCenterRect(float width, float height) {
		return new Rect((Screen.width/2) - (width/2), (Screen.height/2) - (height/2), width, height);
		// Rect : L, R, W, H
	}
	
	void OnGUI()
	{
		if(menuOpenSwitch){
			GUI.depth = -1;
			
			Rect scaleMenuUIRect = getScreenCenterRect(uiTexture[0].width, uiTexture[0].height);
			
			GUI.DrawTexture(scaleMenuUIRect, uiTexture[0]);
			
			if(GUI.Button(new Rect(scaleMenuUIRect.xMax - 80, scaleMenuUIRect.y + 30, uiTexture[1].width, uiTexture[1].height), uiTexture[1], GUIStyle.none)) {
				menuOpenSwitch = false;
				background.gamePlay();
			}
		}
	}

}
