using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthController : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private BossController bossCtrlr;
    public int GetBossHealth { get { return health; } }


    private void Start()
    {
        bossCtrlr = GameManager.Instance.GetBossController;
    }

    private void BossDeath()
    {
        GameManager.Instance.BattleOver ();
    }

    public void DamageBoss(int damage)
    {
        BossHealthEnum bossHealthEnum = BossHealthEnum.FullStrength;
        health -= damage;
        Debug.Log("Health" + health);
        if(health <= 0)
        {
            health = 0;
            BossDeath();
        }

        if ( health > 75 && health <= 100 )
        {
            bossHealthEnum = BossHealthEnum.FullStrength;
        }
        else if ( health > 50 && health <= 75 )
        {
            bossHealthEnum = BossHealthEnum.Scratched;
        }
        else if ( health > 25 && health <= 50 )
        {
            bossHealthEnum = BossHealthEnum.Weak;
        }
        else if ( health > 0 && health <= 25 )
        {
            bossHealthEnum = BossHealthEnum.Critical;
        }

        bossCtrlr.UpdateHealthEnum ( bossHealthEnum );
        bossCtrlr.IsHit ();
    }
}
