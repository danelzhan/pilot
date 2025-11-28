using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttack : MonoBehaviour
{
    [SerializeField]
    private float DespawnTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

}
