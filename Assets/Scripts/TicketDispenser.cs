using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketDispenser : MonoBehaviour
{
    //I can cycle through available ticket with an array
    public GameObject ticket;
    public Transform ticketLocation;

    public bool ticketDispensed;
    private void OnMouseDown()
    {
        //dispense ticket
        if (!ticketDispensed)
        {
            Instantiate(ticket, ticketLocation.position, ticketLocation.rotation, null);
            ticketDispensed = true;
        }
    }
}
