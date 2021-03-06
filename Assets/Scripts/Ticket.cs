using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{
    Player player;
    bool inHand;
    public int destination;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnMouseDown()
    {
        Debug.Log("I Click on ticket");
        //add ticket to hand
        //if hand is full, don't pick up ticket

        //if (!inHand)
        //{
        //    if (player.inventory.Count < 3)
        //    {
        //        player.inventory.Add(this.gameObject);
        //        player.UpdateInventory();
        //        inHand = true;
        //        FindObjectOfType<TicketDispenser>().ticketDispensed = false;
        //    }
        //}

        //give ticket to NPC if they are in view.
        NPC[] allNPCs = FindObjectsOfType<NPC>();

        Debug.Log(allNPCs.Length);
        foreach (NPC npc in allNPCs)
        {
            //Debug.Log(FindObjectOfType<Player>().currentNPC);
            //Debug.Log(npc);
            //this doesn't work anymore
            if (FindObjectOfType<Player>().currentNPC != null && FindObjectOfType<Player>().currentNPC == npc)
            {
                //there is an npc in ur view
                if (npc.myTicket == null)
                {
                    //they don't have a ticket yet
                    //so give em a ticket
                    player.inventory.Remove(this.gameObject);
                    player.UpdateInventory();

                    npc.myTicket = this;
                    //update the position of the ticket
                    npc.UpdateMyTicket();

                }
            }
        }


    }
}
