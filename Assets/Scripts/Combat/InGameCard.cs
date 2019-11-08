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

    public Cards soToReferTo;

    public bool gotADrawEffect;

    [Header("Gameplay Infos")]
    public Belligerent owner;
    public bool cantUseForDrawEffect;

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

    public void UpdateCardStatut()
    {
        if (owner.stunned || cantUseForDrawEffect)
        {
            useButton.interactable = false;
        }
        else
        {
            useButton.interactable = true;
        }
    }

    public void PlayCard()
    {
        CombatManager.Instance.UseCard(owner, this);
    }
}

