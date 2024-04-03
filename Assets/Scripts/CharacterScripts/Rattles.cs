using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rattles : PlayerMovement
{
    [SerializeField] private GameObject crown;
    [SerializeField] private Rigidbody2D crownRB;
    [SerializeField] private bool crownThrowActive;
    [SerializeField] private float crownSpeed = 500f;
    
    // Start is called before the first frame update
    void Start()
    {
        crownThrowActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(!crownThrowActive)
            {
                if(transform.localScale.x > 0)
                {
                    crownRB.AddForce(new Vector2(crownSpeed, 0f));
                }
                crownThrowActive = true;
            }
        }
    }
}
