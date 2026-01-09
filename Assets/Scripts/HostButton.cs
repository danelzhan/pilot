using Unity.Netcode;
using UnityEngine;

public class HostButton : MonoBehaviour
{
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
    }
}
