using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float maxSpeed = 20f;
    public float jumpHeight;
    public bool isGrounded;
    Animator anim;
    public int curHp;
    int maxHp = 3;
    public bool isHit = false;
    public Transform groundCheck;
    public Main main;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        curHp = maxHp;
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

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            main.Lose();
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
            transform.localRotation = Quaternion.Euler(0,0,0);
        if (Input.GetAxis("Horizontal") < 0 )
            transform.localRotation = Quaternion.Euler(0,180,0);
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
        if ((Input.GetButtonDown("Vertical") && isGrounded))
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
        }
        if (curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1.5f);
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

    void Lose()
    {
        main.GetComponent<Main>().Lose();
    }
}
