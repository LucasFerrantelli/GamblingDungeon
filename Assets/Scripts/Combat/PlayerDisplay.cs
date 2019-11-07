using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public Belligerent belligerent;
    public GameObject barPrefab;
    public List<SpriteRenderer> bars;

    public Button drawButton;

    public GameObject barInitialPosition;

    public Color fullBarColor;
    public Color emptyBarColor;


    public void InitializeBar()
    {
        for (int i = 0; i < belligerent.powerCeiling; i++)
        {
            GameObject _bar = Instantiate(barPrefab, transform);
            bars.Add(_bar.GetComponent<SpriteRenderer>());
            bars[i].transform.position = barInitialPosition.transform.position + new Vector3(0, 0.5f, 0) * i;
        }
    }

    public void ColorBars(int barsToColor)
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

    private void FixedUpdate()
    {
        UpdateDrawButtonStatut();
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
