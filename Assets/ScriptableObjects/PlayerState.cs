using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PlayerState : ScriptableObject
{
    public string StateName;
    public float PlayerSpeed;
    public bool IsPhasable;
    public GameObject EquipedWeapon;
    public float PlayerScale = 1;

}
