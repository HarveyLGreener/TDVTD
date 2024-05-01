using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon
{
    public GameObject projectile;
    public float direction;
    public float initialAttackCooldown;
    public bool canAttack = true;

    public void Start()
    {
        initialAttackCooldown = attackTime;
        attackTime = 0f;
    }
    public override IEnumerator Attack()
    {
        if (canAttack)
        {
            Gamepad.current.SetMotorSpeeds(0.1f, 0.3f);
            StartCoroutine(RumbleEnd(Gamepad.current, 0.05f,true));
            anim.Play("Fire", -1, 0f);
            canAttack = false;
            isAttacking = true;
            Quaternion rotation = this.transform.rotation;
            GameObject bullet = Instantiate(projectile, new Vector3((this.transform.position.x), this.transform.position.y, this.transform.position.z), rotation);
            bullet.GetComponent<Bullet>().direction = this.transform.lossyScale.x;
            bullet.GetComponent<Bullet>().player = this.GetComponentInParent<PlayerMovement>().gameObject;
            bullet.GetComponent<Bullet>().dmg = dmg;

            if (bullet.GetComponent<SpriteRenderer>().enabled)
            {
                //Destroy(bullet, 2.0f);
            }
            attackTime = initialAttackCooldown;
            yield return new WaitForSeconds(0.01f);
            isAttacking = false;
        }
    }

    public void Update()
    {
        if (attackTime > 0f)
        {
            attackTime -= Time.deltaTime;
        }
        else
        {
            isAttacking = false;
            attackTime = initialAttackCooldown;
            canAttack = true;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
