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
            Destroy(this.gameObject.GetComponent<Collider2D>());
            //yield return new WaitForSeconds(attackTime);
            isAttacking = false;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (isAttacking && collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Hit");
            if (!collision.gameObject.GetComponent<PlayerMovement>().iFrames && !collision.gameObject.GetComponent<PlayerMovement>().isParrying && collision.gameObject.GetComponent<PlayerMovement>().hp>0)
            {
                collision.gameObject.GetComponent<PlayerMovement>().hp -= dmg;
                StartCoroutine(collision.gameObject.GetComponent<PlayerMovement>().Damaged());
            }
            else if (collision.gameObject.GetComponent<PlayerMovement>().isParrying)
            {
                Debug.Log("Parried!");
                StartCoroutine(parryStunned());
            }
        }
    }

    public IEnumerator parryStunned()
    {
        this.transform.parent.gameObject.GetComponent<PlayerMovement>().anim.Play("Stunned", 0);
        this.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        this.transform.parent.gameObject.GetComponent<GeneralWeapon>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        this.transform.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        this.transform.parent.gameObject.GetComponent<GeneralWeapon>().enabled = true;
    }

}
