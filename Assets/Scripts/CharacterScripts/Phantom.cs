using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    [SerializeField] private GameObject smokeScreen;
    [SerializeField] private bool activeCountdown = false;
    [SerializeField] private float smokeScreenLength = 0f;
    public PlayerMovement phantom;

    // Update is called once per frame
    void Update()
    {
        if(!activeCountdown && phantom.activeAbility)
        {
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
        activeCountdown = false;
    }
}
