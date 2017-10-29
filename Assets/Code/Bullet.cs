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
        /// Bullet Speed
        /// </summary>
        private float _speed = 5f;

        /// <summary>
        /// Deal with bullet collisions: kill the bullet, do damage to other players
        /// </summary>
        /// <param name="other"></param>
        internal void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.GetComponent<Player>() != null)
            {
	            if (_flag)
	            {
		            var points = Creator == other.gameObject ? -1 : 2;
		            ScoreManager.IncreaseScore(Creator, points);
		            Destroy(gameObject);
	            }     
            }
	        if (!other.gameObject.CompareTag("Wall")) return;
	        if(_flag)
		        Destroy(gameObject);
        }

        /// <summary>
        /// Initialize projectile
        /// </summary>
        public void Init(GameObject creator, Vector3 pos, Vector3 direction)
        {
            Creator = creator;
            GetComponent<SpriteRenderer>().color = creator.GetComponent<Player>().BulletColor;
            transform.position = pos;
            GetComponent<Rigidbody2D>().velocity = _speed*direction;
			_flag = true;
        }
    }
}


