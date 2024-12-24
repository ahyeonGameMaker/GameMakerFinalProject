using Cinemachine;
using UnityEngine;

namespace OutLine
{

	public class PlayerAttack : MonoBehaviour
	{
		public GameObject bulletPrefab; // �Ѿ� ������
		public Transform firePoint; // �Ѿ� �߻� ��ġ

		public float delay;

		private bool canShoot = true;

		private Animator animator;
		private PlayerMove playerMove;
		private CinemachineImpulseSource cinemachineImpulseSource;

		private void Awake()
		{
			animator = GetComponent<Animator>();
			playerMove = GetComponent<PlayerMove>();
			cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
		}

		private void Update()
		{
			Shoot();
			SlowMotion();
		}

		private void Shoot()
		{
			if (Input.GetMouseButtonDown(0) && canShoot)
			{
				if (!playerMove.isJump)
				{
					canShoot = false;
					Invoke("CanShootOn", delay);

					playerMove.canMove = false;

					animator.SetTrigger("GunShoot");

					ShootBullet();

					cinemachineImpulseSource.GenerateImpulse();
				}
				else
				{
					canShoot = false;
					Invoke("CanShootOn", 0.15f);

					ShootBullet();

					cinemachineImpulseSource.GenerateImpulse();
				}
			}
		}

		void ShootBullet()
		{
			// ���콺 Ŭ�� ��ġ ���
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.z = 0;

			// �Ѿ� ȸ�� ����
			Vector3 direction = mousePosition - firePoint.position;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			// �Ѿ� ���� �� ���� ����
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
		}

		private void SlowMotion()
		{
			if (Input.GetMouseButton(1))
			{
				Time.timeScale = 0.5f;
			}
			else
			{
				Time.timeScale = 1f;
			}
		}

		private void CanShootOn()
		{
			canShoot = true;
		}

		private void CanMoveOn()
		{
			playerMove.canMove = true;
		}
	}
}
