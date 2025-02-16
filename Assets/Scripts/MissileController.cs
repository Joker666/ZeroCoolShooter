using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float initialSpeed = 2.0f;
    public float maxSpeed = 25.0f;
    public float accelerationRate = 20.0f;
    private float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase speed over time
        currentSpeed = Mathf.Min(currentSpeed + accelerationRate * Time.deltaTime, maxSpeed);

        // Move missile forward
        transform.Translate(currentSpeed * Time.deltaTime * Vector3.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy missile when it collides with anything
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.InstantiateExplosion(transform, 0.3f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
