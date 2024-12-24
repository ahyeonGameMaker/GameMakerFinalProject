using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFighter
{
    public void TakeDamage(float damage);
    GameObject FighterObject
    {
        get;
    }
}
