using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth { get; private set; }

    public Stats damage;
    public Stats atkSpd; 
    
    private void Awake() {
        curHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
}
