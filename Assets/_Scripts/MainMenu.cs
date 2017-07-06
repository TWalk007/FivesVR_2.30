using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public float gazeActivationTime = 1.25f;

	private bool activate = false;
	private float timeElapsed;
	private float gazeProgress;

	private LevelManager levelManager;
	private GameObject buttonRT130;
	private GameObject buttonQuit;
	private bool buttonRT = false;
	private bool buttonQuitting = false;

	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		buttonRT130 = GameObject.Find ("Button RT 130");
		buttonQuit = GameObject.Find ("Button Quit");
	}


	void Update () {
		if (activate && buttonRT) {

			timeElapsed += Time.deltaTime;

			if (timeElapsed >= gazeActivationTime) {
				timeElapsed = 0;
				levelManager.LoadLevel ("02_VR_Pedestal");
				activate = false;
			}
		} else if (activate && buttonQuitting){
			levelManager.Quit ();
		} else {
			timeElapsed = 0;
		}


	}

	public void ButtonCheck (){
		if (buttonRT130) {
			buttonRT = true;
		} else if (buttonQuit) {
			buttonQuitting = true;
		}
	}

	public void OnPointerEnter (){
		activate = true;
		ButtonCheck ();
	}

	public void OnPointerExit (){
		activate = false;
		timeElapsed = 0;
	}


}
