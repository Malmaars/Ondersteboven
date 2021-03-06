using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public AudioClip openSound, closeSound;
    public GameObject overlayColliders;
    public Transform openBookLoc, closedBookLoc;
    public bool open;

    bool bookInHand;

    Animator animator;

    public int pageNumber;
    public Material[] pages;
    public GameObject boekVoorMaterial;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Open();
    }

    private void Update()
    {
        if (!bookInHand)
        {
            if (open)
            {
                transform.position = Vector3.Lerp(transform.position, openBookLoc.position, Time.deltaTime * 3);
            }

            else
            {
                if (Vector3.Distance(transform.position, closedBookLoc.position) <= 0.01f)
                    bookInHand = true;

                transform.position = Vector3.Lerp(transform.position, closedBookLoc.position, Time.deltaTime * 3);
            }
        }
    }
    private void OnMouseDown()
    {
        //get in ur face
        if(!open)
        {
            Open();
        }
    }

    void Open()
    {
        //LERP
        //transform.position = openBookLoc.position;
        open = true;
        FindObjectOfType<Player>().bookLock = true;
        //not allowed to walk anymore
        //activate page overlay

        //animate book opening
        animator.SetBool("Open", true);
        overlayColliders.SetActive(true);
        bookInHand = false;

        GetComponent<AudioSource>().PlayOneShot(openSound);
    }

    public void Close()
    {
        FindObjectOfType<Player>().bookLock = false;
        animator.SetBool("Open", false);
        //transform.position = closedBookLoc.position;
        open = false;

        overlayColliders.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(closeSound);
    }

    public void WisselPagina()
    {
        boekVoorMaterial.GetComponent<SkinnedMeshRenderer>().material = pages[pageNumber];
    }
}
