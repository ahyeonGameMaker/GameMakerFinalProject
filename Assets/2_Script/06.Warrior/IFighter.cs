using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFighter
{
    public void TakeDamage(float damage, AudioSource attackSound);
    AudioSource AttackSound 
    {
        get; 
    }
    GameObject FighterObject
    {
        get;
    }
}
