using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score;

    public static ScoreManager instance;
    private void Awake()
    {
        instance = this;
    }
}
