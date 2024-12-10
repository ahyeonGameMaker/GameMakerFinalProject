using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public GameObject player;
	public GameObject Projectile;
	public GameObject EffectPrefab;

	private float ProjecttileSpeed = 3f;
	private string[] ProjecttileMsg = { 
		"������", "����", "����", "¥��", "�Ҹ�", "�η���", "�ǰ�", "����"};

	public float speed = 2f; // �̵� �ӵ�
	public float range = 3f; // �¿� �̵� �Ÿ�

	void Start()
	{
		StartCoroutine(FireProjectiles());
	}
	void Update()
	{
		// �¿� �ݺ� �̵�
		float x = Mathf.PingPong(Time.time * speed, range * 2) - range;
		transform.position = new Vector3(x, transform.position.y, transform.position.z);

		if (GameManager05.Instance.GameClear)
		{
			GameObject effectObj = Instantiate(EffectPrefab);
			effectObj.transform.position = transform.position;

			Destroy(gameObject);
		}
	}

	//������ ��ź �߻�
	void FireSpreadProjectiles()
	{
		// �÷��̾� ���� ���
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
		// �÷��̾� ���� ���
		Vector2 direction = (player.transform.position - transform.position).normalized;

		// �ܹ� ����ü �߻�
		InstantiateProjecttile(direction);
	}
}
