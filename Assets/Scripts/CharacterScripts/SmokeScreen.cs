using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScreen : MonoBehaviour
{
    [SerializeField] private float pushForce = 30f;
    [SerializeField] private bool hit = false;
    [SerializeField] private Vector3 knockbackDist = new Vector3(5,0,0);
    [SerializeField] private GameObject phantom;
    [SerializeField] private bool shovingOpp;
    [SerializeField] private bool oppMovementDisabled = false;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 velocity;
    public Rattles inSmoke;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Mathf.Abs(inSmoke.gameObject.transform.position.x - this.gameObject.transform.position.x)<=3)
        {
            //if (inSmoke.transform.position.x > phantom.transform.position.x)
            // {
            if (!hit)
            {
                if(inSmoke.transform.position.x > phantom.transform.position.x)
                {
                    Knockback(inSmoke.gameObject.transform.position, 1.0f);
                }
                else
                {
                    Knockback(inSmoke.gameObject.transform.position, -1.0f);
                }
                SetHit(true);
                SetShovingOpp(true);
                Debug.Log("Set knockback values");
            }
                //inSmoke.GetComponent<Rigidbody2D>().AddForce(Vector3.right * pushForce, ForceMode2D.Impulse);
                //Debug.Log("Pushing");
                //SetPushForce(0.0f);
           // }
/*            else
            {
                inSmoke.GetComponent<Rigidbody2D>().AddForce(Vector3.left * pushForce, ForceMode2D.Impulse);
            }*/
        }

        if(shovingOpp)
        {
            inSmoke.gameObject.transform.position += velocity * Time.deltaTime;
            velocity = Vector3.Lerp(velocity, (targetPos - transform.position).normalized * pushForce, Time.deltaTime * 5f);
        }
    }

    public void SetHit(bool hit)
    {
        this.hit = hit;
    }

    public void SetShovingOpp(bool shovingOpp)
    {
        this.shovingOpp = shovingOpp;
    }

    public void Knockback(Vector3 target, float direction)
    {
        targetPos = target + (knockbackDist * direction);
        velocity = ((target + knockbackDist) - transform.position).normalized * pushForce * direction;
    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        if (rb != null && rb != phantom.GetComponent<Rigidbody2D>())
        {
            if (!oppMovementDisabled)
            {
                hitOpp = col.gameObject;

                if(hitOpp.transform.position.x > phantom.transform.position.x)
                {
                    rb.AddForce(Vector3.right * pushForce, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector3.left * pushForce, ForceMode2D.Impulse);
                }

                //StartCoroutine(RattlesDisableMove());
            }
        }
    }

    public IEnumerator RattlesDisableMove()
    {
        hitOpp.GetComponent<PlayerMovement>().enabled = false;
        oppMovementDisabled = true;
        yield return new WaitForSeconds(0.5f);
        hitOpp.GetComponent<PlayerMovement>().enabled = true;
        oppMovementDisabled = false;
    }*/
}
