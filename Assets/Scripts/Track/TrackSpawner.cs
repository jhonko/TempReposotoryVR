using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] trackPrefabs;

    private Transform playerTransform;

    //track
    private float trackSpawnZ = -200.0f;
    private float trackLength = 3000.0f;
    private float deleteDelay = 2000f;
    private int amnOfTracksOnScreen = 15;

   

    private int lastPrefabIndex;

    private List<GameObject> activeTrack;

	void Start () {
        //instantiate list active track
        activeTrack = new List<GameObject>();
        //player position
        playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
        // spawns the track(amnoftracksonscreen) for the beginning of game
        for (int i = 0; i<amnOfTracksOnScreen; i++)
        {
            SpawnTrack();
        }



    }
	
	
	void Update () {
        // spawns new tiles as player progresses through the game
        if (playerTransform.position.z - deleteDelay > (trackSpawnZ - amnOfTracksOnScreen * trackLength))
        {
            SpawnTrack();
            DeleteTrack();
        }
		
	}

    private void SpawnTrack()
    {
        //creates prefab clone
        GameObject trackClone;
        trackClone = Instantiate(trackPrefabs[RandomPrefabIndex()]) as GameObject;
        // sets clone child to trackspawner object
        trackClone.transform.SetParent(transform);
        // decides where trackclone spawns and updates value for new spawn
        trackClone.transform.position = Vector3.forward * trackSpawnZ;
        trackSpawnZ += trackLength;
        //puts trackclone in the aciveTrack list
        activeTrack.Add(trackClone);
    }

    private void DeleteTrack()
    {
        Destroy(activeTrack[0]);
        activeTrack.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        //if there is only 1 prefab doesnt do random return first1
        if (trackPrefabs.Length <= 1) {
            return 0;
        }
        //the last randomly chosen track, default is first one in list
        int randomIndex = lastPrefabIndex;
        //while the new random is same as last random, random again
        while ( randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, trackPrefabs.Length);
        }
        //sets the new random as last randommed track
        lastPrefabIndex = randomIndex;
        //returns the random chosen track
        return randomIndex;
    }
}
