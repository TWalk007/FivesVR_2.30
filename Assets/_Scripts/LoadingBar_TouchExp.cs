using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar_TouchExp : MonoBehaviour {

	AsyncOperation Load;
	public Image LoadBar;
	public Text LoadPercent;


	void Start()
	{
		//Loading the scene of your choosing
		Load = SceneManager.LoadSceneAsync("03_Touch Explore RT130");

		//Delaying Scene Activation for a fraction of a second to allow "100%" to be displayed at the end.
		Load.allowSceneActivation = false;
	}


	private void Update()
	{
		//Creating variables for our progress bar
		//Load.progress will tell us how far along the scene is
		var progress = Load.progress;
		//Raw output of Load.progress will display a floating number. Ex: (0.34) Multiplying it it by 100 will normalize it.
		float PercentRaw = progress * 100;
		//Converting our floating number to an int so that it will not display decimal numbers.
		int Percent = (int)PercentRaw;
		//Telling our loading graphic to match its fill rate with the actual progress of the scene being loaded in the background.
		LoadBar.fillAmount = progress;
		//Taking our int and converting it to a string so that it can be displayed in text form. 
		LoadPercent.text = Percent.ToString() + "%";


		//Unity scene load finishes at 90%, so to display the full 100% the following code is necessary.
		if (Load.progress == 0.9f)
		{
			//Setting Bar and text to max when scene is fully loaded.
			LoadBar.fillAmount = 1;
			LoadPercent.text = ("100%");
			//Allowing next scene to load 
			Load.allowSceneActivation = true;
		}
	}
}