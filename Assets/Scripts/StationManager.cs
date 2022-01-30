using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    float timer = 0;

    int stationNumber;

    List<NPC> npcsGettingOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= 120)
        {
            //check each NPC if they are on location
            NPC[] allNPCs = FindObjectsOfType<NPC>();

            foreach(NPC npc in allNPCs)
            {
                //npc has a ticket
                if(npc.myTicket != null)
                {
                    npcsGettingOut.Add(npc);

                    //correct destination
                    if(npc.destination == stationNumber)
                    {
                        //replace later
                        Destroy(npc.gameObject);
                    }

                    //incorrect
                    else
                    {
                        //replace later
                        Destroy(npc.gameObject);
                    }
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

            timer = 0;
            //next station
            stationNumber += 1;
            if(stationNumber == 5)
            {
                stationNumber = 0;
            }
        }


    }
}
