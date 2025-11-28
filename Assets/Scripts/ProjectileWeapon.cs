using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileWeapon : PlayerWeapon
{
    [SerializeField] BlasterShot m_BlasterShotPrefab;

    public override void StandardAttackRpc(Vector3 transform, Quaternion rotation)
    {
        
        float delay = p_Delay;
        Debug.Log("attack");
        m_NextAttackTime = Time.time + delay;
        SpawnServerRpc(transform, rotation);

    }

    [ServerRpc]
    private void SpawnServerRpc(Vector3 transf, Quaternion rot)
    {
        var shot = Instantiate(m_BlasterShotPrefab, transf, rot);
        shot.GetComponent<NetworkObject>().Spawn(true);
        Vector3 fwd = rot * Vector3.forward;
        shot.GetComponent<BlasterShot>().LaunchRpc(fwd);
        //shot.transform.localScale(PlayerController.Instance.state
    }
}
