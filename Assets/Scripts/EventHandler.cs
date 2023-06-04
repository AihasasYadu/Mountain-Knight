using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private BossController bossCtrlr;
    private CharacterController charCtrlr;

    private void Start()
    {
        bossCtrlr = GameManager.Instance.GetBossController;
        charCtrlr = GameManager.Instance.GetCharacterController;
    }

    private void EnableCharMovement()
    {
        charCtrlr.enabled = true;
    }

    public void EnableBossBattle()
    {
        EnableCharMovement();
        bossCtrlr.SetBattling = true;
    }

    public void FireProjectile ()
    {
        charCtrlr.FireProjectile ();
    }

    public void FallApart ()
    {
        charCtrlr.FallApartCharacter ();
    }
}
