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
    public BoxCollider2D colliderOfWeapon;
    public Vector2 playerDirection;
    public float playerInitialDirection;
    public float playerNewDirection;
    public void OnAim(InputAction.CallbackContext context)
    {
        weaponAim = context.ReadValue<Vector2>(); 
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = context.action.triggered;
        Debug.Log(attacked);
    }

    public void MoveDirection(InputAction.CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>();
    }
    private void Start()
    {
        playerInitialDirection = this.gameObject.transform.localScale.x;
    }
    // Update is called once per frame
    void Update()
    {
        playerNewDirection = this.gameObject.transform.localScale.x;
        float inputX = weaponAim.x;
        float inputY = weaponAim.y;
        if (!this.gameObject.GetComponent<PlayerMovement>().hit)
        {
            if (attacked)
            {
                attacked = false;
                foreach (GameObject weapon in weapons)
                {
                    if (isSword)
                    {
                        if (!weapon.GetComponent<Sword>().isAttacking)
                        {
                            colliderOfWeapon = weapon.AddComponent<BoxCollider2D>();
                            colliderOfWeapon.offset = new Vector2(0.05f, 0.2f);
                            colliderOfWeapon.size = new Vector2(2.25f, 5.5f);
                            colliderOfWeapon.isTrigger = true;
                            colliderOfWeapon.enabled = false;
                            StartCoroutine(weapon.GetComponent<Sword>().Attack());
                        }
                    }
                    else
                    {
                        StartCoroutine(weapon.GetComponent<Gun>().Attack());
                    }
                }
            }
            else if (weaponAim != Vector2.zero && !weapons[0].GetComponent<Weapon>().isAttacking && playerDirection == Vector2.zero)
            {
                if (inputX > 0)
                {
                    scaleFactor = 1f;
                }
                else if (inputX < 0)
                {
                    scaleFactor = -1f;
                }
                //if (isSword)
                //{
                    if (weaponAnchor.transform.lossyScale.x != this.gameObject.transform.lossyScale.x)
                    {
                        weaponAnchor.transform.parent = null;
                        this.gameObject.transform.localScale = new Vector3(weaponAnchor.transform.lossyScale.x, 1f, 1f);
                        weaponAnchor.transform.parent = this.gameObject.transform;
                    }
                    this.gameObject.transform.localScale = new Vector3(scaleFactor, transform.localScale.y, transform.localScale.z);
                    //weaponAnchor.transform.localScale = new Vector3(scaleFactor, weaponAnchor.transform.localScale.y, weaponAnchor.transform.localScale.z);
                    weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, (inputY * 90f * scaleFactor));
                //}
                /*else
                {
                    this.gameObject.transform.localScale = new Vector3(scaleFactor, transform.localScale.y, transform.localScale.z);
                    weaponAnchor.transform.localScale = new Vector3(scaleFactor, weaponAnchor.transform.localScale.y, weaponAnchor.transform.localScale.z);
                    weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, (inputY * 90f*scaleFactor));
                }*/
            }
            else if (weaponAim != Vector2.zero && !weapons[0].GetComponent<Weapon>().isAttacking && playerDirection != Vector2.zero)
            {
                if (inputX > 0)
                {
                    scaleFactor = 1f;
                }
                else if (inputX < 0)
                {
                    scaleFactor = -1f;
                }
                //if (isSword)
                //{
                this.gameObject.transform.localScale = new Vector3(scaleFactor, 1f, 1f);
                    if (weaponAnchor.transform.lossyScale.x != scaleFactor)
                    {
                        weaponAnchor.transform.parent = null;
                        weaponAnchor.transform.localScale = new Vector3(scaleFactor, weaponAnchor.transform.localScale.y, weaponAnchor.transform.localScale.z);
                        weaponAnchor.transform.parent = this.gameObject.transform;
                    }
                    weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, (inputY * 90f * scaleFactor));
                //}
            }
            else if (playerDirection != Vector2.zero && weaponAim == Vector2.zero)
            {
                weaponAnchor.transform.localScale = new Vector3(1f, 1f, 1f);
                weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
        playerInitialDirection = playerNewDirection;
    }
    private void LateUpdate()
    {
    }
}
