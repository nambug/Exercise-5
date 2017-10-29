using UnityEngine;

namespace Code
{
        /// <summary>
    /// Implements player control of tanks, as well as collision detection.
    /// </summary>
    public class TankControl : MonoBehaviour {
        /// <summary>
        /// How fast to drive
        /// </summary>
        public float ForwardSpeed = 20f;
        
        /// <summary>
        /// Gain for velocity control.
        /// Force is this times the difference between our target velocity and our actual velocity
        /// </summary>
        public float Acceleration = 5f;
    
        /// <summary>
        /// How fast to turn
        /// </summary>
        public float RotationSpeed = 200f;
        
        /// <summary>
        /// Delay between shooting
        /// </summary>
        public float FireCooldown = 0.5f;
    
        /// <summary>
        /// Axis for controlling driving
        /// </summary>
        public string VerticalAxis;
        /// <summary>
        /// Axis for controlling rotation
        /// </summary>
        public string HorizontalAxis;
        /// <summary>
        /// Button to fire projectile
        /// </summary>
        public string FireAxis;
    
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
        private float coolDownTimer;
    
        /// <summary>
        /// Rigid body component for tank.
        /// </summary>
        private Rigidbody2D tankRb;
    
    
        /// <summary>
        /// Initialize
        /// </summary>
        internal void Start() {
            tankRb = GetComponent<Rigidbody2D>();
        }
    
        /// <summary>
        /// Joystick values less than this will be treated as zero
        /// </summary>
        const float DeadZoneSize = 0.1f;
    
        float DeadZone(float axis)
        {
            if (Mathf.Abs(axis) < DeadZoneSize)
                return 0;
            return axis;
        }
        /// <summary>
        /// The player pushed fire.
        /// Launch if we aren't cooling down.
        /// </summary>
        void FireProjectileIfPossible(){
            if (Time.time > coolDownTimer) {
                FireProjectile();
                coolDownTimer = Time.time + FireCooldown;
            }
        }
    
        /// <summary>
        /// Really and truly fire the projectile.
        /// </summary>
        void FireProjectile() {
            var go = Instantiate(Bullet) ;
            var ps = go.GetComponent<Bullet>();
            var up = transform.up.normalized;
            ps.Init(gameObject, transform.position + up * 2f, up);
        }

        internal void Update()
        {
            // Movement
            float inputVal = DeadZone(Input.GetAxis(VerticalAxis));
            
            var fForward = Acceleration * (inputVal * ForwardSpeed - Vector3.Dot(tankRb.velocity, transform.up));
            tankRb.AddForce(fForward * transform.up);
    
            float rotInputVal = DeadZone(Input.GetAxis(HorizontalAxis));
            tankRb.rotation += RotationSpeed * rotInputVal * Time.deltaTime;
    
            var fSkid = Vector3.Dot(tankRb.velocity, transform.right) * -1;
            tankRb.AddForce(fSkid * transform.right);
    
            // Firing
            if (Input.GetAxis(FireAxis) == 1f)
            {
                FireProjectileIfPossible();
            }
        }
    }
}

