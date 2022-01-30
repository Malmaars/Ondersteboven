using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPages : MonoBehaviour
{
    public bool leftRight;

    private void OnMouseDown()
    {
        //next or previous page

        Book book = FindObjectOfType<Book>();

        if (!leftRight)
        {
            //go a page back
            if (book.pageNumber < 4)
            {
                book.pageNumber++;
            }

            //array of textures, scroll through them
        }

        else
        {
            if(book.pageNumber > 0)
            {
                book.pageNumber--;
            }
            //go forward
        }

        book.WisselPagina();
    }
}
