using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpindleExtensionTriggers : MonoBehaviour {
	
	public ExploreController exploreController;

	public bool audioHasPlayed = false;

	void StartNextAudio (){
		if (!audioHasPlayed) {
			StartCoroutine (exploreController.VODelay ());
			audioHasPlayed = true;
		}
	}
}
