using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{


    [Header("Player Health Things")]
    public float playerHealth = 1000f;
    public float presentHealth;





    // Start is called before the first frame update
    void Start()
    {
        presentHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        //Cursor.lockState = CursorLockMode.None;
     
    }


}
