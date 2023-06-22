using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PivoBar : MonoBehaviour
{
    [SerializeField] private Sprite[] fillPivko;
    public Image pivoImage;
    private void Awake()
    {
        pivoImage = GetComponent<Image>();

    }

    public void SetPivoImage(PivoStatus pivo)
    {
        switch (pivo)
        {
            case PivoStatus.Empty:
                pivoImage.sprite = fillPivko[0];
                break;
            case PivoStatus.Quarter:
                pivoImage.sprite = fillPivko[1];
                break;
            case PivoStatus.Third:
                pivoImage.sprite = fillPivko[2];
                break;
            case PivoStatus.Half:
                pivoImage.sprite = fillPivko[3];
                break;
            case PivoStatus.QuarterHalf:
                pivoImage.sprite = fillPivko[4];
                break;
            case PivoStatus.Full:
                pivoImage.sprite = fillPivko[5];
                break;
        }
    }
    public enum PivoStatus
    {
        Empty,
        Quarter,
        Third,
        Half,
        QuarterHalf,
        Full
    }
}
