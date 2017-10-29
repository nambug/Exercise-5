using System.Runtime.CompilerServices;
using UnityEngine;

namespace Code
{
    /// <summary>
    /// Implements player control of tanks, as well as collision detection.
    /// </summary>
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// Cooldown time between shots
        /// </summary>
        private float _cooldown = 0.5f;

        /// <summary>
        /// Thrust axis
        /// </summary>
        private string _vertical;

        /// <summary>
        /// Turn Axis
        /// </summary>
        private string _horizontal;

        /// <summary>
        /// Button to fire projectile
        /// </summary>
        private string _fireAxis;

        /// <summary>
        /// The bullet prefab
        /// </summary>
        public GameObject Bullet;

        /// <summary>
        /// Color to tint the projections fired by this player
        /// might be deprecated by this point, but I don't feel like messing with it
        /// </summary>
        public Color BulletColor;

        /// <summary>
        /// Time at which we will next be allowed to fire.
        /// </summary>
        private float _nextFire;

        /// <summary>
        /// Rigid body component for player.
        /// </summary>
        private Rigidbody2D _rb;

        /// <summary>
        /// Sprite renderer Component for this player.
        /// </summary>
        private SpriteRenderer _sprite;
        
        /// <summary>
        /// Initialize
        /// </summary>
        internal void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
            
            
            if (gameObject.CompareTag("Player1"))
            {
                _horizontal = "Horizontal1";
                _vertical = "Vertical1";
                _sprite.color = Color.red;
                BulletColor = Color.red;
            }
            else
            {
                _horizontal = "Horizontal2";
                _vertical = "Vertical2";
                _sprite.color = Color.blue;
                BulletColor = Color.blue;


            }
            
            if ((Platform.GetPlatform() == PlatformType.Mac) || (Platform.GetPlatform() == PlatformType.Linux))
            {
                if (gameObject.CompareTag("Player1"))
                {
                    _fireAxis = "FireAxis1(Mac)";
                }
                else
                {
                    _fireAxis = "FireAxis2(Mac)";
                }
                return;
            }
            if (Platform.GetPlatform() == PlatformType.Windows)
            {
                if (gameObject.CompareTag("Player1"))
                {
                    _fireAxis = "FireAxis1(Win)";
                }
                else
                {
                    _fireAxis = "FireAxis2(Win)";
                }
                Debug.Log(GameObject.FindGameObjectWithTag("Player1").gameObject.GetComponent<Player>()._fireAxis);
            }
        }
        
        internal void Update()
        {
            Turn(Input.GetAxis(_horizontal));
            Thrust(Input.GetAxis(_vertical));
            
            // Firing
            if (Input.GetAxis(_fireAxis) <= 0f) return;  
            if (Time.time > _nextFire) Fire();
        }
        
        /// <summary>
        /// Fire a bullet
        /// </summary>
        private void Fire()
        {
            var newBullet = Instantiate(Bullet);
            var bulletComponent = newBullet.GetComponent<Bullet>();
            var up = transform.up.normalized;
            bulletComponent.Init(gameObject, gameObject.transform.position + (up * 2f), up);
            _nextFire = Time.time + _cooldown;
        }
        

        private void Turn(float direction)
        {
            if (Mathf.Abs(direction) < 0.2f)
            {
                return;
            }
            _rb.AddTorque(direction * -0.2f);

        }

        private void Thrust(float intensity)
        {
            if (Mathf.Abs(intensity) < 0.2f)
            {
                return;
            }
            _rb.AddRelativeForce(Vector2.up * intensity * 2);
        }
    }
}

