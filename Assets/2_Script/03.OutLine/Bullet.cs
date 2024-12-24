using UnityEngine;

namespace OutLine
{

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
}
