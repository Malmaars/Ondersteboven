using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        FindObjectOfType<Player>().enabled = true;
        transform.parent.gameObject.SetActive(false);
    }
}
