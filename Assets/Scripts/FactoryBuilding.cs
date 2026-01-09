using UnityEngine;

public abstract class FactoryBuilding : MonoBehaviour
{
    public float buildTime = 3f;
    protected bool isBuilt;

    public void Build()
    {
        Invoke(nameof(FinishBuild), buildTime);
    }

    void FinishBuild()
    {
        isBuilt = true;
        OnBuilt();
    }

    protected abstract void OnBuilt();
}