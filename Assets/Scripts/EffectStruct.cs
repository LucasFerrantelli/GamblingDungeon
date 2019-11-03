using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectStruct
{
    public Effects.Effect effect;
    public Effects.Parameter parameter;
    public float parameterValue;
    public Effects.Target target;
}
