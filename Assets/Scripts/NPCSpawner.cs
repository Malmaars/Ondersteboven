using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCSpawner : MonoBehaviour
{
    //Mind position and rotation
    public GameObject npcPrefab;

    public Transform[] PossibleNPCLocations;
    public Transform[] CamLocations;
    bool[] isSomeoneSittingThere;

    [Serializable]
    public class PossibleLines
    {
        public string[] line;
        public int destination;
    }
    public PossibleLines[] DifferentLines;

    public AudioClip[] voicePossibilities;

    // Start is called before the first frame update
    void Start()
    {
        //32 zitplaatsen. 1 mogelijke zitplek per stoel. en 1 zitplek per cabine
        //16 zijn gevuld

        isSomeoneSittingThere = new bool[PossibleNPCLocations.Length];

        foreach (Transform loc in PossibleNPCLocations)
        {
            loc.gameObject.SetActive(false);
        }
        //give random destination + random text
        //random accessoires
        for (int i = 0; i < 11; i++)
        {
            SpawnNPC();
        }
    }

    public void SpawnNPC()
    {
        GameObject npc = Instantiate(npcPrefab);

        int randomInt = UnityEngine.Random.Range(0, PossibleNPCLocations.Length);

        while (isSomeoneSittingThere[randomInt])
        {
            randomInt = UnityEngine.Random.Range(0, PossibleNPCLocations.Length);
        }

        //give random location
        npc.transform.position = PossibleNPCLocations[randomInt].position;
        npc.transform.rotation = PossibleNPCLocations[randomInt].rotation;
        npc.GetComponent<NPC>().cameraPos = CamLocations[randomInt];
        isSomeoneSittingThere[randomInt] = true;

        randomInt = UnityEngine.Random.Range(0, DifferentLines.Length);

        ////random text & fitting destination
        npc.GetComponent<NPC>().myLine = DifferentLines[randomInt].line;
        npc.GetComponent<NPC>().destination = DifferentLines[randomInt].destination;

        //set a random accessory
        randomInt = UnityEngine.Random.Range(3, 16);
        npc.transform.GetChild(randomInt).gameObject.SetActive(true);

        randomInt = UnityEngine.Random.Range(0, voicePossibilities.Length);
        npc.GetComponent<NPC>().myVoice = voicePossibilities[randomInt];
    }
}
