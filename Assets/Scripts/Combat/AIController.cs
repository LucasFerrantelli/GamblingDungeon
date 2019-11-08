using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Belligerent belligerent;

    private void FixedUpdate()
    {
        if(belligerent.isPlaying)
        {
            belligerent.AskForEndTurn();
        }
    }
}
