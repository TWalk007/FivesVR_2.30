using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovieCanvasController : MonoBehaviour {

	public float movieCanvasFadeDuration = 2.0f;

	public GameObject panelTop;
	public GameObject panelBottom;

//	private Canvas canvas;

	void Start (){
//		canvas = this.GetComponent<Canvas> ();
	}

	public void InitializeCanvas(){
		this.GetComponent<Canvas> ().enabled = true;

		Color targetColor = new Color(0f,0f,0f,0f);
		this.transform.Find ("Panel Top").GetComponent<Image> ().color = targetColor;
		this.transform.Find ("Panel Bottom").GetComponent<Image> ().color = targetColor;
	}

	public IEnumerator DeactivateCanvas (){
		yield return new WaitForSeconds (movieCanvasFadeDuration + 1);
		this.GetComponent<Canvas> ().enabled = false;
	}

	public IEnumerator CanvasFadeIn(){
		float topPanelAlpha = 0f;
		float bottomPanelAlpha = 0f;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / movieCanvasFadeDuration) {
			panelTop.GetComponent<Image>().color = new Color (0f, 0f, 0f, Mathf.Lerp (topPanelAlpha, 1f, t));
			panelBottom.GetComponent<Image>().color = new Color (0f, 0f, 0f, Mathf.Lerp (bottomPanelAlpha, 1f, t));
			yield return null;
		}
	}

	public IEnumerator CanvasFadeOut (){
		float topPanelAlpha = 1f;
		float bottomPanelAlpha = 1f;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / movieCanvasFadeDuration) {
			panelTop.GetComponent<Image>().color = new Color (0f, 0f, 0f, Mathf.Lerp (topPanelAlpha, 0f, t));
			panelBottom.GetComponent<Image>().color = new Color (0f, 0f, 0f, Mathf.Lerp (bottomPanelAlpha, 0f, t));
			yield return null;
		}
	}

}
