using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject projectile;
    public float direction;
    public override IEnumerator Attack()
    {
        if (!isAttacking)
        {
            anim.Play("Fire", -1, 0f);
            isAttacking = true;
            Quaternion rotation = this.transform.rotation;
            GameObject bullet = Instantiate(projectile, new Vector3((this.transform.position.x)+(1* this.transform.localScale.x), this.transform.position.y, this.transform.position.z), rotation);
            bullet.GetComponent<Bullet>().direction = this.transform.localScale.x;
            bullet.GetComponent<Bullet>().player = this.GetComponentInParent<PlayerMovement>().gameObject;
            bullet.GetComponent<Bullet>().dmg = dmg;
            yield return new WaitForSeconds(attackTime);
            isAttacking = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
