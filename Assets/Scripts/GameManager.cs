using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] private BossController bossCtrlr;
    [SerializeField] private CharacterController charCtrlr;

    public BossController GetBossController { get { return bossCtrlr; } }
    public CharacterController GetCharacterController { get { return charCtrlr; } }
}
