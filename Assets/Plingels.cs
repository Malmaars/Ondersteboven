using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plingels : MonoBehaviour
{
    float plingelTimer;

    public AudioClip[] plingels;

    private void Start()
    {
        plingelTimer = Random.Range(30, 120);
    }
    // Update is called once per frame
    void Update()
    {
        plingelTimer -= Time.deltaTime;

        if(plingelTimer < 0)
        {
            plingelTimer = Random.Range(30, 90);

            int randomOneShot = Random.Range(0, 3);
            GetComponent<AudioSource>().PlayOneShot(plingels[randomOneShot]);
        }
    }
}
