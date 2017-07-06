using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public float fadeInTime;

	private Image fadePanel;
	private Color currentColor = new Color (1f, 1f, 1f, 0f);

	void Start () {
		fadePanel = GetComponent<Image> ();
	}

	void Update (){
		if (Time.timeSinceLevelLoad < fadeInTime) {
			float alphaChange = Time.deltaTime / fadeInTime;
			currentColor.a += alphaChange;
			fadePanel.color = currentColor;
		}
	}
}