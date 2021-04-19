using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackController : MonoBehaviour
{
    private int lifeSpan = 2;
    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }
}
