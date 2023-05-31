using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float bulletSpeed = 500f;
    public float maxLifeTime = 5f;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void Project(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * this.bulletSpeed);
        Destroy(this.gameObject, this.maxLifeTime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
