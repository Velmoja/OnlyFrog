using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLock : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            StartCoroutine(DestroyBlock()); 

        }
    }

    IEnumerator DestroyBlock() 
    {
        Destroy(this.gameObject, 5f);
        yield return new WaitForSeconds(5f);
        StopCoroutine(DestroyBlock());
    }
}
