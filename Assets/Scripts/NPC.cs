using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    int lineNumber;
    public string[] myLine;
    public Vector3 simplifiedPosition;
    bool talking;

    public Transform letterParent;
    public GameObject letterPrefab;

    GameObject[] letterAnimArray;
    int currentAnimLetter;
    int goingDown = 1;

    public Ticket myTicket;
    public Transform ticketPos;

    //getal van 0 t/m 4
    public int destination;

    int currentLetterAnim;

    public int deathCounter;
    // Start is called before the first frame update
    void Start()
    {
        //simplify my position to my grid position
        simplifiedPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

        if (simplifiedPosition.x == -1.5 && simplifiedPosition.z == 8.5)
        {
            simplifiedPosition = new Vector3(-0.5f, 0, 7.5f);
        }

        if (simplifiedPosition.x == -1.5 && simplifiedPosition.z == 9.5)
        {
            simplifiedPosition = new Vector3(-0.5f, 0, 10.5f);
        }

        if (simplifiedPosition.x == -1.5 && simplifiedPosition.z == 14.5)
        {
            simplifiedPosition = new Vector3(-0.5f, 0, 13.5f);
        }

        if (simplifiedPosition.x == -1.5 && simplifiedPosition.z == 15.5)
        {
            simplifiedPosition = new Vector3(-0.5f, 0, 16.5f);
        }

        if (simplifiedPosition.x == -1.5 && simplifiedPosition.z == 20.5)
        {
            simplifiedPosition = new Vector3(-0.5f, 0, 19.5f);
        }

        if (simplifiedPosition.x == -1.5 && simplifiedPosition.z == 21.5)
        {
            simplifiedPosition = new Vector3(-0.5f, 0, 22.5f);
        }

        if (simplifiedPosition.x == -1.5 && simplifiedPosition.z == 26.5)
        {
            simplifiedPosition = new Vector3(-0.5f, 0, 25.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //simplify camera position to its grid position
        Vector3 simplifiedPlayer = new Vector3(Mathf.Round(Camera.main.transform.position.x), Mathf.Round(Camera.main.transform.position.y), Mathf.Round(Camera.main.transform.position.z));

        //Debug.Log(simplifiedPosition);
        //Debug.Log(simplifiedPlayer);

        if(simplifiedPlayer == simplifiedPosition)
        {
            if (talking == false)
            {
                Talk();
                lineNumber = 0;
                talking = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Got to the next line");
                lineNumber += 1;
                Talk();
            }
        }

        if(simplifiedPlayer != simplifiedPosition && talking)
        {
            talking = false;
            for (int i = letterParent.childCount - 1; i >= 0; i--)
            {
                Destroy(letterParent.GetChild(i).gameObject);
            }
        }


        //animate the text
        //raise the letter by 0.1f, per letter

        if (letterAnimArray != null && currentAnimLetter < letterAnimArray.Length)
        {
            letterAnimArray[currentAnimLetter].transform.localPosition = new Vector3(letterAnimArray[currentAnimLetter].transform.localPosition.x, letterAnimArray[currentAnimLetter].transform.localPosition.y + Time.deltaTime * goingDown * 1.5f, letterAnimArray[currentAnimLetter].transform.localPosition.z);
            if (letterAnimArray[currentAnimLetter].transform.localPosition.y > 0.1f)
            {
                goingDown = -1;
            }

            if (letterAnimArray[currentAnimLetter].transform.localPosition.y <= 0f && goingDown == -1)
            {
                currentAnimLetter += 1;

                if (currentAnimLetter == letterAnimArray.Length)
                    currentAnimLetter = 0;

                goingDown = 1;
            }
        }
    }

    void Talk()
    {

        for(int i = letterParent.childCount - 1; i >= 0; i--)
        {
            Destroy(letterParent.GetChild(i).gameObject);
        }
        //Make a new textmeshcomponent for each character and move them independently 

        if (lineNumber < myLine.Length)
        {
            char[] characters = myLine[lineNumber].ToCharArray();
            GameObject[] characterGameObjects = new GameObject[characters.Length];

            for(int i = 0; i < characters.Length; i++)
            {
                //create a new gameobject
                characterGameObjects[i] = Instantiate(letterPrefab);
                characterGameObjects[i].GetComponent<TextMeshPro>().text = characters[i].ToString();
                characterGameObjects[i].transform.parent = letterParent;
                //characters.Length * 0.2 / 2;
                //characterGameObjects[i].transform.localPosition = new Vector3(-(characters.Length * 0.15f / 2) + (0.15f * i - 1), 0, 0);
                characterGameObjects[i].transform.localPosition = new Vector3(-(characters.Length * 0.15f / 2) + (0.15f * i), 0, 0);
                characterGameObjects[i].transform.localScale = new Vector3(0.06653757f, 0.06653757f, 0.06653757f);
                //0.2f verschil
                currentAnimLetter = 0;
                
            }

            letterAnimArray = characterGameObjects;
            //GameObject.Find("NPCDialogue").GetComponent<TextMeshProUGUI>().text = myLine[lineNumber];
        }

        else
        {
            letterAnimArray = null;
        }
    }

    public void UpdateMyTicket()
    {
        myTicket.transform.position = ticketPos.position;
        myTicket.transform.forward = ticketPos.transform.right;
        myTicket.transform.parent = this.gameObject.transform;
    }
}
