using UnityEngine;
using System.Collections;

public class KeyMenu : MonoBehaviour {
	public Texture libraryTexture;
	public Texture wareHouseTexture;
	private bool menuOpenSwitch;

	// Use this for initialization
	void Start ()
	{
		menuOpenSwitch = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDown()
	{
		menuOpenSwitch = !menuOpenSwitch;
	}

	void OnGUI()
	{
		if(menuOpenSwitch){
			Vector2 keyIconPosition = Camera.main.WorldToScreenPoint(this.transform.position);
			float iconWidth   = wareHouseTexture.width;
			float iconHeight  = wareHouseTexture.height;
			float iconCenterX = keyIconPosition.x - (iconWidth/2);
			float iconCenterY = (Screen.height - keyIconPosition.y) - (iconHeight/2);
			int iconGap = 50;
			if(GUI.Button(new Rect(iconCenterX, iconCenterY - iconWidth - iconGap, iconWidth, iconHeight), libraryTexture)){
				Application.LoadLevel("LibraryScene");
			}//MoveLibrarySceneButton
			if(GUI.Button(new Rect(iconCenterX - iconHeight - iconGap, iconCenterY, iconWidth, iconHeight), wareHouseTexture)){

			}//MoceWareHouseSceneButton
		}
	}
}
