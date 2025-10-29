using UnityEngine;

public class Barril : MonoBehaviour, IDamageable, IInteratable
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IDamageable.TakeDamage(float damage)
    {
        Debug.Log ("enemigo recibiendo daño");
    }
    void IInteratable.Interact()
    {
        Debug.Log("Comiendo pisha");
    }
}

