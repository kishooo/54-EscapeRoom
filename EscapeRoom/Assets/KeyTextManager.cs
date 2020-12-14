using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyTextManager : MonoBehaviour
{
    public GameObject player;
    playerController PC;
    // Start is called before the first frame update
    void Start()
    {
        PC = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PC != null)
        {
            GetComponent<TextMeshProUGUI>().text = "Keys: " + PC.keys;
        }
        else
        {
            PC = player.GetComponent<playerController>();
        }
        
    }
}
