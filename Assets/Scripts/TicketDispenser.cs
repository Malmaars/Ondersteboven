using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketDispenser : MonoBehaviour
{
    //I can cycle through available ticket with an array
    public GameObject ticket;
    public Material[] ticketTypes;
    //public Transform ticketLocation;

    public bool ticketDispensed;

    public int destinationOnTicket;
    private void OnMouseDown()
    {
        Debug.Log("ClickButton");
        Player player = FindObjectOfType<Player>();
        //dispense ticket

        Debug.Log(Vector3.Distance(Camera.main.transform.position, this.transform.position));
        if (player.inventory.Count < 3 && Vector3.Distance(Camera.main.transform.position, this.transform.position) < 0.74f)
        {
            GetComponent<Animator>().SetTrigger("Stamp");

            GameObject temp = Instantiate(ticket);
            temp.GetComponent<Ticket>().destination = destinationOnTicket;
            temp.GetComponentInChildren<MeshRenderer>().material = ticketTypes[destinationOnTicket];


            //change the ticket type
            player.inventory.Add(temp);
            player.UpdateInventory();
        }
    }
}
