using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

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
        
    }

    void Update()
    {
        
    }

    public void AddGold(int addGold)
    {
        gold += addGold;
    }
}
