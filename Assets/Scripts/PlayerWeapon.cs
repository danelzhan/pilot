using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum WeaponType {Projectile, Melee}

public abstract class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] protected WeaponType p_Type;
    [SerializeField] protected float p_Delay;
    protected float m_NextAttackTime;

    // MODIFIES: THIS
    public abstract void StandardAttackRpc(Vector3 transf, Quaternion rotation);

    public WeaponType GetType() {return p_Type; }

}
