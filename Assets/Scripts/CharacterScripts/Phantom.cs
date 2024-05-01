using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Phantom : MonoBehaviour
{
    [SerializeField] private GameObject smokeScreen;
    [SerializeField] private bool activeCountdown = false;
    [SerializeField] private float smokeScreenLength = 0f;
    public PlayerMovement phantom;
    public SpriteRenderer activeRender;
    public AudioSource audioSource;
    public AudioClip active;

    // Update is called once per frame
    void Update()
    {
        if(!activeCountdown && phantom.activeAbility)
        {
            audioSource.PlayOneShot(active);
            Gamepad controller = Gamepad.current;
            controller.SetMotorSpeeds(0.3f, 1f);
            StartCoroutine(RumbleEnd(controller, 0.3f));
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
        smokeScreen.GetComponent<SmokeScreen>().SetHit(false);
        smokeScreen.GetComponent<SmokeScreen>().SetShovingOpp(false);
        activeCountdown = false;
    }

    public IEnumerator RumbleEnd(Gamepad controller, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        controller.SetMotorSpeeds(0f, 0f);
    }
}
