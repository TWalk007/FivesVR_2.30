using UnityEngine;
using UnityEngine.VR;

public class NegateTracking : MonoBehaviour {

    public GameObject cameraObject;
    public Transform hotSpotSpindleTransform;
    public Transform hotSpotoperatorTransform;

    public bool spindleScene = false;
    public bool operatorScene = false;

    private Camera cam;
    private Vector3 currentPos;

    private void Start()
    {
        cam = cameraObject.GetComponent<Camera>();
    }

    void Update () {

        //if (spindleScene)
        //{
        //    InvertCamPos(hotSpotSpindleTransform);
        //}

        //if (operatorScene)
        //{
        //    InvertCamPos(hotSpotoperatorTransform);
        //}


        transform.rotation = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.CenterEye));
    
        Quaternion targetRot = Quaternion.Euler(5f, -77f, 0f);
        cam.transform.rotation = targetRot;
    }

    //public void InvertCamPos(Transform trans)
    //{
    //    currentPos = -InputTracking.GetLocalPosition(VRNode.CenterEye);
    //    transform.position = new Vector3 (trans.position.x, trans.position.y + 2.25f, trans.position.z);
    //}


}
