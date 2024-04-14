using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    float timeToDisable = 4f;
    public GameObject explosion;
    public GameObject instBullet;


    void Start()
    {
        StartCoroutine(SetDisabeled());
    }

    IEnumerator SetDisabeled()
    {
        yield return new WaitForSeconds(timeToDisable);
        bulletPrefab.SetActive(false);
        Destroy(bulletPrefab, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(SetDisabeled());
        Destroy(bulletPrefab);
        Explode();
        if (collision.gameObject.tag != "Ground") 
        {
            Destroy(bulletPrefab);
        }
    }

    public void Explode() 
    {
        GameObject newExp = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(newExp, 2f);
    } 
}
