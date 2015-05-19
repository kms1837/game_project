using UnityEngine;
using System.Collections;

public class LibraryScript : MonoBehaviour
{
	public int menuId;
	public Texture2D[] uiTexture;

	private bool bedDisplaySwitch;
	private bool bookcaseDisplaySwitch;

	private int selectedBook;
	private int sleepTime;
	
	private int skillBookSize;
	private int guideBookSize;

	private int optimalHeight = 910;
	private int optimalWidth  = 1618;

	/*
		menuId
		1: bed
		2: Bookcase
	 */

	void Start () {
		bedDisplaySwitch 	  = false;
		bookcaseDisplaySwitch = false;

		selectedBook = 0;
		sleepTime	 = 7;
	}

	void OnMouseDown(){
		if(!bedDisplaySwitch && !bookcaseDisplaySwitch) {
			switch(menuId) {
				case 1:
					if(!bedDisplaySwitch) bedDisplaySwitch = true;
					break;
				case 2:
					if(!bookcaseDisplaySwitch) bookcaseDisplaySwitch = true;
					break;
			}
		}
	}

	void OnGUI()
	{
		if(bookcaseDisplaySwitch) {
			Rect bookUIRect = getScreenCenterRect(uiTexture[0].width, uiTexture[0].height);

			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

			GUI.DrawTexture(bookUIRect, uiTexture[0]);

			if(GUI.Button(new Rect(bookUIRect.xMax - 80, bookUIRect.y + 30, uiTexture[1].width, uiTexture[1].height), uiTexture[1])) {
				bookcaseDisplaySwitch = false;
			}

			Texture2D tapIconTexture;
			float tapIconTop 	= bookUIRect.y + 130;
			float tapIconTopGap = 120;
			for(int i=0; i<2; i++) {
				//selectedBook
				if(i == 0) 	tapIconTexture = uiTexture[3];
				else 		tapIconTexture = uiTexture[2];

				if(GUI.Button(new Rect(bookUIRect.x + 20, tapIconTop + (tapIconTopGap * i), tapIconTexture.width, tapIconTexture.height), tapIconTexture)){
					selectedBook = i;
				}
			}

			Texture2D bookIconTexture;
			float bookIconTop		= bookUIRect.y + 150;
			float bookIconLeft		= bookUIRect.x + 130;
			float bookIconLeftGap 	= 180;
			float bookIconTopGap  	= 220;
			Vector2 mousePosition 	= Input.mousePosition;
			Vector2 convertPosition = new Vector2(mousePosition.x, Screen.height - mousePosition.y);
			//스크린 좌표계의 최상단은 스크린 높이를 600이라 하면 ex)(0, 600) 이지만 drawTexture는 최상단이 (0, 0)기준으로 그린다.

			switch(selectedBook){
				case 0:{ //skillTap
					Rect bookIconRect;
					for(int i=0; i<10; i++) {
						if(i < 2) bookIconTexture = uiTexture[4];
						else 	  bookIconTexture = uiTexture[5];

						bookIconRect = new Rect(bookIconLeft + (bookIconLeftGap * (i % 5)),
					                            bookIconTop  + (bookIconTopGap * (i / 5)),
					                            bookIconTexture.width,
					                            bookIconTexture.height);

						GUI.DrawTexture(bookIconRect, bookIconTexture);

						if(i < 2 && bookIconRect.Contains(convertPosition)) {
							GUIStyle fontStyle  = GUI.skin.GetStyle("Label");
							fontStyle.alignment = TextAnchor.MiddleCenter;
							fontStyle.fontSize	= 15;

							GUI.Label(new Rect(bookIconRect.x, bookIconRect.y + (bookIconRect.height/2), bookIconRect.width, 30), "1 / 1010", fontStyle);
							//BookInfoLabel
							GUI.Label(new Rect(bookIconRect.x, bookIconRect.yMax, bookIconRect.width, 30), "상인의기본", fontStyle);
							//BookNameLabel

							//isuue - 정보가 동일 책에따라 정보를 바꿔줘야함
						}
					}
					break;
				}
				case 1:{ //guideTab
					Rect bookIconRect;
					bookIconTexture = uiTexture[4];
					for(int i=0; i<5; i++) {
						bookIconRect = new Rect(bookIconLeft + (bookIconLeftGap * (i % 5)), bookIconTop + (bookIconTopGap * ( i / 5)), bookIconTexture.width, bookIconTexture.height);
						GUI.DrawTexture(bookIconRect, bookIconTexture);

						if(bookIconRect.Contains(convertPosition)) {
							GUIStyle fontStyle  = GUI.skin.GetStyle("Label");
							fontStyle.alignment = TextAnchor.MiddleCenter;
							fontStyle.fontSize	= 15;
							
							GUI.Label(new Rect(bookIconRect.x, bookIconRect.y + (bookIconRect.height/2), bookIconRect.width, 30), "1 / 2014", fontStyle);
							//BookInfoLabel
							GUI.Label(new Rect(bookIconRect.x, bookIconRect.yMax, bookIconRect.width, 30), "몬스터 도감", fontStyle);
							//BookNameLabel
						}
					}
					break;
				}
			}
		}

		if(bedDisplaySwitch){
			Rect bedUIRect = getScreenCenterRect(uiTexture[0].width, uiTexture[0].height);
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
			GUI.DrawTexture(bedUIRect, uiTexture[0]);
			GUIStyle fontStyle = GUI.skin.GetStyle("Label");
			fontStyle.alignment = TextAnchor.MiddleRight;
			fontStyle.fontSize	= 35;

			if(GUI.Button(new Rect(bedUIRect.xMax - 60, bedUIRect.y + 30, uiTexture[1].width, uiTexture[1].height), uiTexture[1])){
				bedDisplaySwitch = false;
			}//bedMenuCloseButton

			if(GUI.Button(new Rect(bedUIRect.xMax - 130, bedUIRect.y + 80, uiTexture[2].width, uiTexture[2].height), uiTexture[2])){
				if(sleepTime < 24) 	sleepTime++;
			}//SleepTimeUpButton

			if(GUI.Button(new Rect(bedUIRect.xMax - 130, bedUIRect.y + 110, uiTexture[3].width, uiTexture[3].height), uiTexture[3])){
				if(sleepTime > 7)	sleepTime--;
			}//SleepTimeDownButton

			GUI.Label(new Rect(bedUIRect.x + 95, bedUIRect.y + 85, 240, 40), sleepTime + " hour", fontStyle); //SleepTimeLabel
		}
	}

	Rect getScreenCenterRect(float width, float height) {
		return new Rect((Screen.width/2) - (width/2), (Screen.height/2) - (height/2), width, height);
		// Rect : L, R, W, H
	}

	Rect ResizeGUI(Rect _rect)
	{
		Debug.Log ("(origin) width : " + _rect.width + ", height : " + _rect.height);

		float FilScreenWidth 	= _rect.width / optimalWidth;
		float FilScreenHeight 	= _rect.height / optimalHeight;
		float rectWidth			= FilScreenWidth * Screen.width;
		float rectHeight 		= FilScreenHeight * Screen.height;
		float rectX 			= (_rect.x / optimalWidth)  * Screen.width;
		float rectY 			= (_rect.y / optimalHeight) * Screen.height;
		Rect  retrunData 		= new Rect(rectX, rectY, rectWidth, rectHeight);

		Debug.Log ("(resize) width : " + rectWidth + ", height : " + rectHeight);

		return retrunData;
	}

	void OnMouseOver()
	{
		if(!bedDisplaySwitch && !bookcaseDisplaySwitch) {
			gameObject.renderer.material.color = Color.blue;
		}
	}

	void OnMouseExit ()
	{
		gameObject.renderer.material.color = Color.white;
	}
	
}
