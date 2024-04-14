using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject pl;
    [SerializeField] private Transform start;
    public void Lose()
    {
        pl.transform.position = start.position;
    }
}
