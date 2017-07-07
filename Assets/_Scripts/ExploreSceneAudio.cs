using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreSceneAudio : MonoBehaviour {

	public float startDelayTime = 1f;
	public float checkDelayTime = 1f;

	public AudioClip [] audioClip = new AudioClip[6];
	public AudioClip selectedClip;
	public bool audioGreenLight = true;

	public Transform camPos;
	public GameObject cuttingTable;

	private AudioSource audioSource;

	void Start(){
		audioSource = (AudioSource)this.GetComponent<AudioSource> ();
		cuttingTable.GetComponent<Collider> ().enabled = false;
	}

	void Update () {
		CanVOPlay ();

		if (!audioSource.isPlaying) {
			if (audioGreenLight) {

				if (selectedClip == audioClip [0]) {
					float delay = selectedClip.length + startDelayTime;
					FrontLoadingVO ();
					StartCoroutine (AudioDelay (delay));
					float check = selectedClip.length + checkDelayTime;
					selectedClip = null;
					StartCoroutine (HasAssemblyVOPlayed (check));
				}

				if (selectedClip == audioClip [1]) {
					float delay = selectedClip.length + startDelayTime;
					TableVO ();
					StartCoroutine (AudioDelay (delay));
					float check = selectedClip.length + checkDelayTime;
					selectedClip = null;
					StartCoroutine (HasAssemblyVOPlayed (check));
				}

				if (selectedClip == audioClip [2]) {
					float delay = selectedClip.length + startDelayTime;
					SpindleVO ();
					StartCoroutine (AudioDelay (delay));
					float check = selectedClip.length + checkDelayTime;
					selectedClip = null;
					StartCoroutine (HasAssemblyVOPlayed (check));
				}

				if (selectedClip == audioClip [3]) {
					float delay = selectedClip.length + startDelayTime;
					SpindleInternal ();
					StartCoroutine (AudioDelay (delay));
					float check = selectedClip.length + checkDelayTime;
					selectedClip = null;
					StartCoroutine (HasAssemblyVOPlayed (check));
				}

				if (selectedClip == audioClip [4]) {
					float delay = selectedClip.length + startDelayTime;
					OperatorVO ();
					StartCoroutine (AudioDelay (delay));
					float check = selectedClip.length + checkDelayTime;
					selectedClip = null;
					StartCoroutine (HasAssemblyVOPlayed (check));
				}

				if (selectedClip == audioClip [5]) {
					float delay = selectedClip.length + startDelayTime;
					USFlagVO ();
					StartCoroutine (AudioDelay (delay));
					float check = selectedClip.length + checkDelayTime;
					selectedClip = null;
					StartCoroutine (HasAssemblyVOPlayed (check));
				}

			}
		}
	}

	private void CanVOPlay(){
		for (int i = 0; i < audioClip.Length; i++) {
			if (audioSource.isPlaying) {
				audioGreenLight = false;

			} else {
				audioGreenLight = true;
			}
		}
	}

	private IEnumerator AudioDelay (float seconds){
		yield return new WaitForSeconds (seconds);
		audioGreenLight = true;
		yield break;
	}

	private IEnumerator HasAssemblyVOPlayed(float seconds){
		yield return new WaitForSeconds (seconds);
		cuttingTable.GetComponent<Collider> ().enabled = true;
	}

	public void FrontLoadingVO(){
		audioSource.clip = audioClip [0];
		audioSource.Play();
	}

	public void TableVO(){
		audioSource.clip = audioClip [1];
		audioSource.Play();
	}

	public void SpindleVO(){
		audioSource.clip = audioClip [2];
		audioSource.Play ();
	}

	public void SpindleInternal (){
		audioSource.clip = audioClip [3];
		audioSource.Play ();
	}

	public void OperatorVO (){
		audioSource.clip = audioClip [4];
		audioSource.Play ();
	}

	public void USFlagVO (){
		audioSource.clip = audioClip [5];
		audioSource.Play ();
	}
}
