using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    public OVRInput.Controller thisController;





    void Update () {

        Debug.Log(OVRInput.IsControllerConnected(OVRInput.Controller.RTouch));
        
        transform.localPosition = OVRInput.GetLocalControllerPosition(thisController);
        transform.localRotation = OVRInput.GetLocalControllerRotation(thisController);
    }
}
