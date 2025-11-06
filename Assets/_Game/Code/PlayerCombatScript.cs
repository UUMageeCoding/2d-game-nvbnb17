using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombatScript : MonoBehaviour
{

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

    }
    
    void Attack()
    {
        //Play an attack animation
        animator.SetTrigger("Attack");
        //detect enemies in range of attack
        //damage enemies
    }
}
