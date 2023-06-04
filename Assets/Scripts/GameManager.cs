using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] 
    private BossController bossCtrlr;
    public BossController GetBossController { get { return bossCtrlr; } }

    [SerializeField] 
    private CharacterController charCtrlr;
    public CharacterController GetCharacterController { get { return charCtrlr; } }
}
