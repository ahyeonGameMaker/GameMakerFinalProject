using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject EffectPrefab;

    void Update()
    {
		transform.position += Vector3.up * Time.deltaTime * 5f;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Projectile"))
		{

            //GameObject effectObj = Instantiate(EffectPrefab);
            //effectObj.transform.position = collision.transform.position;

            Destroy(collision.gameObject);
			Destroy(gameObject);

			GameManager05.Instance.Score++;
		}
	}
}
