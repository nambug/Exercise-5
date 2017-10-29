using UnityEngine;


namespace Code
{
    /// <summary>
    /// The ordnance shot by the tanks
    /// </summary>
    public class Bullet : MonoBehaviour {
        /// <summary>
        /// Who shot us
        /// </summary>
        public GameObject Creator;

		private bool _flag = false;

        /// <summary>
        /// How fast to move
        /// </summary>
        public float Speed = 5f;

        /// <summary>
        /// Do dammage if hitting player
        /// </summary>
        /// <param name="collision"></param>
        internal void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.GetComponent<Player>() != null)
            {
	            if (_flag)
	            {
		            var points = Creator != other.gameObject ? 10 : -20;
		            ScoreManager.IncreaseScore(Creator, points);
		            Destroy(gameObject);
	            }     
            }
			if (other.gameObject.CompareTag ("Wall")) {
				if(_flag)
					Destroy(gameObject);
			}
        }

        /// <summary>
        /// Start the projectile moving.
        /// </summary>
        /// <param name="creator">Who's shooting</param>
        /// <param name="pos">Where to place the projectile</param>
        /// <param name="direction">Direction to move in (unit vector)</param>
        public void Init(GameObject creator, Vector3 pos, Vector3 direction)
        {
            Creator = creator;
            GetComponent<SpriteRenderer>().color = creator.GetComponent<Player>().ProjectileColor;
            transform.position = pos;
            GetComponent<Rigidbody2D>().velocity = Speed*direction;
			_flag = true;
        }
    }
}


