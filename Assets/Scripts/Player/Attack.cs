using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void AttackSpriteFlip();
}

public class Attack: MonoBehaviour, IAttack {

    //Direction
    SpriteRenderer sprite;

    //Attack Triggers
    public GameObject attackLeft, attackRight;

    //Attack Settings
    public float attackCoolDown;

    /// <summary>
    /// Activates and Plays Attack animation and trigger in the correct direction.
    /// </summary>
    public void AttackSpriteFlip()
    {
        if (!sprite.flipX) //facing right
        {
            AttackTriggerEnable(AttackDirection.RIGHT);
        }
        else
        {
            AttackTriggerEnable(AttackDirection.LEFT);
        }
    }

    void AttackTriggerEnable(AttackDirection dir)
    {
        if(dir == AttackDirection.LEFT)
        {
            attackLeft.SetActive(true);
        }
        else
        {
            attackRight.SetActive(true);
        }

        //Activate cooldown
        StartCoroutine(AttackTriggerCooldown(attackCoolDown));
    }

    IEnumerator AttackTriggerCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        attackRight.SetActive(false);
        attackLeft.SetActive(false);
    }

    public enum AttackDirection
    {
        RIGHT,
        LEFT
    }

}
