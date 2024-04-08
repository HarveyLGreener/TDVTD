using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    [SerializeField] private GameObject smokeScreen;
    [SerializeField] private bool activeCountdown = false;
    [SerializeField] private float smokeScreenLength = 0.75f;
    public PlayerMovement phantom;

    // Update is called once per frame
    void Update()
    {
        if(!activeCountdown && phantom.activeAbility)
        {
            smokeScreen.SetActive(true);
            if(!activeCountdown)
            {
                StartCoroutine(SmokeScreenActive());
            }
        }
    }

    public IEnumerator SmokeScreenActive()
    {
        activeCountdown = true;
        yield return new WaitForSeconds(smokeScreenLength);
        smokeScreen.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        activeCountdown = false;
    }
}
