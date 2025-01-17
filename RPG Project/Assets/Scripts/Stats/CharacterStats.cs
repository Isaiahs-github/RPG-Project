using UnityEngine;

// Base class that player and enemies can derive from to include stats. 

public class CharacterStats : MonoBehaviour{
    
    // Health 
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public event System.Action<int, int> OnHealthChanged; 

    // Set Current health to max health
    // when starting the game.
    void Awake(){
        currentHealth = maxHealth;
    }
    

    // Test damage character to test 
    // armor modifiers work properly.
    // void Update(){
    //     if(Input.GetKeyDown(KeyCode.T)){
    //         TakeDamage(10);
    //     }
    // }

    // Damage the character
    public void TakeDamage(int damage){

        // Subtract the armor value
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Damage the character
        currentHealth -= damage; 
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null){
            OnHealthChanged(maxHealth, currentHealth);
        }

        // If health reaches zero
        if (currentHealth <= 0){
            Die();
        }
    }

    public virtual void Die(){
        //  Die in some way
        //  This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }




}
