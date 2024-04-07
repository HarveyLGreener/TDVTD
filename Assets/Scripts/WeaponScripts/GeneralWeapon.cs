using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneralWeapon : MonoBehaviour
{
    private Vector2 weaponAim;
    private float scaleFactor = 1f;
    public GameObject weaponAnchor;
    public bool isSword = true;
    private bool attacked = false;
    public GameObject[] weapons;
    public void OnAim(InputAction.CallbackContext context)
    {
        weaponAim = context.ReadValue<Vector2>();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = context.action.triggered;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = weaponAim.x;
        float inputY = weaponAim.y;
        if (!this.gameObject.GetComponent<PlayerMovement>().hit)
        {
            if (attacked)
            {
                foreach (GameObject weapon in weapons)
                {
                    if (isSword)
                    {
                        StartCoroutine(weapon.GetComponent<Sword>().Attack());
                    }
                    else
                    {
                        StartCoroutine(weapon.GetComponent<Gun>().Attack());
                    }
                }
            }
            else if (weaponAim != Vector2.zero && !weapons[0].GetComponent<Weapon>().isAttacking)
            {
                if (inputX > 0)
                {
                    scaleFactor = 1f;
                }
                else if (inputX < 0)
                {
                    scaleFactor = -1f;
                }
                if (isSword)
                {
                    this.gameObject.transform.localScale = new Vector3(scaleFactor, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    this.gameObject.transform.localScale = new Vector3(scaleFactor, transform.localScale.y, transform.localScale.z);
                    weaponAnchor.transform.localScale = new Vector3(scaleFactor, weaponAnchor.transform.localScale.y, weaponAnchor.transform.localScale.z);
                    weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, (inputY * 90f * scaleFactor));
                }
            }
        }
    }
    private void LateUpdate()
    {
        attacked = false;
    }
}
