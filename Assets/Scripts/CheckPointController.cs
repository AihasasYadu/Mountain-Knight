using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private const int PLAYER_LAYER = 9;
    private BossController bossCtrlr;
    private CharacterController charMovement;

    private void Awake()
    {
        bossCtrlr = GameManager.Instance.GetBossController;
        charMovement = GameManager.Instance.GetCharacterController;
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer.Equals(PLAYER_LAYER))
        {
            GetComponent<Collider>().isTrigger = false;
            bossCtrlr.SetBossAwake = true;
            charMovement.enabled = false;
        }
    }
}
