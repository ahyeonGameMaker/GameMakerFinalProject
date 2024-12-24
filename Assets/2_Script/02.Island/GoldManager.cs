using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    public Text goldText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    public int gold;


    void Start()
    {
        goldText.text = $"{gold}G";
    }

    void Update()
    {
        
    }

    public void AddGold(int addGold)
    {
        gold += addGold;
        goldText.text = $"{gold}G";
    }
}
