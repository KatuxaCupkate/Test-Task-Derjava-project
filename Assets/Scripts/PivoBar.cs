using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PivoBar : MonoBehaviour
{
    [SerializeField] private Sprite[] fillPivko;
    public Image pivoImage;
    public bool isEmpty { get; private set; }
    
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
                isEmpty = true;
                break;
            case PivoStatus.Quarter:
                pivoImage.sprite = fillPivko[1];
                isEmpty = false;
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
