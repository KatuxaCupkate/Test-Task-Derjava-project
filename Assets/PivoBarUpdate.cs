using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivoBarUpdate : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerCombat;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat=GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
