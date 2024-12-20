using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOff : MonoBehaviour
{
    void Start()
    {
        Invoke("Off", 7f);
    }

    void Off()
    {
        gameObject.SetActive(false);
    }
}
