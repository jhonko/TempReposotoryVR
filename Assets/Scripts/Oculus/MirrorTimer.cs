using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class MirrorTimer :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //this class keeps track of how long the player looks in a mirror, this can be used to analyse how often and long the player looked in the camera

    public float startTime;
    public float endTime;
    public float timeLookedInMirror;
    public float timesLookedinMirror;

    public Text timeLookingInMirror;
    public Text timesLookedAtMirror;






    // Use this for initialization
    void Start()
    {

        

    }

    void Update()
    {

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("in");
        startTime = Time.time;
        timesLookedinMirror++;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        endTime = Time.time;
        timeLookedInMirror += (endTime - startTime);
    }
}

