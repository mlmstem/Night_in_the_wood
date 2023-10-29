using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour

{
    public AudioSource Footstepsound;

    void Update()

    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Footstepsound.enabled = true;

        }
        else
        {
            Footstepsound.enabled = false;
        }

    }
}
