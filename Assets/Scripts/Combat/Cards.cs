using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_newCard", menuName = "ScriptableObjects/Card")]
public class Cards : ScriptableObject
{
    public string gameplayName;
    public int[] cost;

    public EffectStruct[] effects;

    public int nbrOfUtilisation;
    public LocalizedText names;
    public LocalizedText descriptions;

    public bool gotADrawEffect;

}


