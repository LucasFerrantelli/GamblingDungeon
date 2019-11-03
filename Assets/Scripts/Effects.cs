using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Effects 
{
    public enum Effect { DealDamage, Heal, Draw, AddPower, SetPower, SetHp }
    public enum Parameter { Int, Modifier, Power, CardsOnBoard, ActualHP }
    public enum Target { Himself, Enemy, Both}
}

