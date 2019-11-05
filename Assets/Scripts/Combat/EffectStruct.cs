using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectStruct
{
    public Effects.Effect effect;
    public ParameterStruct[] parameters;
    public Effects.Target target;
}

[System.Serializable]
public struct ParameterStruct
{
    public Effects.CombinaisonType combinaisonType;
    public Effects.Parameter parameter;
    public int parameterValue;
    public Effects.Target parametersOwner;
}
