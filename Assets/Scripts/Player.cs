using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool horizontalInputCheck, verticalInputCheck, camLock;
    public bool bookLock;

    Vector3 newPos, oldPos;
    Quaternion newRot, oldRot;

    GameObject[] walkableTiles;

    public Transform stampView;
    public List<GameObject> inventory;
    public GameObject hand;

    public NPC currentNPC;

    bool camFix;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);
        //I can set this to any point to start the player wherever I want
        newPos = transform.position;
        newRot = transform.rotation;

        walkableTiles = GameObject.FindGameObjectsWithTag("Walkable");
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (bookLock == false)
        {
            if (camLock)
            {
                if (Input.GetAxisRaw("Vertical") == -1)
                {
                    //go out of cam lock
                    newPos = oldPos;
                    newRot = oldRot;
                    currentNPC = null;
                }

                if (transform.position == oldPos && transform.rotation == oldRot && Input.GetAxisRaw("Vertical") == 0)
                {
                    camLock = false;
                }
            }

            LerpToPosition();
            RotateToPosition();
        }

        //if(transform.position.x < -2)
        //{
        //    transform.position = new Vector3(0, transform.position.y, transform.position.z);
        //}
    }

    void RotateToPosition()
    {
        //I check if left or right is pressed
        if (Input.GetAxisRaw("Horizontal") != 0 && !horizontalInputCheck && !verticalInputCheck && !camLock)
        {
            //Debug.Log("Q: VerticalInput: " + Input.GetAxisRaw("Vertical") + ", Horizontal Input: " + Input.GetAxisRaw("Horizontal") + ", VerticalInputCheck: " + verticalInputCheck + ", HorizontalInpuCheck: " + horizontalInputCheck);
            horizontalInputCheck = true;
            //if it is, I set a destination target to rotate to
            newRot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90 * Input.GetAxisRaw("Horizontal"), transform.rotation.eulerAngles.z);
        }

        if(newRot != transform.rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 40);

            if(Vector3.Distance(transform.rotation.eulerAngles, newRot.eulerAngles) < 1f)
            {
                //Debug.Log(newRot.eulerAngles);
                transform.rotation = newRot;
            }
        }

        if (transform.rotation == newRot && Input.GetAxisRaw("Horizontal") == 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Mathf.RoundToInt(transform.rotation.eulerAngles.y), transform.rotation.eulerAngles.z);
            horizontalInputCheck = false;
        }
    }

    void LerpToPosition()
    {
        //I check if up or down is pressed
        if (Input.GetAxisRaw("Vertical") != 0 && !verticalInputCheck && !horizontalInputCheck && !camLock)
        {
            if (!camLock)
            {
                Transform specialCamCheck = CheckSpecialCamPosition();
                if (specialCamCheck != null)
                {
                    camLock = true;
                    oldPos = transform.position;
                    oldRot = transform.rotation;
                    newPos = specialCamCheck.position;
                    newRot = specialCamCheck.rotation;
                    return;
                }

            }

            for (int i = 0; i < walkableTiles.Length; i++)
            {
                //Debug.Log(transform.position + transform.forward * Input.GetAxisRaw("Vertical"));
                if (transform.position + transform.forward * Input.GetAxisRaw("Vertical") == walkableTiles[i].transform.position)
                {
                    break;
                }

                if (i == walkableTiles.Length - 1)
                {
                    return;
                }
            }

            //Debug.Log("V: VerticalInput: " + Input.GetAxisRaw("Vertical") + ", Horizontal Input: " + Input.GetAxisRaw("Horizontal") + ", VerticalInputCheck: " + verticalInputCheck + ", HorizontalInpuCheck: " + horizontalInputCheck);
            verticalInputCheck = true;
            //if it is, I set a destination target forward or backward from where the player is facing
            newPos = transform.position + transform.forward.normalized * Input.GetAxisRaw("Vertical");
            //Debug.Log(newPos);
        }

        if (transform.position != newPos)
        {
            //we lerp to the destination
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 40);

            //If we arrive where we want to go, we can set a new destination again (reset bool)
            if (Vector3.Distance(transform.position, newPos) < 0.01f)
            {
                //Debug.Log("moving");

                if (newPos.y == 1)
                    transform.position = new Vector3(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y), Mathf.RoundToInt(newPos.z));

                else
                {
                    transform.position = newPos;
                }

            }
        }

        if (newPos == transform.position && Input.GetAxisRaw("Vertical") == 0)
        {
            verticalInputCheck = false;
        }
    }

    Transform CheckSpecialCamPosition()
    {

        Vector3 CheckPos = transform.position + transform.forward * Input.GetAxisRaw("Vertical");
        //if a special view is in that position, switch to the special view.
        //GameObject[] specialviews = GameObject.FindGameObjectsWithTag("SpecialView");

        //foreach(GameObject view in specialviews)
        //{
        //    if(view.transform.position.x < CheckPos.x + 0.5f && view.transform.position.x > CheckPos.x - 0.5f &&
        //       view.transform.position.y < CheckPos.y + 0.5f && view.transform.position.y > CheckPos.y- 0.5f &&
        //       view.transform.position.z < CheckPos.z + 0.5f && view.transform.position.z > CheckPos.z - 0.5f)
        //    {
        //        //there is a special cam there. go to the special cam.
        //        return view.transform;
        //    }
        //}

        NPC[] npcs = FindObjectsOfType<NPC>();

        foreach(NPC singleNPC in npcs)
        {
            //Debug.Log(CheckPos);
            //Debug.Log(singleNPC.simplifiedPosition);
            if(singleNPC.simplifiedPosition == CheckPos)
            {
                currentNPC = singleNPC;
                //character can start talking
                singleNPC.talking = true;
                singleNPC.Talk();
                return singleNPC.cameraPos;

            }
        }

        if(CheckPos == new Vector3(0, 1, 25))
        {
            return stampView;
        }

        currentNPC = null;

        return null;
    }

    public void UpdateInventory()
    {
        if(inventory.Count > 0)
        {
            //show them
            hand.transform.localPosition = new Vector3(0.185f, -0.2f, 0.5f);
            for(int i = 0; i < inventory.Count; i++)
            {
                //maak een waaier van tickets?

                //change this (hand.transform) to another transform later
                inventory[i].transform.parent = hand.transform;
                inventory[i].transform.position = hand.transform.GetChild(0).transform.position;
                inventory[i].transform.localPosition = new Vector3(0, 0.3f, 0 + i * 0.01f);
                //inventory[i].transform.rotation = hand.transform.GetChild(0).transform.rotation;

                if (inventory.Count == 2)
                {
                    inventory[i].transform.rotation = hand.transform.GetChild(i + 1).transform.rotation;
                }
                else
                    inventory[i].transform.rotation = hand.transform.GetChild(i).transform.rotation;
            }
            //max 3 tickets
        }

        else
        {
            //put away the hand
            hand.transform.localPosition = new Vector3(0.35f, -0.5f, 0.5f);
        }
    }
}
