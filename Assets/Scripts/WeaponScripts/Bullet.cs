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

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null && collision.gameObject != player)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            if (!collision.gameObject.GetComponent<PlayerMovement>().iFrames)
            {
                collision.gameObject.GetComponent<PlayerMovement>().hp -= dmg;
                StartCoroutine(collision.gameObject.GetComponent<PlayerMovement>().Damaged());
                if (!collision.gameObject.GetComponent<PlayerMovement>().iFrames)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else if (collision.gameObject.GetComponent<PlayerMovement>() == null)
        {
            Destroy(this.gameObject);
        }
    }
}
