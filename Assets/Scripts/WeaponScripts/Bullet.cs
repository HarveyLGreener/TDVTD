using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public int dmg;
    public float direction;
    public bool destroy = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += transform.right * speed * Time.deltaTime * direction;
    }

    private void LateUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objectHit = collision.gameObject;
        if (objectHit.GetComponent<PlayerMovement>() != null && objectHit != player)
        {
            if (!objectHit.GetComponent<PlayerMovement>().iFrames && !objectHit.GetComponent<PlayerMovement>().isParrying && objectHit.GetComponent<PlayerMovement>().hp > 0)
            {
                destroy = false;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<Collider2D>().enabled = false;
                objectHit.GetComponent<PlayerMovement>().hp -= dmg;
                StartCoroutine(objectHit.GetComponent<PlayerMovement>().Damaged());
                StartCoroutine(waitToDestroy(objectHit));

            }
            else if (objectHit.GetComponent<PlayerMovement>().isParrying)
            {
                player = objectHit;
                player.GetComponent<PlayerMovement>().parriesUsed++;
                direction = direction * -1;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else if (objectHit.GetComponent<PlayerMovement>() == null && objectHit.GetComponent<Bullet>() == null)
        {
            if (destroy)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public IEnumerator waitToDestroy(GameObject collision)
    {
        yield return new WaitUntil(() => !collision.GetComponent<PlayerMovement>().iFrames);
        Destroy(this.gameObject);
    }

    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => (this.gameObject.GetComponent<Bullet>().destroy));
        Destroy(this.gameObject);
    }
}
