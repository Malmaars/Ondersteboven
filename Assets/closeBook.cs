using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeBook : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<Book>().Close();
    }
}
