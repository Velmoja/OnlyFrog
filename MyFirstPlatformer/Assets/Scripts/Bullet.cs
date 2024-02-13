using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    float timeToDisable = 4f;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetDisabeled());
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
    IEnumerator SetDisabeled()
    {
        yield return new WaitForSeconds(timeToDisable);
        bulletPrefab.SetActive(false);
        Destroy(gameObject);
        Explode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(SetDisabeled());
        bulletPrefab.SetActive(false);
        if (collision.gameObject.tag != "Ground") 
        {
            Destroy(gameObject);
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); //Создается экземпляр созданного префаба на том месте, куда попала пуля.
        Destroy(gameObject); //Пуля уничтожается.
    }
}
