using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }

        if (collision.tag == "Projectile")
        {
            gameObject.SetActive(false);
        }
    }
}
