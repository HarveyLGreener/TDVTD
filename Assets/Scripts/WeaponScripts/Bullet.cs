using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool right = true;
    public GameObject player;
    public int dmg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.localPosition -= transform.right * speed * Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null && collision.gameObject != player)
        {
            collision.gameObject.GetComponent<PlayerMovement>().hp-= dmg;
        }
        if (collision.gameObject != player)
        {
            Destroy(this.gameObject);
        }
    }
}
