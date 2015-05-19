using UnityEngine;
using System.Collections;

public class KeyMenu : MonoBehaviour
{
	public int UIDepth;
	public Texture keyTexture;
	public Texture libraryTexture;
	public Texture wareHouseTexture;

	private bool  menuOpenSwitch;
	private float originDepth;
	
	void Start ()
	{
		menuOpenSwitch 	= false;
		originDepth		= this.transform.position.z;
	}

	void OnMouseDown()
	{
		GameObject backgroundObject = GameObject.Find("Background");
		BackGround background		= (BackGround)backgroundObject.GetComponent("BackGround");

		if(!menuOpenSwitch){
			background.gameStop();
			menuOpenSwitch = true;
			this.transform.Translate(0, 0, UIDepth-1);
		}else{
			background.gamePlay();
			menuOpenSwitch = false;
			this.transform.Translate(0, 0, originDepth);
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

	
	void OnGUI()
	{
		GUI.depth = UIDepth;

		if(menuOpenSwitch){
			Vector2 keyIconPosition = Camera.main.WorldToScreenPoint(this.transform.position);
			float iconWidth   = wareHouseTexture.width;
			float iconHeight  = wareHouseTexture.height;
			float iconCenterX = keyIconPosition.x - (iconWidth/2);
			float iconCenterY = (Screen.height - keyIconPosition.y) - (iconHeight/2);
			int iconGap = 50;

			if(GUI.Button(new Rect(iconCenterX, iconCenterY - iconWidth - iconGap, iconWidth, iconHeight), libraryTexture, GUIStyle.none)){
				Application.LoadLevel("LibraryScene");
			}//MoveLibrarySceneButton
			if(GUI.Button(new Rect(iconCenterX - iconHeight - iconGap, iconCenterY, iconWidth, iconHeight), wareHouseTexture, GUIStyle.none)){

			}//MoceWareHouseSceneButton
		}
	}
}
