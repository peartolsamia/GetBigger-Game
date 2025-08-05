using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]

    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } // public to get method but private to set

    private Animator anim;

    private bool dead;


    [Header("iFrames")]

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;

    private SpriteRenderer spriteRend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponentInChildren<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

    }


    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // adding boundaries to health bar

        if (currentHealth > 0) // the case player is not dead
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else // player is dead
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false; // disable players control
                dead = true;
            }
        }
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability() // player will take no damage when invulnerable
    {
        Physics2D.IgnoreLayerCollision(10, 11, true); 

        for (int i = 0; i < numOfFlashes; i++) // flashing red and becomes a little trasperent while invulnerable
        {
            spriteRend.color = new Color(1, 0, 0, 0.8f); 
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

}
