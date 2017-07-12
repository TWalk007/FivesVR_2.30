using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTeleportTouch : MonoBehaviour {

	public TeleportTransition teleportTransition;
	public GameObject teleportTransitionCanvas;
	public GameObject teleportFadePanel;
	public GameObject touchCameraController;

    public Transform gazeDirection;

    private Vector3 heightAdjustedPos;

    void Start () {
		heightAdjustedPos = new Vector3 (transform.position.x, transform.position.y + 2.25f, transform.position.z);        
	}
    
	public void Teleport () {
        touchCameraController.transform.position = heightAdjustedPos;
		TeleportTransition ();
		teleportFadePanel.GetComponent<TeleportTransition> ().TeleportingFalse ();
        Camera.main.transform.LookAt(gazeDirection);
    }

    private void TeleportTransition()
    {
        Instantiate(teleportTransitionCanvas);
        teleportFadePanel.GetComponent<TeleportTransition>().TeleportingTrue();
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f);
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDown()
    {
        Teleport();
    }
    
}
