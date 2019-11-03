﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCard : MonoBehaviour
{
    public string gameplayName;
    public int[] cost;

    public EffectStruct[] effects;

    public int nbrOfUtilisation;
    public LocalizedText names;
    public LocalizedText descriptions;
}

