using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthPref;
    [SerializeField] private Player pl;
    public int maxHealth;
    public int curHealth;

    public List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        pl = FindObjectOfType<Player>();
        maxHealth = pl.maxHp;
        DrawHealth();
        curHealth = maxHealth;
    }

    public void DrawHealth() 
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(healthPref, transform);
            hearts.Add(heart);
        }
    }
    public void UpdateHealth()
    {
        // �������� �� ������ �������� � �������� ������� 
        // (����� �������� ������� � ���������� �������� ��� ��������)
        for (int i = hearts.Count - 1; i >= 0; i--)
        {
            if (i < curHealth)
            {
                hearts[i].SetActive(true); // ���������� ������ ������
            }
            else
            {
                // ������� �������� ������ �� ������ � �����
                Destroy(hearts[i]);
                hearts.RemoveAt(i);
            }
        }
    }
}
