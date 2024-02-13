using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Explode()); //��� �������� ������� ���������� ����� Explode().
    }

    IEnumerator Explode()
    {

        yield return new WaitForSeconds(0.16f); //����� ���� 0,32 �������, � ������ ����� ���������� � ���������� ���������� ����.

        Destroy(gameObject); //������ ���������.
    }
}
