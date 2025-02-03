using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public Transform muzzleSpawnPosition;
    public float destroyTime = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xPos, yPos, 0);
        movement = movement.normalized * speed * Time.deltaTime;
        transform.Translate(movement);

        // Clamp position using GameManager's boundary values
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -5.77f, 5.77f);
        // You can also set custom Y boundaries, for example -5 to 5
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.44f, 4.44f);
        transform.position = clampedPosition;
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject gm = Instantiate(missile, missileSpawnPosition);
            gm.transform.SetParent(null);
            Destroy(gm, destroyTime);

            GameObject muzzle = Instantiate(GameManager.instance.muzzleFlash, muzzleSpawnPosition);
            muzzle.transform.SetParent(null);
            Destroy(muzzle, destroyTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.InstantiateExplosion(transform, 1.0f);
            Destroy(collision.gameObject);
            Destroy(gameObject);

            GameManager.instance.GameOver();
        }
    }
}
