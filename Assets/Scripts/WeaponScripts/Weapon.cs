using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public int dmg = 1;
    [SerializeField] protected float attackTime = 1f;
    [SerializeField] public bool isAttacking = false;
    public Animator anim;
    public GameObject hitParticle;
    public Transform particleSpawnPoint;
    public bool rumbleChanged = false;

    protected virtual void OnEnable()
    {
    }

    private void LateUpdate()
    {
    }

    public virtual IEnumerator Attack()
    {
        Gamepad.current.SetMotorSpeeds(0.1f, 0.3f);
        StartCoroutine(RumbleEnd(Gamepad.current, 0.05f,false));
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
                Gamepad.current.SetMotorSpeeds(0.1f, 0.3f);
                StartCoroutine(RumbleEnd(Gamepad.current, 0.05f,false));
                collision.gameObject.GetComponent<PlayerMovement>().hp -= dmg;
                collision.gameObject.GetComponent<PlayerMovement>().Damaged();
                GameObject particle = Instantiate(hitParticle);
                particle.transform.position = particleSpawnPoint.position;
                Destroy(particle, 0.33f);

            }
            else if (collision.gameObject.GetComponent<PlayerMovement>().isParrying)
            {
                Debug.Log("Parried!");
                StartCoroutine(parryStunned()); 
                GameObject particle = Instantiate(hitParticle);
                particle.transform.position = particleSpawnPoint.position;
                Destroy(particle, 0.33f);
            }
            else
            {
                Gamepad.current.SetMotorSpeeds(0.1f, 0.3f);
                StartCoroutine(RumbleEnd(Gamepad.current, 0.05f, false));
            }
        }
    }

    public IEnumerator parryStunned()
    {
        Debug.Log("Started Coroutine");
        Debug.Log(this.transform.parent.parent.gameObject.name);
        this.transform.parent.parent.gameObject.GetComponent<PlayerMovement>().anim.Play("Stunned", 0);
        Gamepad controller = Gamepad.all[this.transform.parent.parent.gameObject.GetComponent<PlayerMovement>().controllerNum];
        controller.SetMotorSpeeds(0.3f, 1f);
        rumbleChanged = true;
        StartCoroutine(RumbleEnd(controller, 1f,true));
        Debug.Log("Found parent and anim");
        this.transform.parent.parent.gameObject.GetComponent<PlayerMovement>().enabled = false;
        this.transform.parent.parent.gameObject.GetComponent<GeneralWeapon>().enabled = false;
        Debug.Log("Disabled Stuff");
        yield return new WaitForSeconds(1.0f);
        this.transform.parent.parent.gameObject.GetComponent<PlayerMovement>().enabled = true;
        this.transform.parent.parent.gameObject.GetComponent<GeneralWeapon>().enabled = true;
        Debug.Log("Made it to end");
    }

    public IEnumerator parryStunned(GameObject parent)
    {
        Debug.Log("Started Coroutine");
        Debug.Log(this.transform.parent.parent.gameObject.name);
        parent.GetComponent<PlayerMovement>().anim.Play("Stunned", 0);
        Gamepad controller = Gamepad.all[this.transform.parent.parent.gameObject.GetComponent<PlayerMovement>().controllerNum];
        controller.SetMotorSpeeds(0.3f, 1f);
        StartCoroutine(RumbleEnd(controller, 1f, true));
        Debug.Log("Found parent and anim");
        parent.GetComponent<PlayerMovement>().enabled = false;
        parent.GetComponent<GeneralWeapon>().enabled = false;
        Debug.Log("Disabled Stuff");
        yield return new WaitForSeconds(1.0f);
        parent.GetComponent<PlayerMovement>().enabled = true;
        parent.GetComponent<GeneralWeapon>().enabled = true;
        Debug.Log("Made it to end");
    }

    protected IEnumerator RumbleEnd(Gamepad controller, float time, bool rumbleOverride)
    {
        yield return new WaitForSeconds(time);
        if (!rumbleChanged || rumbleOverride)
        {
            controller.SetMotorSpeeds(0f, 0f);
        }
        rumbleChanged = false;
    }
}
