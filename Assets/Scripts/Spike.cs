using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private int _lifes = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(1);
        ChangeLifes();
    }

    private void ChangeLifes()
    {
        if (_lifes - 1 <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _lifes--;
        }
    }
}
