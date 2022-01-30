using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    float timer = 0;

    int stationNumber;
    int totalLeaving;
    int goodJobs;
    int badJobs;

    int direction = 1;

    List<NPC> npcsGettingOut;

    //change the texture depending on the station
    public GameObject visualGameobject;
    public GameObject MainCamera;
    Player player;

    public Transform[] stationResults;

    public Material[] stationVisuals;
    public MeshRenderer stationMesh;

    public Transform pointer;

    int secondLimit = 180;
    float timerRot = -72;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        npcsGettingOut = new List<NPC>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * direction;

        //72/secondlimit/5
        if (direction == 1)
        {
            timerRot += Time.deltaTime / 2.5f;
            pointer.transform.localRotation = Quaternion.Euler(-90, 0, timerRot);
        }

        if(direction == -1)
        {
            //kill peeps
            foreach (NPC npc in npcsGettingOut)
            {
                if (npc.destination != stationNumber - 1)
                {
                    //slowly fade and die
                    npc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(npc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color.r, npc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color.g, npc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color.b, npc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color.a - Time.deltaTime / 3);
                }
            }
        }
        
        if(timer >= secondLimit)
        { 
            //check each NPC if they are on location
            NPC[] allNPCs = FindObjectsOfType<NPC>();

            foreach(NPC npc in allNPCs)
            {
                //npc has a ticket
                if(npc.myTicket != null && npc.myTicket.destination == stationNumber)
                {
                    npcsGettingOut.Add(npc);
                    npc.gameObject.transform.position = stationResults[totalLeaving].position;
                    npc.gameObject.transform.rotation = stationResults[totalLeaving].rotation;
                    //correct destination

                    if (npc.destination == stationNumber)
                    {
                        //replace later
                        goodJobs++;
                    }

                    //incorrect
                    else
                    {
                        Debug.Log("WRONG STATION");
                        //replace later
                        npc.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
                        badJobs++;
                    }

                    totalLeaving++;
                }

                else
                {
                    if(npc.destination == stationNumber)
                    {
                        npc.deathCounter += 1;
                        if(npc.deathCounter >= 3)
                        {
                            //die
                            Destroy(npc.gameObject);
                        }
                    }
                }
            }

            //apply visuals
            MainCamera.SetActive(false);
            visualGameobject.SetActive(true);

            stationMesh.material = stationVisuals[stationNumber];

            stationNumber++;
            timer = 0;
            direction = -1;
            //next station
            //stationNumber += 1;
            if (stationNumber == 5)
            {
                stationNumber = 0;
            }
        }

        if(timer < -6)
        {
            timer = 0;
            direction = 1;
            MainCamera.SetActive(true);
            visualGameobject.SetActive(false);
            foreach(NPC npc in npcsGettingOut)
            {
                npcsGettingOut.Remove(npc);
                Destroy(npc.gameObject);
                FindObjectOfType<NPCSpawner>().SpawnNPC();
            }
        }


    }
}
