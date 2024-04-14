using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    private AdWarning aw;
    SpriteRenderer spriteRenderer;
    public DeathCounter DeathCounter;
    public AudioController audController;
    public HealthBar healthBar;
    public float speed;
    public float maxSpeed = 20f;
    public float jumpHeight;
    public bool isGrounded;
    public int curHp;
    public int maxHp = 3;
    public bool isHit = false;
    public Transform groundCheck;
    public Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        aw = GetComponent<AdWarning>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        curHp = maxHp;
        audController = FindObjectOfType<AudioController>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Jump();
        if (Input.GetAxis("Horizontal") == 0 && (isGrounded))
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            Flip();
            if (isGrounded)
                anim.SetInteger("State", 2);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Flip() 
    {
        if (Input.GetAxis("Horizontal") > 0 )
            transform.localScale = new Vector3(2, 2, 2);
        if (Input.GetAxis("Horizontal") < 0 )
            transform.localScale = new Vector3(-2, 2, 2);
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;

        if (!isGrounded)
            anim.SetInteger("State", 3);
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Vertical") && isGrounded) && Input.GetKeyDown(KeyCode.W) || (Input.GetButtonDown("Vertical") && isGrounded) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    public void RecountHp(int deltaHP)
    {
        curHp = curHp + deltaHP;
        if (deltaHP < 0)
        {
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit());
            audController.SoundHit();
            healthBar.curHealth = curHp;
            healthBar.UpdateHealth();
        }
        if (curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1.5f);
            healthBar.curHealth = curHp;
            healthBar.UpdateHealth();
            DeathCounter.DeathCount++;
        }
    }

    IEnumerator OnHit()
    {
        if (isHit)
            spriteRenderer.color = new Color(1f, spriteRenderer.color.g - 0.04f, spriteRenderer.color.b - 0.04f);
        else
            spriteRenderer.color = new Color(1f, spriteRenderer.color.g + 0.04f, spriteRenderer.color.b + 0.04f);

        if (spriteRenderer.color.g == 1)
            StopCoroutine(OnHit());

        if (spriteRenderer.color.g <= 0)
        {
            isHit = false;
            anim.SetInteger("hit", 1);
        }

        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }

    public void Lose()
    {
        this.gameObject.transform.position = startPos.position;
        GetComponent<CapsuleCollider2D>().enabled = true;
        healthBar.DrawHealth();
        curHp = 3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hamumu") 
        {
            audController.SoundHamumuJump();
        }
    }
}
