using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private ParticleSystemRenderer aura;

    private CharacterController character;
    private Color auraColor;
    private float timer;

    public int DamagePlayerHealthBy { set { DamageDealt(value); } }

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        aura = GetComponent<ParticleSystemRenderer>();
    }

    private void Update()
    {
        UpdateHealth();
    }

    private void DamageDealt(int damage)
    {
        health -= damage;
    }

    private void UpdateHealth()
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
        if(health == 0)
        {
            character.enabled = false;
            StartCoroutine(LevelReset());
        }
    }

    private IEnumerator LevelReset()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
