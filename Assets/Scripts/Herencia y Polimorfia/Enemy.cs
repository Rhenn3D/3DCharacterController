using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 3;
    public float attackDamage = 10;
    public float health = 20;


    public void Movement()
    {
        Debug.Log("Ze Mueve!");
    }

    public virtual void Attack()
    {
        Debug.Log("Ataca");
    }
}
