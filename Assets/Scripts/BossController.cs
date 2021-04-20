using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private const string BOSS_AWAKE_PARAM = "BossAwake";
    private bool isBossAwake = false;
    private bool isBattling = false;
    private bool isAttacking = false;

    private float idleTimer = 0;
    private float idleWaitTime = 10;
    private float attackTimer = 0;
    private float attackWaitTime = 2;
    private int attackCount = 1;
    [SerializeField] private Animator anim;
    [SerializeField] private BossHealthController bossHealth;
    [SerializeField] private BoxCollider handTrigger_L;
    [SerializeField] private BoxCollider handTrigger_R;

    public bool SetBattling { set { isBattling = value; } }
    public bool IsBossAwake { get { return isBossAwake; } }
    public bool SetBossAwake { set { isBossAwake = value; } }

    private void Update()
    {
        WakeUp();
    }

    private void WakeUp()
    {
        if(isBossAwake)
        {
            anim.SetBool(BOSS_AWAKE_PARAM, true);
            if(isBattling)
            {
                if(!isAttacking)
                {
                    idleTimer += Time.deltaTime;
                }
                else
                {
                    idleTimer = 0;
                    attackTimer += Time.deltaTime;
                    if(attackTimer >= attackWaitTime)
                    {
                        isAttacking = false;
                        attackTimer = 0;
                        handTrigger_L.enabled = true;
                        handTrigger_R.enabled = true;
                    }
                }
                if(idleTimer >= idleWaitTime)
                {
                    isAttacking = true;
                    idleTimer = 0;
                }
            }
            else
            {
                idleTimer = 0;
            }
        }
    }
}
