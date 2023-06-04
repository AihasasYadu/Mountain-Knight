using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField] 
    private int health = 0;

    [SerializeField] 
    private ParticleSystemRenderer aura = null;

    private CharacterController character = null;
    private Color auraColor = Color.green;
    private float timer = 0.0f;
    private bool isPlayerDead = false;

    public int DamagePlayerHealthBy { set { DamageDealt(value); } }

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        aura = GetComponent<ParticleSystemRenderer>();
    }

    private void Update()
    {
        updateHealth();
    }

    private void DamageDealt(int damage)
    {
        health -= damage;
    }

    private void updateHealth()
    {
        if(health == 3)
        {
            auraColor = Color.green;
            aura.material.color = auraColor;
        }
        if(health == 2)
        {
            auraColor = Color.yellow;
            aura.material.color = auraColor;
        }
        if(health == 1)
        {
            auraColor = Color.red;
            aura.material.color = auraColor;
        }
        if(health <= 0)
        {
            GameManager.Instance.BattleOver();
            if ( !isPlayerDead )
            {
                playerDying ();
            }
            else
            {
                playerDead ();
                StartCoroutine(levelReset());
            }
        }
    }

    private IEnumerator levelReset()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void playerDying ()
    {
        isPlayerDead = true;
        character.CharacterAnim.SetBool ( "isDead", true );
    }

    private void playerDead ()
    {
        if ( character.CharacterAnim.GetCurrentAnimatorStateInfo (0).IsName ( "Knight_Death_01_F" ) )
        {
            character.CharacterAnim.SetBool ( "isDead", false );
        }

        character.CharacterAnim.SetFloat ( "speed", 0.0f );
        character.enabled = false;
        GameManager.Instance.GameOver ();
    }
}
