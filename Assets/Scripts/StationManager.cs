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
        
        if(timer >= 15)
        { 
            //check each NPC if they are on location
            NPC[] allNPCs = FindObjectsOfType<NPC>();

            foreach(NPC npc in allNPCs)
            {
                //npc has a ticket
                if(npc.myTicket != null && npc.myTicket.destination == stationNumber)
                {
                    npcsGettingOut.Add(npc);

                    //correct destination
                    if(npc.destination == stationNumber)
                    {
                        //replace later
                        npc.gameObject.transform.position = stationResults[totalLeaving].position;
                        npc.gameObject.transform.rotation = stationResults[totalLeaving].rotation;
                        goodJobs++;
                    }

                    //incorrect
                    else
                    {
                        //replace later
                        npc.gameObject.transform.position = stationResults[totalLeaving].position;
                        npc.gameObject.transform.rotation = stationResults[totalLeaving].rotation;
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


            timer = 0;
            direction = -1;
            //next station
            //stationNumber += 1;
            //if(stationNumber == 5)
            //{
            //    stationNumber = 0;
            //}
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
            }
        }


    }
}
