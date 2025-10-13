using UnityEngine;

public class MeleeEnemy : Enemy, IDamageable
{
    float maxHealth = 10;
    float currentHealth = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Attack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("Ataque melee");
    }

    void IDamageable.TakeDamage()
    {
        Debug.Log ("enemigo recibiendo daño");
    }
}
