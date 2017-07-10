using UnityEngine;
using UnityEngine.VR;

public class NegateTracking : MonoBehaviour {

    public Transform hotSpotSpindleTransform;
    public Transform hotSpotoperatorTransform;

    public bool spindleScene = false;
    public bool operatorScene = false;

    private Vector3 currentPos;
    
	void Update () {

        if (spindleScene)
        {
            InvertCamPos(hotSpotSpindleTransform);
        }

        if (operatorScene)
        {
            InvertCamPos(hotSpotoperatorTransform);
        }


        transform.rotation = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.CenterEye));
    }

    public void InvertCamPos(Transform trans)
    {
        currentPos = -InputTracking.GetLocalPosition(VRNode.CenterEye);
        transform.position = new Vector3 (trans.position.x, trans.position.y + 2.25f, trans.position.z);
    }


}
