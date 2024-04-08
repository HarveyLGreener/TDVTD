using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public int dmg;
    public float direction;
    public bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += transform.right * speed * Time.deltaTime * direction;
    }

    private void LateUpdate()
    {
        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objectHit = collision.gameObject;
        if (objectHit.GetComponent<PlayerMovement>() != null && objectHit != player)
        {
            if (!objectHit.GetComponent<PlayerMovement>().iFrames && !objectHit.GetComponent<PlayerMovement>().isParrying)
            {
                objectHit.GetComponent<PlayerMovement>().hp -= dmg;
                StartCoroutine(objectHit.GetComponent<PlayerMovement>().Damaged());
                StartCoroutine(waitToDestroy(objectHit));

            }
            else if (objectHit.GetComponent<PlayerMovement>().isParrying)
            {
                player = objectHit;
                direction = direction * -1;
            }
        }
        else if (objectHit.GetComponent<PlayerMovement>() == null && objectHit.GetComponent<Bullet>() == null)
        {
            destroy = true;
        }
    }

    public IEnumerator waitToDestroy(GameObject collision)
    {
        yield return new WaitUntil(() => !collision.GetComponent<PlayerMovement>().iFrames);
        destroy = true;
    }
}
