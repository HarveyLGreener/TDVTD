using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownBoomerang : MonoBehaviour
{
    [SerializeField] private float speed;

    //private Transform spriteTransform;
    private Vector3 targetPos;
    private Vector3 velocity;
    private bool thrown;
    private bool hit;
    private Transform returnTarg;

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

    public void Throw(Vector3 target, Transform newReturnTarg)
    {
        transform.SetParent(null);

        targetPos = target;
        velocity = ((target + new Vector3(0, 5, 0)) - transform.position).normalized * speed;
        returnTarg = newReturnTarg;

        thrown = true;
        hit = false;
    }
}
