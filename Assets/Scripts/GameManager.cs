using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] 
    private BossController bossCtrlr;
    public BossController GetBossController 
    { 
        get 
        { 
            return bossCtrlr; 
        } 
    }

    [SerializeField] 
    private CharacterController charCtrlr;
    public CharacterController GetCharacterController 
    { 
        get 
        { 
            return charCtrlr; 
        } 
    }

    private bool isBattleActive = false;
    public bool IsBattleActive
    {
        get
        {
            return isBattleActive;
        }

        set
        {
            isBattleActive = value;
        }
    }

    public void BattleOver ()
    {
        // do all end game setup here
        IsBattleActive = false;
    }

    public void GameOver ()
    {
        BattleOver ();
        StartCoroutine ( LevelRestart() );
    }

    private IEnumerator LevelRestart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
