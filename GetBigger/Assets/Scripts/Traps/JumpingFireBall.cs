using UnityEngine;

public class JumpingFireBall : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;

    private Vector3 startPos;
    private SpriteRenderer spriteRenderer;

    private bool flipped = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        float sinValue = Mathf.Sin(Time.time * moveSpeed);

        float newY = startPos.y + sinValue * moveDistance;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        if (sinValue > 0.99f && !flipped)
        {
            spriteRenderer.flipY = true;
            flipped = true;
        }
        else if (sinValue < -0.99f && flipped)
        {
            spriteRenderer.flipY = false;
            flipped = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
