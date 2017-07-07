using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptionsCanvas : MonoBehaviour {



	public GameObject [] textPanels;
	public GameObject ccBottom;
	public Image ccBottomImage;

	private float canvasFadeDuration = 1.5f;
	private float textFadeDuration = 2f;
	private float textFadeOutDuration = 1.0f;
	private float textDelay = 0.25f;
	private float activeAlpha;

	private bool panelOn = false;
	private int arrayInt;

	void Start (){
		activeAlpha = ccBottomImage.color.a;
		arrayInt = 0;
	}

	public void FadeCaptionsPanelToggle(int arrInt){
		arrayInt = arrInt;
		if (!panelOn) {
			StartCoroutine (CanvasFadeIn());
			StartCoroutine (TextFadeIn ());
			panelOn = true;
		} else {
			StartCoroutine (TextFadeOut ());
			StartCoroutine (CanvasFadeOut());
			panelOn = false;
		}
	}

	private IEnumerator TextFadeIn (){
		textPanels [arrayInt].SetActive(true);
		Text text = textPanels [arrayInt].GetComponent<Text> ();
		float resetAlpha = 0f;
		text.color = new Color (1f, 1f, 1f, resetAlpha);
		yield return new WaitForSeconds (textDelay);
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / textFadeDuration / 10f) {
			text.color = new Color (1f, 1f, 1f, Mathf.Lerp (text.color.a, 1.0f, t));
			yield return null;
		}
	}

	private IEnumerator TextFadeOut (){
		Text text = textPanels [arrayInt].GetComponent<Text> ();
		text.CrossFadeAlpha (0f, textFadeOutDuration, true);
		yield return new WaitForSeconds (textFadeOutDuration + 0.5f);
		textPanels [arrayInt].SetActive(false);
		yield break;
	}

	private IEnumerator CanvasFadeIn(){
		ccBottomImage.enabled = true;
		float resetAlpha = 0f;
		ccBottomImage.color = new Color (0f, 0f, 0f, resetAlpha);
		for (float t = 0.0f; t < activeAlpha; t += Time.deltaTime / canvasFadeDuration / 10f) {
			ccBottomImage.color = new Color (0f, 0f, 0f, Mathf.Lerp (ccBottomImage.color.a, activeAlpha, t));
			yield return null;
		}
	}

	private IEnumerator CanvasFadeOut (){
		ccBottomImage.CrossFadeAlpha (0f, canvasFadeDuration, true);
		yield return new WaitForSeconds (canvasFadeDuration + 0.5f);
		ccBottomImage.enabled = false;
		yield break;
	}


}
