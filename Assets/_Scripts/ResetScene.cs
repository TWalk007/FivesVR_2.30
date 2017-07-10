using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScene : MonoBehaviour {

	public float gazeActivationTime = 1.25f;

	private LevelManager levelManager;

	private float timeElapsed;
	private bool resetScene = false;


	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager> ();

	}

	void Update () {
		if (resetScene){
			timeElapsed += Time.deltaTime;

			if (timeElapsed >= gazeActivationTime) {
				timeElapsed = 0;
				levelManager.ResetScene ();
				resetScene = false;
			}
		} else {
			timeElapsed = 0;
		}
	}

	public void OnPointerEnter (){
		resetScene = true;
	}

	public void OnPointerExit (){
		resetScene = false;
		timeElapsed = 0;
	}

}