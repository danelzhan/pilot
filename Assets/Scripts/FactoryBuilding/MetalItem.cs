using UnityEngine;

public class MetalItem : MonoBehaviour
{
    private Vector3Int [] mPath;
    private int mCurrentIndex = 0;

    [SerializeField]
    private float m_MoveSpeed = 2f;
    [SerializeField]
    private float m_ReachThreshold = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mPath == null || mPath.Length == 0)
            return;

        if (mCurrentIndex >= mPath.Length)
        {
            Destroy(gameObject);
            TeamInventory.Instance.AddMetal(1);
            return;
        }

        Vector3 target = mPath[mCurrentIndex];
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            m_MoveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) <= m_ReachThreshold)
        {
            mCurrentIndex++;
        }
    }

    public void Init(Vector3Int[] path)
    {
        mPath = path;
    }
}
