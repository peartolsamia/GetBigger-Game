using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    [SerializeField] private float speed; // speed of the projectile
    private bool hit; // did projectile hit something
    private float direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit) // if hit, return and not execute rest of the code
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * direction;

        transform.Translate(movementSpeed, 0, 0); // move projectile by the x axis


        lifeTime += Time.deltaTime;
        if (lifeTime > 5)
        {
            gameObject.SetActive(false); // deactivate projectile if it doesnt hit anything for 5 secs
        }
    }



    private void OnTriggerEnter2D(Collider2D collision) // on hit, set the related variables and trigger hit animation
    {
        if (collision.CompareTag("Exit")) return; // dont count as a hit if its exit object
        if (collision.CompareTag("Health Collectible")) return;

        hit = true; 
        boxCollider.enabled = false;
        anim.SetTrigger("hit");
    }

    public void SetDirection(float _direction)
    {
        lifeTime = 0;

        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) // flip direction if its false
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
