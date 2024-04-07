using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int dmg = 1;
    [SerializeField] protected float attackTime = 1f;
    [SerializeField] public bool isAttacking = false;
    public Animator anim;

    protected virtual void OnEnable()
    {
    }

    private void LateUpdate()
    {
    }

    public virtual IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            this.GetComponent<Collider2D>().enabled = true;
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.33f);
            this.GetComponent<Collider2D>().enabled = false;
            yield return new WaitForSeconds(attackTime);
            isAttacking = false;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<PlayerMovement>().hp -= dmg;
        }
    }

}
