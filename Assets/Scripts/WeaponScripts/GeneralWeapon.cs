using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneralWeapon : MonoBehaviour
{
    private Vector2 weaponAim;
    private float scaleFactor = 1f;
    public GameObject weaponAnchor;
    public void OnAim(InputAction.CallbackContext context)
    {
        weaponAim = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = weaponAim.x;
        float inputY = weaponAim.y;
        if (weaponAim != Vector2.zero)
        {
            if (inputX > 0)
            {
                scaleFactor = 1f;
            }
            else if (inputX < 0)
            {
                scaleFactor = -1f;
            }
            this.gameObject.transform.localScale = new Vector3(scaleFactor, transform.localScale.y, transform.localScale.z);
            weaponAnchor.transform.localScale = new Vector3(scaleFactor, weaponAnchor.transform.localScale.y, weaponAnchor.transform.localScale.z);
            weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, (inputY * 90f * scaleFactor));
        }
    }
}
