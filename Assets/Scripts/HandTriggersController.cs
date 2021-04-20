using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTriggersController : MonoBehaviour
{
    private const int PLAYER_LAYER = 9;
    private CharacterController charCtrlr;

    private void Start()
    {
        charCtrlr = GameManager.Instance.GetCharacterController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(PLAYER_LAYER) && other.gameObject.GetComponent<HealthController>())
        {
            other.gameObject.GetComponent<HealthController>().DamagePlayerHealthBy = 1;
        }
    }
}
