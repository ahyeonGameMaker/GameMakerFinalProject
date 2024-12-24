using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public Action endAttackListener;
    public Action startAttackListener;
    public Action dieAttackListener;
    public void EndAttack()
    {
        endAttackListener?.Invoke();
    }
    public void StartAttack()
    {
        startAttackListener?.Invoke();
    }
    public void Die()
    {
        dieAttackListener?.Invoke();
    }
}
