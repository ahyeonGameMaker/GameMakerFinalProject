using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public float moveSpeed = 2f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 20f);
    }

    void Update()
    {
        float moveZ = moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, -moveZ);
    }
}
