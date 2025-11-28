using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System;

public class PlayerController : NetworkBehaviour
{
    public static PlayerController Instance;
    public Transform MeleeAttackPoint;
    public Transform ProjectileAttackPoint;

    public TextMeshProUGUI UIText;


    private PlayerState mPlayerState;
    private int mCurrentStateIndex;
    [SerializeField]
    private AllPlayerStates m_AllPlayerStates;

    [SerializeField]
    private MovementController m_MovementController;


    private GameObject mCurrentWeaponGO;
    private GameObject mCurrentWeaponGOPrefab;
    private PlayerWeapon mCurrentWeaponx;

    [SerializeField] private PlayerWeapon mCurrentWeapon;

    // Start is called before the first frame update

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        ChangeState(m_AllPlayerStates.AllStates[0]);
        mCurrentStateIndex = 1;
        Debug.Log("state changed");
        mPlayerState = m_AllPlayerStates.AllStates[mCurrentStateIndex];
        NetworkUI.OnStateChanged.Invoke(OwnerClientId, "shooter");

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("attack");
            if (mPlayerState.EquipedWeapon != null)
            {
                switch (mCurrentWeapon.GetType()) {
                    case WeaponType.Melee:
                        mCurrentWeapon.StandardAttackRpc(ProjectileAttackPoint.position, transform.rotation);
                        break;
                    case WeaponType.Projectile:
                        mCurrentWeapon.StandardAttackRpc(ProjectileAttackPoint.position, transform.rotation);
                        break;

                }
            }
        }
        if (Input.GetKeyDown("space"))
        {
            /*
            mCurrentStateIndex += 1;
            if (mCurrentStateIndex >= m_AllPlayerStates.AllStates.Count)
            {
                mCurrentStateIndex = 0;
            } 
            ChangeState(m_AllPlayerStates.AllStates[mCurrentStateIndex]);
            */
        }
    }


    public void ChangeState(PlayerState state)
    {
        Destroy(mCurrentWeaponGOPrefab);
        //UIText.text = state.StateName;

        mPlayerState = state;
        m_MovementController.SetSpeed(state.PlayerSpeed);
        Physics.IgnoreLayerCollision(3, 7, mPlayerState.IsPhasable);

        transform.localScale = new Vector3(1,1,1) * state.PlayerScale;

        mCurrentWeaponGO = mPlayerState.EquipedWeapon;

        if (mCurrentWeaponGO != null)
        {
            mCurrentWeapon = mCurrentWeaponGO.GetComponent<PlayerWeapon>();
        }

        if (mCurrentWeaponGO != null)
        {
            if (mCurrentWeapon.GetType() == WeaponType.Melee)
            {
                mCurrentWeaponGOPrefab = Instantiate(mCurrentWeaponGO, MeleeAttackPoint.position, transform.rotation, MeleeAttackPoint);
            } 
        }
    }
}
