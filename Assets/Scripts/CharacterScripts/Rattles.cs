using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rattles : PlayerMovement
{
    //[SerializeField] private GameObject crown;
    [SerializeField] private float crownSpeed = 50f;
    [SerializeField] private Vector3 crownThrowDist = new Vector3(5, 0, 0);
    [SerializeField] private CrownBoomerang crown;
    private bool holdingBoomerang;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        holdingBoomerang = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingBoomerang && Input.GetKeyDown(KeyCode.C))
        {
            targetPos = crown.gameObject.transform.position + crownThrowDist;
            holdingBoomerang = false;

            crown.Throw(targetPos, transform);
        }
    }

}
