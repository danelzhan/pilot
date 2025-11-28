using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MeleeWeapon : PlayerWeapon
{
    [SerializeField]
    private Animator m_Animator;


    [Rpc(SendTo.Server)]
    public override void StandardAttackRpc(Vector3 transf, Quaternion Rotation)
    {
        float delay = p_Delay;

        m_NextAttackTime = Time.time + delay;
        Debug.Log(m_Animator.isActiveAndEnabled);
        m_Animator.SetTrigger("Swing");

    }
}
