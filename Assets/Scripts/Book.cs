using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject overlayColliders;
    public Transform openBookLoc, closedBookLoc;
    public bool open;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (open)
        {
            transform.position = Vector3.Lerp(transform.position,openBookLoc.position, 0.01f);
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, closedBookLoc.position, 0.01f);
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
        Player player = FindObjectOfType<Player>();
        player.bookLock = true;
        //not allowed to walk anymore
        //activate page overlay

        //animate book opening
        animator.SetBool("Open", true);
        overlayColliders.SetActive(true);
    }

    public void Close()
    {
        animator.SetBool("Open", false);
        //transform.position = closedBookLoc.position;
        open = false;

        overlayColliders.SetActive(false);
    }
}
