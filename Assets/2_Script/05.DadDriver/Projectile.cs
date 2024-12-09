using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TextMeshPro Tmp;

    // Start is called before the first frame update
    public void SetText(string text)
    {
        Tmp.text = text;
    }
}
