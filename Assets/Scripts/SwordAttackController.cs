using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackController : MonoBehaviour
{
    private const int BOSS_LAYER = 10;
    private int lifeSpan = 2;
    [SerializeField] private int attackPower = 30;
    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer.Equals(BOSS_LAYER))
        {
            if (collision.gameObject.GetComponent<BossHealthController>() && collision.gameObject.GetComponent<BossHealthController>().GetBossHealth > 0)
            {
                collision.gameObject.GetComponent<BossHealthController>().DamageBoss = attackPower;
                Destroy(gameObject);
            }
        }
    }
}
