using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MovementController : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody m_Rb;
    [SerializeField]
    private float m_PlayerSpeed = 5;
    [SerializeField]
    private float m_TurnSpeed = 360;
    private Vector3 mInput;

    [SerializeField] 
    
    LayerMask m_AimLayerMask;


    private void Update()
    {
        if (!IsOwner) { return; }
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        mInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, m_AimLayerMask))
        {
            var destination = hitInfo.point;
            destination.y = transform.position.y;

            Vector3 direction = destination - transform.position;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

    }
    private void Move()
    {
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        var skewedInput = matrix.MultiplyPoint3x4(mInput);
        m_Rb.MovePosition(transform.position + skewedInput * m_PlayerSpeed * Time.fixedDeltaTime);
    }

    public void SetSpeed (float speed)
    {
        m_PlayerSpeed = speed;
    }
    private void OnGUI()
    {
        GUILayout.Label($"Time {Time.deltaTime}");
    }
}
