using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : PlayerMovement
{
    [SerializeField] private GameObject smokeScreen;
    [SerializeField] private bool activeCountdown = false;
    [SerializeField] private float smokeScreenLength = 0f;
    public PlayerMovement phantom;
    public SpriteRenderer activeRender;

    // Update is called once per frame
    void Update()
    {
        if(!activeCountdown && phantom.activeAbility)
        {
            activeRender.color = Color.grey;
            CircleCollider2D colliderOfWeapon = smokeScreen.AddComponent<CircleCollider2D>();
            colliderOfWeapon.radius = 1f;
            colliderOfWeapon.isTrigger = true;
            colliderOfWeapon.enabled = false;
            smokeScreen.SetActive(true);
            StartCoroutine(SmokeScreenActive(colliderOfWeapon));
        }
    }

    public IEnumerator SmokeScreenActive(Collider2D collider)
    {
        activeCountdown = true;
        collider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        collider.enabled = false;
        smokeScreen.SetActive(false);
        Destroy(collider);
        yield return new WaitForSeconds(3.0f);
        activeRender.color = Color.white;
        activeCountdown = false;
    }

    public override IEnumerator Dash()
    {
        dashOnCooldown = true;
        playerDashSprite.color = Color.red;
        float inputX = inputMovement.x;
        float inputY = inputMovement.y;
        if (inputX > 0)
        {
            transform.position = new Vector3(horizontalDash * inputX, rb.velocity.y, 0);
            //transform.position = new Vector2(horizontalDash * inputX, )
        }
        else if (inputX < 0)
        {
            //rb.velocity = new Vector2(horizontalDash * -1, rb.velocity.y);
        }
        if (inputY > 0)
        {
            transform.position = new Vector3(rb.velocity.x, verticalDash * 500, 0);
            //rb.velocity = new Vector2(rb.velocity.x, verticalDash);
        }
        else if (inputY < 0)
        {
            //rb.velocity = new Vector2(rb.velocity.x, verticalDash * -1);
        }
        canDash = false;
        yield return new WaitForSeconds(0.75f);
        dashOnCooldown = false;
    }
}
