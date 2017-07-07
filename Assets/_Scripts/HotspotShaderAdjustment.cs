using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotShaderAdjustment : MonoBehaviour {

	public bool colorCrossFade = false;
	public Color colorStart = Color.white;
	public Color colorEnd = Color.magenta;
	public float duration = 1.0f;

	private Renderer rend;


	void Start () {
		rend = this.GetComponent<Renderer> ();
	}
	
	void Update () {
		if (colorCrossFade) {
			CrossFade ();
		}
	}

	private void CrossFade(){
		float lerp = Mathf.PingPong (Time.time, duration) / duration;
		rend.material.color = Color.Lerp (colorStart, colorEnd, lerp);
	}
}
