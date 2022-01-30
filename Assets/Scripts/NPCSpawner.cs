using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCSpawner : MonoBehaviour
{
    //Mind position and rotation
    GameObject npcPrefab;

    public Transform[] PossibleNPCLocations;
    bool[] isSomeoneSittingThere;

    [Serializable]
    public class PossibleLines
    {
        public string[] line;
        public int destination;
    }
    public PossibleLines[] DifferentLines;

    // Start is called before the first frame update
    void Start()
    {
        //51 zitplaatsen. 1 mogelijke zitplek per stoel. en 1 zitplek per cabine
        //26 zijn gevuld

        //give random destination + random text
        //random accessoires
        for (int i = 0; i < 26; i++)
        {
            SpawnNPC();
        }
    }

    void SpawnNPC()
    {
        isSomeoneSittingThere = new bool[PossibleNPCLocations.Length];

        GameObject npc = Instantiate(npcPrefab);

        int randomInt = UnityEngine.Random.Range(0, PossibleNPCLocations.Length);

        while (isSomeoneSittingThere[randomInt])
        {
            randomInt = UnityEngine.Random.Range(0, PossibleNPCLocations.Length);
        }

        //give random location
        npc.transform.position = PossibleNPCLocations[randomInt].position;
        isSomeoneSittingThere[randomInt] = true;

        randomInt = UnityEngine.Random.Range(0, DifferentLines.Length);

        //random text & fitting destination
        npc.GetComponent<NPC>().myLine = DifferentLines[randomInt].line;
        npc.GetComponent<NPC>().destination = DifferentLines[randomInt].destination;

    }
}
