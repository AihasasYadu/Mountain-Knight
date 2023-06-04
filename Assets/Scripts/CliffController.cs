using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffController : MonoBehaviour
{
    private const int PLAYER_LAYER = 9;

    [SerializeField]
    private bool isRightCliff = false;

    public void OnTriggerEnter ( Collider collider )
    {
        if ( collider.gameObject.layer.Equals ( PLAYER_LAYER ) )
        {
            GameManager.Instance.GetBossController.SwitchAttackCliff ( isRightCliff );
        }
    }
}
