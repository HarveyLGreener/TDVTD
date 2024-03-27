using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int dmg = 1;
    [SerializeField] protected float attackTime = 1f;
    [SerializeField] protected bool isAttacking = false;

    protected virtual void OnEnable()
    {
        isAttacking = false;
        if (isAttacking == false)
        {
            StartCoroutine(Attack());
        }
    }

    private void LateUpdate()
    {
        if (!isAttacking)
        {
            this.gameObject.active = false;
        }
    }

    public virtual IEnumerator Attack()
    {
        isAttacking = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<PlayerMovement>().hp -= dmg;
            this.gameObject.active = false;
        }
    }

}
