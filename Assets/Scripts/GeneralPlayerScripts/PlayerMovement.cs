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
    public bool flashComplete = false;
    public bool waitingForFlash = false;
    public Animator anim;
    public Animator camAnim;
    public int currentAnim = 0;
    public bool hit = false;
    public bool iFrames = false;
    public SpriteRenderer playerDashSprite;
    public SpriteRenderer playerParry;
    public SpriteRenderer passive;
    public SpriteRenderer active;
    public GameObject winText;
    public bool parry = false;
    public bool isParrying = false;
    public AnimationClip parryClip;
    public int parriesUsed = 0;
    public bool activeAbility=false;
    public GameObject guns;
    public GameObject phantomDissolve;
    [SerializeField] private float hitStunLength = 0.5f;
    [SerializeField] private float iFramesLength = 1.0f;
    private float initialHitStunLength;
    private float initialIFramesLength;
    public bool jump = false;
    public float timeChange;


    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialHitStunLength = hitStunLength;
        initialIFramesLength = iFramesLength;
        Debug.Log(initialHitStunLength);
        Debug.Log(initialIFramesLength);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
            dashed = context.action.triggered;

    }

    public void OnParry(InputAction.CallbackContext context)
    {
            parry = context.action.triggered;
    }

    public void OnActive(InputAction.CallbackContext context)
    {
        activeAbility = context.action.triggered;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.action.triggered;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (iFrames)
        {
            if(!waitingForFlash && !flashComplete)
            {
                StartCoroutine(WaitForFlash());
            }
            else if(flashComplete)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
        else if(!flashComplete && iFrames)
        {

        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (canDash)
        {
            playerDashSprite.color = Color.white;
        }
        if (!hit & !isParrying)
        {
            if (currentAnim != anim.GetCurrentAnimatorStateInfo(0).shortNameHash)
            {
                changeCollider();
            }
            currentAnim = anim.GetCurrentAnimatorStateInfo(0).shortNameHash;
            //Debug.Log(screenPos.x);
            //controls horizontal movement, moves at a constant speed
            float inputX = inputMovement.x;
            if (inputX != 0)
            {
                anim.SetBool("Running", true);
            }
            else
            {
                anim.SetBool("Running", false);

            }
            if (inputX < 0)
            {
                this.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (inputX > 0)
            {
                this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            transform.position += transform.right * inputX * MoveSpeed * Time.deltaTime;


            //controls jumping, uses an impulse force to give the feeling of a jump
            float inputY = inputMovement.y;
            if (rb.velocity.y == 0 && dashOnCooldown == false && canJump == true)
            {
                canDash = true;
                anim.SetBool("Falling", false);
            }
            if (rb.velocity.y == 0 && jump && canJump == true)
            {
                rb.AddForce(transform.up * 10, ForceMode2D.Impulse);
            }
            if (rb.velocity.y < -20f)
            {
                rb.velocity = new Vector2(rb.velocity.x, -20f);
            }
            if (rb.velocity.y < -0.05f || rb.velocity.y > 0.05f)
            {
                anim.SetBool("Falling", true);
            }
            if ((canDash == true) & (dashed == true))
            {
                StartCoroutine(Dash());
                anim.SetTrigger("Dashing");
                changeCollider();
            }
        }
        if (hp <= 0)
        {
            anim.Play("Dead");
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            winText.active = true;
            this.enabled = false;
        }
        if (parry && parriesUsed < 1 && !isParrying) 
        {
            StartCoroutine(Parry());
        }
        if (iFrames && !hit)
        {
            iFramesLength -= Time.deltaTime;
            if (iFramesLength <= 0f)
            {
                iFrames = false;
                iFramesLength = initialIFramesLength;
                Debug.Log(iFramesLength);
            }
        }
        else if (iFrames && hit)
        {
            hitStunLength -= Time.deltaTime;
            if (hitStunLength <=0)
            {
                hit = false;
                hitStunLength = initialHitStunLength;
            }
        }
        else
        {
            flashComplete = false;
        }
    }
    IEnumerator Dash()
    {
        dashOnCooldown = true;
        playerDashSprite.color = Color.grey;
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
            canDash = false;
            yield return new WaitForSeconds(0.75f);
            dashOnCooldown = false;
        }
        else if (cupheadDash == true)
        {
            GameObject decoy = Instantiate(phantomDissolve, this.transform.position, Quaternion.identity);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            guns.active = false;
            iFrames = true;
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
            canDash = false;
            yield return new WaitForSeconds(0.3f);
            iFrames = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            guns.active = true;
            yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(0.25f);
            Destroy(decoy);
            dashOnCooldown = false;

        }

    }

    public void changeCollider()
    {
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public void Damaged()
    {
        iFrames = true;
        hit = true;
        anim.Play("Hit", 0);
        camAnim.SetTrigger("DamageTaken");
    }

    public IEnumerator WaitForFlash()
    {
        waitingForFlash = true;
        yield return new WaitForSeconds(0.333f);
        flashComplete = true;
        waitingForFlash = false;
    }

    public IEnumerator Parry()
    {
        isParrying = true;
        parriesUsed = parriesUsed + 1;
        anim.Play("Parry", 0);
        yield return new WaitForSeconds(parryClip.length*2f);
        if (parriesUsed >= 1)
        {
            playerParry.color = Color.red;
        }
        isParrying = false;
    }

    public void iFrameTimer(float iFramesLength)
    {
        Debug.Log(iFramesLength);
        bool running = true;
        while (running)
        {

        }
    }
}
