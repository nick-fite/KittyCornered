using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int health = 10;
    public void AddHealth(int healthToAdd)
    {
        health += healthToAdd;
        if(health <= 0) {
            Death();
        }
    }

    virtual public void Death(){}
}
