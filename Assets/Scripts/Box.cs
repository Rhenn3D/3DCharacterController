using UnityEngine;

public class Box : MonoBehaviour, IGrabbeable, IDamageable
{

    [SerializeField] private float _health;
    public void Grab()
    {
        Debug.Log("Vivan las Cojidas");
    }

    public void TakeDamage(float damage)
    {

        _health -= damage;
        
        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
