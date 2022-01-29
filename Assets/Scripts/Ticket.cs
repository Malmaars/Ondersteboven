using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{
    Player player;
    bool inHand;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnMouseDown()
    {
        //add ticket to hand
        //if hand is full, don't pick up ticket

        if (!inHand)
        {
            if (player.inventory.Count < 3)
            {
                player.inventory.Add(this.gameObject);
                player.UpdateInventory();
                inHand = true;
                FindObjectOfType<TicketDispenser>().ticketDispensed = false;
            }
        }

        else
        {
            //give ticket to NPC if they are in view.
            NPC[] allNPCs = FindObjectsOfType<NPC>();

            Vector3 simplifiedPlayer = new Vector3(Mathf.Round(Camera.main.transform.position.x), Mathf.Round(Camera.main.transform.position.y), Mathf.Round(Camera.main.transform.position.z));

            foreach (NPC npc in allNPCs)
            {
                if(simplifiedPlayer == npc.simplifiedPosition)
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
}
