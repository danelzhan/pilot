using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BlasterShot : NetworkBehaviour
{
    [SerializeField] GameObject m_ExplosionPrefab;

    [SerializeField] private float m_Speed;

    [SerializeField] private bool m_IsExplosive;


    [Rpc(SendTo.ClientsAndHost)]
    public void LaunchRpc (Vector3 direction)
    {
        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().linearVelocity = direction * m_Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (m_IsExplosive)
        {
            GameObject spawnedObject = Instantiate(m_ExplosionPrefab, transform.position, transform.rotation);
            spawnedObject.GetComponent<NetworkObject>().Spawn();
        }
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
