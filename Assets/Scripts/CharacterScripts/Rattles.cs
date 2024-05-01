using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rattles : PlayerMovement
{
    //[SerializeField] private GameObject crown;
    //[SerializeField] private float crownSpeed = 50f;
    [SerializeField] private Vector3 crownThrowDist = new Vector3(10, 0, 0);
    [SerializeField] private CrownBoomerang crown;
    [SerializeField] private CrownBoomerang crownToThr;
    private float direction;
    public bool holdingBoomerang;
    private bool cooldownActive = false;
    private Vector3 targetPos;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        holdingBoomerang = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        direction = transform.localScale.x;
        if (holdingBoomerang && activeAbility)
        {
            Gamepad controller = Gamepad.all[controllerNum];
            controller.SetMotorSpeeds(0.3f, 1f);
            StartCoroutine(RumbleEnd(controller, 0.3f));
            active.color = Color.grey;
            crownToThr = Instantiate(crown, transform.position, Quaternion.identity);
            crownToThr.setRattles(this);

            targetPos = this.gameObject.transform.position + (crownThrowDist * direction);
            holdingBoomerang = false;

            crownToThr.Throw(targetPos, transform, direction);
        }

        if (!holdingBoomerang && !cooldownActive && crownToThr == null)
        {
            StartCoroutine(CrownCooldown());
        }
        base.Update();

    }

    public Vector3 getCrownThrowDist()
    {
        return crownThrowDist;
    }

    public IEnumerator CrownCooldown()
    {

        cooldownActive = true;
        yield return new WaitForSeconds(3.0f);
        holdingBoomerang = true;
        cooldownActive = false;
        active.color = Color.white;
    }

}
