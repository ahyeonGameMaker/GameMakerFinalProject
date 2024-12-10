using System.Collections;
using UnityEngine;

public class Taxi : MonoBehaviour
{
	public GameObject BulletPrefab;
	public GameObject EffectPrefab;
	public Transform left;
	public Transform right;
	public float moveSpeed = 5f; // 이동 속도
	public AudioSource AudioSource;
	public AudioClip GetShotClip;
	public AudioClip ShootGunClip;

    private void Start()
	{
		StartCoroutine(FireBullet());
	}

	IEnumerator FireBullet()
	{
		while (true)
		{
			if (!GameManager05.Instance.ScriptTime && !GameManager05.Instance.GameClear)
			{
				GameObject leftBullet = Instantiate(BulletPrefab, left.position, left.rotation);
				Destroy(leftBullet, 2);
				AudioSource.PlayOneShot(ShootGunClip);
				GameObject rightBullet = Instantiate(BulletPrefab, right.position, right.rotation);
				Destroy(rightBullet, 2);
				AudioSource.PlayOneShot(ShootGunClip);
			}
			yield return new WaitForSeconds(1f);
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
		if (collision.tag.Equals("Projectile"))
		{
			if (!GameManager05.Instance.GameClear)
			{
				GameObject effectObj = Instantiate(EffectPrefab);
				effectObj.transform.position = transform.position;

				GameManager05.Instance.Score -= 2;
				AudioSource.PlayOneShot(GetShotClip);

			}
			Destroy(collision.gameObject, 0.1f);
		}
	}

}
