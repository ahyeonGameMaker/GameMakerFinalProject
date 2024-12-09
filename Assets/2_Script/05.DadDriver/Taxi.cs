using System.Collections;
using UnityEngine;

public class Taxi : MonoBehaviour
{
	public GameObject BulletPrefab;
	public Transform left;
	public Transform right;
	public float moveSpeed = 5f; // 이동 속도

	private void Start()
	{
		StartCoroutine(FireBullet());
	}

	IEnumerator FireBullet()
	{
		while (true)
		{
			if (!GameManager05.Instance.ScriptTime)
			{
				GameObject leftBullet = Instantiate(BulletPrefab, left.position, left.rotation);
				Destroy(leftBullet, 2);
				GameObject rightBullet = Instantiate(BulletPrefab, right.position, right.rotation);
				Destroy(rightBullet, 2);
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	void Update()
	{
		Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

		if (moveDirection.magnitude >= 0.1f)
			transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Tax Trigger " + collision.name);
		if (collision.tag.Equals("Projectile"))
		{
			GameManager05.Instance.Score -= 2;
			Destroy(collision.gameObject, 0.1f);
		}
	}

}
