using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject creditsPanel;
	public GameObject howToPanel;
//	public Canvas HUD1;
//	public Canvas HUD2;
//	public Canvas HUD3;
//	public Canvas HUD4;
	public GameObject rewiredEventManager;

	public GameObject mainPanelFirst;
	public GameObject pausePanelFirst;							//Store a reference to the Game Object PausePanel 
	public GameObject optionsPanelFirst;							//Store a reference to the Game Object PausePanel 
	public GameObject creditsPanelFirst;
	public GameObject howToPanelFirst;

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		rewiredEventManager.GetComponent<EventSystem> ().SetSelectedGameObject (optionsPanelFirst);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
		rewiredEventManager.GetComponent<EventSystem> ().SetSelectedGameObject (mainPanelFirst);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}

	//credits show
	public void ShowCredits(){
		creditsPanel.SetActive (true);
		rewiredEventManager.GetComponent<EventSystem> ().SetSelectedGameObject (creditsPanelFirst);
	}

	//credits hide
	public void HideCredits(){
		creditsPanel.SetActive (false);
	}

	//how to show
	public void ShowHowTo(){
		howToPanel.SetActive (true);
		rewiredEventManager.GetComponent<EventSystem> ().SetSelectedGameObject (howToPanelFirst);
	}

	//how to hide
	public void HideHowTo(){
		howToPanel.SetActive (false);
	}

	//hUD
//	public void ShowHUD(){
//		HUD1.gameObject.SetActive (true);
//		HUD2.gameObject.SetActive (true);
//		HUD3.gameObject.SetActive (true);
//		HUD4.gameObject.SetActive (true);
//	}

	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		rewiredEventManager.GetComponent<EventSystem> ().SetSelectedGameObject (pausePanelFirst);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);

	}


}
