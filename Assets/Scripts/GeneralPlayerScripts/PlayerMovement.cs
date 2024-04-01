using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float JumpSpeed = 20f;
    public float FallSpeed = 20f;
    public float OriginalFallSpeed = 20f;
    public Rigidbody2D rb;
    [SerializeField] private bool canDash=true;
    [SerializeField] private float horizontalDash=15f;
    [SerializeField] private float verticalDash=15f;
    [SerializeField] private bool dashOnCooldown=false;
    [SerializeField] private bool cupheadDash = false;
    [SerializeField] private float dashTime = 0.25f;
    [SerializeField] private bool canJump = true;
    [SerializeField] public int hp = 3;
    public GameObject playerAttack;
    private Vector2 inputMovement;
    public bool dashed = false;
    public bool attacked = false;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        dashed = context.action.triggered;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(screenPos.x);
        //controls horizontal movement, moves at a constant speed
        float inputX = inputMovement.x;
        transform.position += transform.right * inputX * MoveSpeed * Time.deltaTime;

        if (inputX < 0 && !playerAttack.active)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        if (inputX > 0 && !playerAttack.active)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }


        //controls jumping, uses an impulse force to give the feeling of a jump
        float inputY = inputMovement.y;
        if (rb.velocity.y == 0 && dashOnCooldown == false && canJump == true)
        {
            canDash = true;
        }
        if (rb.velocity.y == 0 && inputY > 0 && canJump == true)
        {
            rb.AddForce(transform.up * 10, ForceMode2D.Impulse);
        }
        if (rb.velocity.y < -20f)
        {
           rb.velocity = new Vector2(rb.velocity.x, -20f);
        }
        if ((canDash == true) & (dashed == true))
        {
            StartCoroutine(Dash());
            anim.SetTrigger("Dashing");
        }
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        if (attacked)
        {
            playerAttack.active = true;
            attacked = false;
        }

    }
    IEnumerator Dash()
    {
        dashOnCooldown = true;
        float inputX = inputMovement.x;
        float inputY = inputMovement.y;
        if (cupheadDash == false)
        {
            if (inputX > 0)
            {
                rb.velocity = new Vector2(horizontalDash, rb.velocity.y);
            }
            else if (inputX < 0)
            {
                rb.velocity = new Vector2(horizontalDash * -1, rb.velocity.y);
            }
            if (inputY > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalDash);
            }
            else if (inputY < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalDash * -1);
            }
        }
        else if (cupheadDash == true)
        {
            if (inputX > 0)
            {
                rb.velocity = new Vector2(horizontalDash, 0);
                FallSpeed = 0;
                yield return new WaitForSeconds(dashTime);
                FallSpeed = OriginalFallSpeed;
            }
            else if (inputX < 0)
            {
                rb.velocity = new Vector2(horizontalDash * -1, 0);
                FallSpeed = 0;
                yield return new WaitForSeconds(dashTime);
                FallSpeed = OriginalFallSpeed;
            }
            if (inputY > 0 && inputX > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalDash);
            }
            else if (inputY < 0 && inputX > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalDash * -1);
            }
        }
        canDash = false;
        yield return new WaitForSeconds(0.75f);
        dashOnCooldown = false;
    }
}
