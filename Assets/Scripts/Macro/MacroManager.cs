using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroManager : MonoBehaviour
{
    static public PlayerInstance player;
    static public Persona opponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitiateFight(Persona _opponent)
    {
        opponent = _opponent;
        //GoToSceneFight
    }
}
