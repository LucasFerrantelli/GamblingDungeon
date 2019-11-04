using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameCard : MonoBehaviour
{
    [Header ("Gameplay variables")]
    public string gameplayName;
    public int[] cost;

    public EffectStruct[] effects;

    public int nbrOfUtilisation;
    public LocalizedText names;
    public LocalizedText descriptions;

    [Header("Prefab Assignation")]
    public TextMeshPro nameField;
    public TextMeshPro descriptionField;
    public TextMeshPro costField;
    public Button useButton;


    public void UpdateInfos()
    {
        nameField.text = names.text[0];
        descriptionField.text = descriptions.text[0];

        costField.text = cost[0].ToString();
        
    }
}

