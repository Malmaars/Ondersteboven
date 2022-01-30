using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    int lineNumber;
    public string[] myLine;
    public Vector3 simplifiedPosition;
    public bool talking;

    Transform letterParent;
    public GameObject letterPrefab;

    public Transform cameraPos;

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
        simplifiedPosition = new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z));

        if (simplifiedPosition.x == -1 && simplifiedPosition.z == 9)
        {
            simplifiedPosition = new Vector3(0, 1, 8);
        }

        if (simplifiedPosition.x == -1 && simplifiedPosition.z == 10)
        {
            simplifiedPosition = new Vector3(0, 1, 11);
        }

        if (simplifiedPosition.x == -1 && simplifiedPosition.z == 13)
        {
            simplifiedPosition = new Vector3(0, 1, 14);
        }

        if (simplifiedPosition.x == -1 && simplifiedPosition.z == 18)
        {
            simplifiedPosition = new Vector3(0, 1, 17);
        }

        if (simplifiedPosition.x == -1 && simplifiedPosition.z == 19)
        {
            simplifiedPosition = new Vector3(0, 1, 20);
        }

        if (simplifiedPosition.x == -1 && simplifiedPosition.z == 24)
        {
            simplifiedPosition = new Vector3(0, 1, 23);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //simplify camera position to its grid position
        //Debug.Log(simplifiedPosition);
        //Debug.Log(simplifiedPlayer);

        //if(simplifiedPlayer == simplifiedPosition)
        //{
        //    if (talking == false)
        //    {
        //        Talk();
        //        lineNumber = 0;
        //        talking = true;
        //    }

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Debug.Log("Got to the next line");
        //        lineNumber += 1;
        //        Talk();
        //    }
        //}

        //if(simplifiedPlayer != simplifiedPosition && talking)
        //{
        //    talking = false;
        //    for (int i = letterParent.childCount - 1; i >= 0; i--)
        //    {
        //        Destroy(letterParent.GetChild(i).gameObject);
        //    }
        //}

        //Debug.Log(talking);
        if (talking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Got to the next line");
                lineNumber += 1;
                Talk();
            }

            if(Input.GetAxisRaw("Vertical") == -1)
            {
                //fuck you
                talking = false;
            }
        }

        else
        {
            if (letterParent != null)
            {
                for (int i = letterParent.childCount - 1; i >= 0; i--)
                {
                    Destroy(letterParent.GetChild(i).gameObject);
                }
            }

            lineNumber = 0;
        }


        //animate the text
        //raise the letter by 0.1f, per letter

        if (letterAnimArray != null && currentAnimLetter < letterAnimArray.Length)
        {
            letterAnimArray[currentAnimLetter].transform.localPosition = new Vector3(letterAnimArray[currentAnimLetter].transform.localPosition.x, letterAnimArray[currentAnimLetter].transform.localPosition.y + Time.deltaTime * goingDown * 45f, letterAnimArray[currentAnimLetter].transform.localPosition.z);
            if (letterAnimArray[currentAnimLetter].transform.localPosition.y > 10f)
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

    public void Talk()
    {
        letterParent = GameObject.FindGameObjectWithTag("Dialogue").transform;

        for (int i = letterParent.childCount - 1; i >= 0; i--)
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
                characterGameObjects[i].GetComponent<TextMeshProUGUI>().text = characters[i].ToString();
                characterGameObjects[i].transform.parent = letterParent;
                //characters.Length * 0.2 / 2;
                //characterGameObjects[i].transform.localPosition = new Vector3(-(characters.Length * 0.15f / 2) + (0.15f * i - 1), 0, 0);
                characterGameObjects[i].transform.localPosition = new Vector3(-(characters.Length * 40 / 2) + (40 * i), 0, 0);
                characterGameObjects[i].transform.localScale = new Vector3(2,2,2);
                //0.2f verschil
                currentAnimLetter = 0;
                
            }

            letterAnimArray = characterGameObjects;
            //GameObject.Find("NPCDialogue").GetComponent<TextMeshProUGUI>().text = myLine[lineNumber];
        }

        else
        {
            letterAnimArray = null;
            talking = false;
        }
    }

    public void UpdateMyTicket()
    {
        myTicket.transform.position = ticketPos.position;
        myTicket.transform.forward = ticketPos.transform.up;
        myTicket.transform.parent = this.gameObject.transform;
    }
}
