using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownBoomerang : Weapon
{
    [SerializeField] private float speed;

    //private Transform spriteTransform;
    public Vector3 targetPos;
    private Vector3 velocity;
    private bool thrown;
    public bool hit;
    public GameObject oppHit;
    public bool isDamagingOpp;
    private float direction;
    private Transform returnTarg;
    [SerializeField] private Rattles rattles;

    private void Start()
    {
        //spriteTransform = transform.GetChild(0);
    }

    private void Update()
    {
        if (thrown)
        {
            if (!hit)
            {
                transform.position += velocity * Time.deltaTime;
                velocity = Vector3.Lerp(velocity, (targetPos - transform.position).normalized * speed, Time.deltaTime * 5f);
                if (Vector2.Distance(transform.position, targetPos) < 0.5f)
                {
                    hit = true;
                }
            }
            else
            {
                transform.position += velocity * Time.deltaTime;
                velocity = Vector3.Lerp(velocity, (returnTarg.position - transform.position).normalized * speed, Time.deltaTime * 5f);
            }
            //spriteTransform.Rotate(new Vector3(0, 0, Time.deltaTime * 100f));
        }
    }

    public void setRattles(Rattles rattles)
    {
        this.rattles = rattles;
    }

    public void Throw(Vector3 target, Transform newReturnTarg, float direction)
    {
        this.direction = direction;
        targetPos = target;
        velocity = ((target + new Vector3(5, 0, 0)) - transform.position).normalized * speed * direction;
        returnTarg = newReturnTarg;

        thrown = true;
        hit = false;
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject != rattles.gameObject)
        {
            base.OnTriggerEnter2D(col);

            if (col.GetComponent<PlayerMovement>().iFrames)
            {
                oppHit = col.gameObject;
                isDamagingOpp = true;
                /*this.GetComponent<BoxCollider2D>().isTrigger = false;
                StartCoroutine(WaitForDamagedCoroutine);
                this.GetComponent<BoxCollider2D>().isTrigger = true;*/
            }
        }
/*        else if(hit)
        {
            if(isDamagingOpp)
            {
                StartCoroutine(WaitForDamagedCoroutine());
            }
            else
            {
                rattles.holdingBoomerang = true;
                Destroy(this.gameObject);
            }
        }*/
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(hit && col.gameObject == rattles.gameObject)
        {
            if (isDamagingOpp)
            {
                StartCoroutine(WaitForDamagedCoroutine());
            }
            else
            {
                rattles.holdingBoomerang = true;
                Destroy(this.gameObject);
            }
        }
    }

    public IEnumerator WaitForDamagedCoroutine()
    {
        if(oppHit != null)
        {
            yield return new WaitUntil(() => !oppHit.GetComponent<PlayerMovement>().iFrames);
        }
        isDamagingOpp = false;
    }
}
