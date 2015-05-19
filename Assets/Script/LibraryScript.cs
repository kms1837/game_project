using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class LibraryScript : MonoBehaviour
{
	public int menuId;
	public Texture2D[] uiTexture;
	public AudioClip[] soundEffect;
	public AudioClip testSound;

	//private AudioSource audio2;

	private bool bedDisplaySwitch;
	private bool bookcaseDisplaySwitch;

	private ScreenChangeEffect fadeEffect;
	private int  selectedBook;
	private int  sleepTime;
	private bool sleepSwitch;
	
	private int skillBookSize;
	private int guideBookSize;

	private int optimalHeight = 910;
	private int optimalWidth  = 1618;

	private ArrayList bookList;
	private ArrayList EncyclopediaList;

	BackGround background;

	/*
		menuId
		1: bed
		2: Bookcase
	 */

	void Start () {
		GameObject backgroundObject = GameObject.Find("Background");
		background					= (BackGround)backgroundObject.GetComponent("BackGround");
		bedDisplaySwitch 	  		= false;
		bookcaseDisplaySwitch 		= false;
		sleepSwitch					= false;
		initialization();
	}

	void initialization()
	{
		selectedBook = 0;
		sleepTime	 = 7;
	}

	void OnMouseDown(){
		if(!bedDisplaySwitch && !bookcaseDisplaySwitch) {
			initialization();
			switch(menuId) {
				case 1:{
					if(!bedDisplaySwitch){
						bedDisplaySwitch = true;
						background.gameStop();
					}
					break;
				}
				case 2:{
					if(!bookcaseDisplaySwitch){
						bookcaseDisplaySwitch = true;
						background.gameStop();
					}
					break;
				}
			}
		}
	}

	void OnGUI()
	{
		GUI.depth = -1;

		if(bookcaseDisplaySwitch) {
			Rect bookUIRect = getScreenCenterRect(uiTexture[0].width, uiTexture[0].height);

			GUI.DrawTexture(bookUIRect, uiTexture[0]);

			if(GUI.Button(new Rect(bookUIRect.xMax - 80, bookUIRect.y + 30, uiTexture[1].width, uiTexture[1].height), uiTexture[1], GUIStyle.none)) {
				bookcaseDisplaySwitch = false;
				background.gamePlay();
			}

			Texture2D tapIconTexture;
			float tapIconTop 	= bookUIRect.y + 130;
			float tapIconTopGap = 120;
			for(int i=0; i<2; i++) {
				//selectedBook
				if(i == 0) 	tapIconTexture = uiTexture[3];
				else 		tapIconTexture = uiTexture[2];

				if(GUI.Button(new Rect(bookUIRect.x + 20, tapIconTop + (tapIconTopGap * i), tapIconTexture.width, tapIconTexture.height), tapIconTexture, GUIStyle.none)){
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
			Rect bookIconRect;

			switch(selectedBook){
				case 0:{ //skillTap
					CSVParser bookParser = new CSVParser("Assets/Resource/Library/Book.csv");
					bookList 			 = bookParser.ParsingList;
					GUIStyle fontStyle   = GUI.skin.GetStyle("Label");
					fontStyle.alignment  = TextAnchor.MiddleCenter;
					fontStyle.fontSize	 = 15;

					for(int i=0; i<bookList.Count; i++) {
						string[] bookInfo = (string[])bookList[i];
						bool bookState;

						if(bookInfo[4] == "true"){
							bookState = true;
							bookIconTexture = uiTexture[4];
						}else{
						 	bookState = false;
							bookIconTexture = uiTexture[5];
						}

						bookIconRect = new Rect(bookIconLeft + (bookIconLeftGap * (i % 5)),
					                            bookIconTop  + (bookIconTopGap  * (i / 5)),
					                            bookIconTexture.width,
					                            bookIconTexture.height);

						GUI.DrawTexture(bookIconRect, bookIconTexture);
						
						if(bookState) {
							fontStyle.normal.textColor = Color.white;
							GUI.Label(new Rect(bookIconRect.x, bookIconRect.yMax, bookIconRect.width, 30), bookInfo[0], fontStyle);
							//BookNameLabel

							if(bookIconRect.Contains(convertPosition)) {
								string bookPageInfo;
								bookPageInfo = bookInfo[2] + " / " + bookInfo[3];
								fontStyle.normal.textColor = Color.black;
								GUI.Label(new Rect(bookIconRect.x, bookIconRect.y + (bookIconRect.height/2), bookIconRect.width, 30), bookPageInfo, fontStyle);
								//BookInfoLabel
							}//MouseOver to Display Book Info
						}
					}
					break;
				}
				case 1:{ //guideTab
					CSVParser encyclopediaParser = new CSVParser("Assets/Resource/Library/Encyclopedia.csv");
					EncyclopediaList 			 = encyclopediaParser.ParsingList;
					bookIconTexture 			 = uiTexture[4];

					for(int i=0; i<5; i++) {
						string[] bookInfo = (string[])EncyclopediaList[i];
						bookIconRect = new Rect(bookIconLeft + (bookIconLeftGap * (i % 5)), bookIconTop + (bookIconTopGap * ( i / 5)), bookIconTexture.width, bookIconTexture.height);
						GUI.DrawTexture(bookIconRect, bookIconTexture);

						GUIStyle fontStyle  = GUI.skin.GetStyle("Label");
						fontStyle.alignment = TextAnchor.MiddleCenter;
						fontStyle.fontSize	= 15;

						GUI.Label(new Rect(bookIconRect.x, bookIconRect.yMax, bookIconRect.width, 30), bookInfo[0], fontStyle);
						//BookNameLabel
					}
					break;
				}
			}
		}//bookcase Popup

		if(bedDisplaySwitch){
			Rect bedUIRect = getScreenCenterRect(uiTexture[0].width, uiTexture[0].height);
			GUI.DrawTexture(bedUIRect, uiTexture[0]);
			GUIStyle fontStyle = GUI.skin.GetStyle("Label");
			fontStyle.alignment = TextAnchor.MiddleRight;
			fontStyle.fontSize	= 35;

			if(GUI.Button(new Rect(bedUIRect.xMax - 60, bedUIRect.y + 30, uiTexture[1].width, uiTexture[1].height), uiTexture[1], GUIStyle.none)){
				bedDisplaySwitch = false;
				background.gamePlay();
			}//bedMenuCloseButton

			if(GUI.Button(new Rect(bedUIRect.xMax - 130, bedUIRect.y + 80, uiTexture[2].width, uiTexture[2].height), uiTexture[2], GUIStyle.none)){
				if(sleepTime < 24) 	sleepTime++;
			}//SleepTimeUpButton

			if(GUI.Button(new Rect(bedUIRect.xMax - 130, bedUIRect.y + 110, uiTexture[3].width, uiTexture[3].height), uiTexture[3], GUIStyle.none)){
				if(sleepTime > 1)	sleepTime--;
			}//SleepTimeDownButton

			if(GUI.Button(new Rect((bedUIRect.x + (bedUIRect.width/2)) - (240/2), bedUIRect.yMax - 55, 240, 30), "Confirm")){
				GameObject backgroundObject = GameObject.Find("Background");
				fadeEffect 					= (ScreenChangeEffect)backgroundObject.GetComponent("ScreenChangeEffect");
				bedDisplaySwitch 			= false;
				sleepSwitch 				= true;
				fadeEffect.fadeOutIn();
			}//SleepButton

			GUI.Label(new Rect(bedUIRect.x + 95, bedUIRect.y + 85, 240, 40), sleepTime + " hour", fontStyle); //SleepTimeLabel
		}//Bed Popup

		if(sleepSwitch){
			if(fadeEffect.playState == false){
				sleepSwitch = false;
				background.gamePlay();

				soundEffectTest();
			}//Sleep End
		}

	}

	void soundEffectTest()
	{
		audio.PlayOneShot(testSound);
	}
	
	Rect getScreenCenterRect(float width, float height) {
		return new Rect((Screen.width/2) - (width/2), (Screen.height/2) - (height/2), width, height);
		// Rect : L, R, W, H
	}

	Rect ResizeGUI(Rect _rect)
	{
		//Debug.Log ("(origin) width : " + _rect.width + ", height : " + _rect.height);
		float FilScreenWidth 	= _rect.width  / optimalWidth;
		float FilScreenHeight 	= _rect.height / optimalHeight;
		float rectWidth			= FilScreenWidth  * Screen.width;
		float rectHeight 		= FilScreenHeight * Screen.height;
		float rectX 			= (_rect.x / optimalWidth)  * Screen.width;
		float rectY 			= (_rect.y / optimalHeight) * Screen.height;
		Rect  retrunData 		= new Rect(rectX, rectY, rectWidth, rectHeight);
		//Debug.Log ("(resize) width : " + rectWidth + ", height : " + rectHeight);

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
