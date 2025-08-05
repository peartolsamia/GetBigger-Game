using UnityEngine;


// this scripts goal is to use spawn projectile function outside the playerAttack script

public class AnimationEventRelay : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }


    public void SpawnProjectile()
    {
        if (playerAttack != null)
        {
            playerAttack.SpawnProjectile();
        }
        else
        {
            Debug.LogWarning("PlayerAttack baðlantýsý yok!");
        }
    }
}
