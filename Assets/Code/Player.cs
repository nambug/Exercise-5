using UnityEngine;

namespace Code
{
    /// <summary>
    /// Implements player control of tanks, as well as collision detection.
    /// </summary>
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// Delay between shooting
        /// </summary>
        private float _cooldown = 0.5f;

        /// <summary>
        /// Axis for controlling driving
        /// </summary>
        private string _vertical;

        /// <summary>
        /// Axis for controlling rotation
        /// </summary>
        private string _horizontal;

        /// <summary>
        /// Button to fire projectile
        /// </summary>
        private string _fireAxis;

        /// <summary>
        /// Prefab for the bullets we fire.
        /// </summary>
        public GameObject Bullet;

        /// <summary>
        /// Color to tint the projections fired by this tank
        /// </summary>
        public Color ProjectileColor = Color.white;

        /// <summary>
        /// Time at which we will next be allowed to fire.
        /// </summary>
        private float _nextFire;

        /// <summary>
        /// Rigid body component for tank.
        /// </summary>
        private Rigidbody2D _rb;


        /// <summary>
        /// Initialize
        /// </summary>
        internal void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (gameObject.CompareTag("Player1"))
            {
                _horizontal = "Horizontal1";
                _vertical = "Vertical1";
            }
            else
            {
                _horizontal = "Horizontal2";
                _vertical = "Vertical2";
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
                return;
            }
        }
        
        internal void Update()
        {
            Turn(Input.GetAxis(_horizontal));
            Thrust(Input.GetAxis(_vertical));
            
            // Firing
            if (Input.GetAxis(_fireAxis) > 0f)
            {
                FireProjectileIfPossible();   
            }

            
        }
        
        /// <summary>
        /// The player pushed fire.
        /// Check to see if we can fire projectile
        /// </summary>
        void FireProjectileIfPossible()
        {
            if (Time.time > _nextFire)
            {
                FireProjectile();
                _nextFire = Time.time + _cooldown;
            }
        }

        /// <summary>
        /// Actually fire.
        /// </summary>
        void FireProjectile()
        {
            var go = Instantiate(Bullet);
            var ps = go.GetComponent<Bullet>();
            var up = transform.up.normalized;
            ps.Init(gameObject, transform.position + up * 2f, up);
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

