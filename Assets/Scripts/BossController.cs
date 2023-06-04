using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private const string BOSS_AWAKE_PARAM = "bossAwake";

    [SerializeField]
    private bool isBossAwake = false;
    [SerializeField]
    private bool isBattling = false;
    [SerializeField]
    private bool isAttacking = false;
    [SerializeField]
    private float idleTimer = 0;
    [SerializeField]
    private float idleWaitTime = 10;
    [SerializeField]
    private float attackTimer = 0;
    [SerializeField]
    private float attackWaitTime = 2;
    [SerializeField]
    private int attackCount = 0;
    [SerializeField]
    private int attackLimit = 0;

    [SerializeField] 
    private Animator anim = null;

    [SerializeField] 
    private BossHealthController bossHealth = null;

    [SerializeField] 
    private BoxCollider handTrigger_L = null;

    [SerializeField] 
    private BoxCollider handTrigger_R = null;

    [SerializeField]
    private BossHealthEnum bossHealthEnum = BossHealthEnum.FullStrength;

    public bool SetBattling 
    { 
        set 
        { 
            isBattling = value;
        } 
    }
    public bool IsBossAwake 
    { 
        get 
        { 
            return isBossAwake;
        } 

        private set
        { 
            isBossAwake = value;
        } 
    }

    public void WakeUp ()
    {
        anim.SetBool(BOSS_AWAKE_PARAM, true);
        IsBossAwake = true;
    }

    public void InitateBossAttack ()
    {
        updateAttack ();
        StartCoroutine (bossAttack ());
    }

    private IEnumerator bossAttack()
    {
        while ( GameManager.Instance.IsBattleActive )
        {
            if (isBossAwake)
            {
                if(isBattling)
                {
                    if(!isAttacking)
                    {
                        idleTimer += 1;
                    }
                    else
                    {
                        idleTimer = 0;
                        attackTimer += 1;
                        anim.SetBool ( "attackReady", true );
    
                        if(attackTimer >= attackWaitTime)
                        {
                            anim.SetTrigger ( "bossAttack" );
    
                            attackTimer = 0;
    
                            handTrigger_L.enabled = true;
                            handTrigger_R.enabled = true;

                            if ( attackCount >= attackLimit )
                            {
                                isAttacking = false;
                                attackCount = 0;
                                attackTimer = 0;
                                anim.SetBool ( "attackReady", false );
                            }
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
                
                anim.SetInteger ( "attackCount", attackCount );
                yield return new WaitForSeconds ( 1 );
            }
        }
    }

    private void updateAttack ()
    {
        switch ( bossHealthEnum )
        {
            case BossHealthEnum.FullStrength:
            {
                attackLimit = 1;
                attackWaitTime = 4.0f;
                break;
            }
            case BossHealthEnum.Scratched:
            {
                attackLimit = 2;
                attackWaitTime = 3.0f;
                break;
            }
            case BossHealthEnum.Weak:
            {
                attackLimit = 3;
                attackWaitTime = 2.0f;
                break;
            }
            case BossHealthEnum.Critical:
            {
                attackLimit = 4;
                attackWaitTime = 1.0f;
                break;
            }
        }
    }

    public void UpdateHealthEnum ( BossHealthEnum healthEnum )
    {
        if ( healthEnum != bossHealthEnum )
        {
            bossHealthEnum = healthEnum;
            updateAttack ();
        }
    }

    public void AttackCompleted ()
    {
        attackCount += 1;
    }

    public void SwitchAttackCliff ( bool switchToRight )
    {
        anim.SetBool ( "playerRight", switchToRight );
    }

    public void IsHit ()
    {
        anim.SetTrigger ( "isHit" );
    }
}
