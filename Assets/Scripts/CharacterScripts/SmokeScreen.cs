using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScreen : MonoBehaviour
{
    [SerializeField] private float pushForce = 3f;
    [SerializeField] private GameObject phantom;
    [SerializeField] private GameObject hitOpp;
    [SerializeField] private bool oppMovementDisabled = false;
    public Rattles inSmoke;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Mathf.Abs(inSmoke.gameObject.transform.position.x - this.gameObject.transform.position.x)<=3)
        {

            if (inSmoke.transform.position.x > phantom.transform.position.x)
            {
                inSmoke.GetComponent<Rigidbody2D>().AddForce(Vector3.right * pushForce, ForceMode2D.Impulse);
            }
            else
            {
                inSmoke.GetComponent<Rigidbody2D>().AddForce(Vector3.left * pushForce, ForceMode2D.Impulse);
            }
        }
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
