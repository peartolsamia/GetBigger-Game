using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //private Animator anim;
    [SerializeField] private Animator anim;

    private PlayerMovement playerMovement;

    [SerializeField] private float attackCooldown; // min amount of time to attack again

    private float cooldownTimer = Mathf.Infinity; // time past since last attack

    [SerializeField] private Transform firePoint; // point that projectiles that will generate

    [SerializeField] private GameObject[] projectiles; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer > attackCooldown && playerMovement.canAttack()) // attcak can only perform if left mouse clicked, attack cooldown time past and player is able to attack
        {
            Attack();
        }

        cooldownTimer = cooldownTimer + Time.deltaTime;
    }




    private void Attack()
    {
        anim.SetTrigger("attack");

        cooldownTimer = 0;
    }


    public void SpawnProjectile()
    {
        projectiles[FindProjectile()].transform.position = firePoint.position;// everytime we attack, we take one of our projectile object and reset it to fire point
        projectiles[FindProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));// set direction of the projectile to player object's direction
    }

    private int FindProjectile() // looks if prjectile is active in the hierarchy, if active returns zero, if not returns its number
    {
        for (int i = 0; i < projectiles.Length;  i++)
        {
            if(!projectiles[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

}
