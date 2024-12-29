using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialonecontroller : MonoBehaviour
{
    [SerializeField] private GameObject tutorialOne;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            tutorialOne.SetActive(true);
        }
    }
}