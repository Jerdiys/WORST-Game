using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // Health of the enemy
    private Animator animator; // Animator component for animations
    public GameObject machete; // Reference to the machete object
    void Start()
    {
        // Initialize any necessary components or variables
        if (animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        else if (other.CompareTag("Machete"))
        {
            // Machete has hit the enemy
            Debug.Log("Enemy hit by machete!");
            TakeDamage(80); // Call TakeDamage with a damage value, e.g., 20
        }   
    }

    void TakeDamage(int damage)
    {
        health -= damage; // Reduce health by 20 (or any damage value)
        Debug.Log("Enemy took damage: " + damage + ", Remaining health: " + health);
    }
}
