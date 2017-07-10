using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour {

	public float gazeActivationTime = 1.25f;

	private LevelManager levelManager;

	private float timeElapsed;
	private bool returnToMainMenu = false;


	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager> ();

	}

	void Update () {
		if (returnToMainMenu){
			timeElapsed += Time.deltaTime;

			if (timeElapsed >= gazeActivationTime) {
				timeElapsed = 0;
				levelManager.LoadLevel ("01_Main");
				returnToMainMenu = false;
			}
		} else {
			timeElapsed = 0;
		}
	}

	public void OnPointerEnter (){
		returnToMainMenu = true;
	}

	public void OnPointerExit (){
		returnToMainMenu = false;
		timeElapsed = 0;
	}

}