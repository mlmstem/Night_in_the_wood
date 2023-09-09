using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=DLAIYSMYy2g

public class DestroyItem : MonoBehaviour
{
    public void DestroyWhenUsed()
    {
        // Destroy the clicked item
        Destroy(gameObject);
    }

}