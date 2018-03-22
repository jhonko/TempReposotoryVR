using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class VRCameraHandeller : MonoBehaviour
{

    Transform parentObj;
    Vector3 startPos;
    Vector3 originalPos;

    void Start()
    {
        startPos = transform.localPosition;
        originalPos = transform.localPosition;
        parentObj = transform.root;
    }

    void Update()
    {
        ResetVR();
    }

    void ResetVR()
    {
        if (parentObj != null)
        {
            startPos -= UnityEngine.XR.InputTracking.GetLocalPosition(XRNode.CenterEye);

            Quaternion tempRot = Quaternion.Inverse(parentObj.localRotation);
            Vector3 newAngle = tempRot.eulerAngles;
            transform.localEulerAngles = new Vector3(newAngle.x, originalPos.y, originalPos.z);
        }
    }
}