using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject projectile;
    public Vector3 Target;
    public override IEnumerator Attack()
    {
        isAttacking = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Target.z = 0f;
        Quaternion rotation = Quaternion.LookRotation(Target - transform.position, transform.TransformDirection(Vector3.up));
        rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        GameObject bullet = Instantiate(projectile, this.transform.position, rotation);
        bullet.GetComponent<Bullet>().player= this.GetComponentInParent<PlayerMovement>().gameObject;
        bullet.GetComponent<Bullet>().dmg = dmg;
        if (Target.x < transform.position.x)
        {
            bullet.GetComponent<Bullet>().right = false;
        }
        Destroy(bullet, 3.0f);
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
