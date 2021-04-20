using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthController : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private BossController bossCtrlr;
    public int DamageBoss { set { UpdateHealth(value); } }
    public int GetBossHealth { get { return health; } }

    private void Start()
    {
        bossCtrlr = GameManager.Instance.GetBossController;
    }

    private void BossDeath()
    {
        StartCoroutine(LevelRestart());
    }

    private void UpdateHealth(int damage)
    {
        health -= damage;
        Debug.Log("Health" + health);
        if(health <= 0)
        {
            health = 0;
            BossDeath();
        }
    }

    private IEnumerator LevelRestart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
