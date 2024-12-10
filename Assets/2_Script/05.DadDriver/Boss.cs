using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public GameObject player;
	public GameObject Projectile;
	public GameObject EffectPrefab;

	private float ProjecttileSpeed = 3f;
	private string[] ProjecttileMsg = { 
		"귀찮음", "버그", "수정", "짜증", "불만", "두려움", "피곤", "일정"};

	public float speed = 2f; // 이동 속도
	public float range = 3f; // 좌우 이동 거리

	void Start()
	{
		StartCoroutine(FireProjectiles());
	}
	void Update()
	{
		// 좌우 반복 이동
		float x = Mathf.PingPong(Time.time * speed, range * 2) - range;
		transform.position = new Vector3(x, transform.position.y, transform.position.z);

		if (GameManager05.Instance.GameClear)
		{
			GameObject effectObj = Instantiate(EffectPrefab);
			effectObj.transform.position = transform.position;

			Destroy(gameObject);
		}
	}

	//퍼지는 포탄 발사
	void FireSpreadProjectiles()
	{
		// 플레이어 방향 계산
		Vector2 direction = (player.transform.position - transform.position).normalized;

		int spreadProjectileCount = 3;
		for (int i = 0; i < spreadProjectileCount; i++)
		{
			float angle = 45f * (i - (spreadProjectileCount - 1) / 2f) / (spreadProjectileCount - 1);
			Vector2 spreadDirection = Quaternion.Euler(0, 0, angle) * direction;
			InstantiateProjecttile(spreadDirection);
		}
	}

	private void InstantiateProjecttile(Vector2 direction)
	{
		GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
		projectile.GetComponent<Projectile>().SetText(ProjecttileMsg[Random.Range(0, ProjecttileMsg.Length)]);
		Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
		rb.velocity = direction * ProjecttileSpeed;

		Destroy(projectile, 3f);
	}

	private IEnumerator FireProjectiles()
	{
		while (GameManager05.Instance.GameClear == false)
		{
			if (Random.Range(0, 2) == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					FireStraightProjectile();
					yield return new WaitForSeconds(1f);
				}
			}
			else
			{
				FireSpreadProjectiles();
				yield return new WaitForSeconds(1f);
			}
		}
	}

	private void FireStraightProjectile()
	{
		// 플레이어 방향 계산
		Vector2 direction = (player.transform.position - transform.position).normalized;

		// 단발 투사체 발사
		InstantiateProjecttile(direction);
	}
}
