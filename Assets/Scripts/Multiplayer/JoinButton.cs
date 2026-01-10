using UnityEngine;
using Unity.Netcode;

public class JoinButton : MonoBehaviour
{
    public void Join()
    {
        NetworkManager.Singleton.StartClient();
    }
}
