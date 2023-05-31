using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _thrusting;
    private float _turnDirection;
    public float thrustSpeed = 1f;
    public float turnSpeed = 1f;
    public Bullet bulletPrefab;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W);

        if (Input.GetKey(KeyCode.A))
        {
            _turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _turnDirection = -1f;
        }
        else
        {
            _turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        ThrustingMovement();
        TurnMovement();
    }
    private void ThrustingMovement()
    {
        if (_thrusting)
        {
            _rigidbody2D.AddForce(this.transform.up * this.thrustSpeed);
        }
    }

    private void TurnMovement()
    {
        if (_turnDirection != 0)
        {
            _rigidbody2D.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            GameManager.instance.PlayerDied();
        }
    }
}
