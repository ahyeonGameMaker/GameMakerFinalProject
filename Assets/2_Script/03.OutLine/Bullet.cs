using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private void Start()
    {
        Invoke("AutoDestroy", 3f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
