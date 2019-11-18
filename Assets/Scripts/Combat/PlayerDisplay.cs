using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDisplay : MonoBehaviour
{
    public Belligerent belligerent;
    public List<SpriteRenderer> bars;
    public List<SpriteRenderer> separationBars;

    public Button drawButton;
    public Button endTurnButton;
    public TextMeshProUGUI cardsInDeckText;

    public GameObject barInitialPosition;

    public Color fullBarColor;
    public Color emptyBarColor;

    [Header ("Prefabs")]
    public GameObject barPrefab;
    public GameObject seperationPrefab;

    public void InitializeUI()
    {
        InitializeBar();
        InitializeLadder();

    }

    public void InitializeBar()
    {
        for (int i = 0; i < belligerent.powerCeiling; i++)
        {
            GameObject _bar = Instantiate(barPrefab, transform);
            bars.Add(_bar.GetComponent<SpriteRenderer>());
            bars[i].transform.position = barInitialPosition.transform.position + new Vector3(0, 0.5f, 0) * i;
        }
    }

    void InitializeLadder()
    {
        int _ladderValue = 0;
        for (int i = 0; i < belligerent.ladder.ladder.Length; i++)
        {
            if(_ladderValue != belligerent.ladder.ladder[i])
            {
                GameObject _separationBar =  Instantiate(seperationPrefab, transform);
                separationBars.Add(_separationBar.GetComponent<SpriteRenderer>());
                _separationBar.transform.position = bars[i - 1].transform.position;
            }
            _ladderValue = belligerent.ladder.ladder[i];
        }
    }

    public void ColorBars(int barsToColor)
    {
        if(barsToColor > 0)
        {
            for (int i = 0; i < barsToColor; i++)
            {
                bars[i].color = fullBarColor;
            }
            for (int i = bars.Count; i < bars.Count - barsToColor; i--)
            {
                bars[i].color = emptyBarColor;
            }
        }
        else
        {
            for (int i = 0; i < bars.Count; i++)
            {
                bars[i].color = emptyBarColor;
            }
        }
        
    }

    private void FixedUpdate()
    {
        
        UpdateDrawButtonStatut();
        UpdateEndTurnButtonStatut();
        UpdateNbrOfCardText();
    }

    void UpdateNbrOfCardText()
    {
        cardsInDeckText.text = "Draw (" + belligerent.actualDeck.Count + ")";
    }

    private void UpdateEndTurnButtonStatut()
    {
        if(belligerent.isPlaying)
        {
            endTurnButton.interactable = true;
        }
        else
        {
            endTurnButton.interactable = false;
        }
    }

    void UpdateDrawButtonStatut()
    {
        if(belligerent.canDraw)
        {
            drawButton.interactable = true;
        }
        else
        {
            drawButton.interactable = false;
        }
    }
}
