using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Explode()); //При создании объекта вызывается метод Explode().
    }

    IEnumerator Explode()
    {

        yield return new WaitForSeconds(0.16f); //Метод ждет 0,32 секунды, и только потом приступает к выполнению остального кода.

        Destroy(gameObject); //Объект удаляется.
    }
}
