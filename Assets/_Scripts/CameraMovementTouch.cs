using System.Collections;
using UnityEngine;

public class CameraMovementTouch : MonoBehaviour {

    public float turnSpeed = 4.0f;
    public float reboundSpeed = 6.0f;

    private Vector3 mouseOrigin;
    private bool isRotating;

	void Update () {

        //Get the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        //Disable movement on button release
        if (!Input.GetMouseButton(0))
        {
            isRotating = false;
        }

        //Rotate the camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

	}
}
