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
    public Vector2 attackDirection = new Vector2(0f, 0f);
    public float cooldown = 0f;
    public void OnAim(InputAction.CallbackContext context)
    {
        weaponAim = context.ReadValue<Vector2>();

    }

   
    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = context.action.triggered;
    }

    public void OnAttackDown(InputAction.CallbackContext context)
    {
        if (cooldown <= 0f)
        {
            //attacked = context.action.triggered;
            attackDirection = new Vector2(0f, -1f);
            if (attackDirection == new Vector2(0f, -1f))
            {
                attacked = context.action.triggered;
            }
        }
    }

    public void OnAttackUp(InputAction.CallbackContext context)
    {
        if (cooldown<= 0f)
        {
            attacked = context.action.triggered;
            attackDirection = new Vector2(0f, 1f);
        }

    }

    public void OnAttackRight(InputAction.CallbackContext context)
    {
        if (cooldown <= 0f)
        {
            attacked = context.action.triggered;
            attackDirection = new Vector2(1f, 0f);
        }
    }

    public void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (cooldown <= 0f)
        {
            attacked = context.action.triggered;
            attackDirection = new Vector2(-1f, 0f);
        }
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
        cooldown -= Time.deltaTime;
        playerNewDirection = this.gameObject.transform.localScale.x;
        float inputX = weaponAim.x;
        float inputY = weaponAim.y;
        if (!this.gameObject.GetComponent<PlayerMovement>().hit)
        {
            if (attackDirection != Vector2.zero)
            {
                if (!weapons[0].GetComponent<Weapon>().isAttacking && playerDirection == Vector2.zero)
                {
                    if (attackDirection.x > 0)
                    {
                        scaleFactor = 1f;
                    }
                    else if (attackDirection.x < 0)
                    {
                        scaleFactor = -1f;
                    }
                    if (weaponAnchor.transform.lossyScale.x != this.gameObject.transform.lossyScale.x)
                    {
                        weaponAnchor.transform.parent = null;
                        this.gameObject.transform.localScale = new Vector3(weaponAnchor.transform.lossyScale.x, 1f, 1f);
                        weaponAnchor.transform.parent = this.gameObject.transform;
                    }
                    this.gameObject.transform.localScale = new Vector3(scaleFactor, transform.localScale.y, transform.localScale.z);
                    weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, (90f * attackDirection.y * this.gameObject.transform.localScale.x));
                    attackDirection = Vector2.zero;
                }
                else if (!weapons[0].GetComponent<Weapon>().isAttacking && playerDirection != Vector2.zero)
                {
                    if (attackDirection.x > 0)
                    {
                        scaleFactor = 1f;
                    }
                    else if (attackDirection.x < 0)
                    {
                        scaleFactor = -1f;
                        Debug.Log("Got here");
                    }
                    this.gameObject.transform.localScale = new Vector3(scaleFactor, 1f, 1f);
                    if (weaponAnchor.transform.lossyScale.x != scaleFactor)
                    {
                        weaponAnchor.transform.parent = null;
                        weaponAnchor.transform.localScale = new Vector3(scaleFactor, weaponAnchor.transform.localScale.y, weaponAnchor.transform.localScale.z);
                        weaponAnchor.transform.parent = this.gameObject.transform;
                    }
                    weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, (attackDirection.y * 90f * scaleFactor));
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
            else if (attackDirection == Vector2.zero && weaponAim != Vector2.zero && !weapons[0].GetComponent<Weapon>().isAttacking && playerDirection != Vector2.zero)
            {
                if (inputX > 0)
                {
                    scaleFactor = 1f;
                }
                else if (inputX < 0)
                {
                    scaleFactor = -1f;
                }
                this.gameObject.transform.localScale = new Vector3(scaleFactor, 1f, 1f);
                if (weaponAnchor.transform.lossyScale.x != scaleFactor)
                {
                    weaponAnchor.transform.parent = null;
                    weaponAnchor.transform.localScale = new Vector3(scaleFactor, weaponAnchor.transform.localScale.y, weaponAnchor.transform.localScale.z);
                    weaponAnchor.transform.parent = this.gameObject.transform;
                }
                weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, (inputY * 90f * scaleFactor));
            }
            else if (attackDirection == Vector2.zero && playerDirection != Vector2.zero && weaponAim == Vector2.zero)
            {
                weaponAnchor.transform.localScale = new Vector3(1f, 1f, 1f);
                weaponAnchor.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            if (attacked)
            {
                cooldown = 0.3f;
                
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
                attacked = false;
            }

        }
        playerInitialDirection = playerNewDirection;
    }
    private void LateUpdate()
    {
        
    }
}
