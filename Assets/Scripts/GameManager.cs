using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MatchPhase CurrentPhase;

    public float prepDuration = 60f;
    public float battleDuration = 45f;

    private float timer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartPrepPhase();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            AdvancePhase();
        }
    }

    void AdvancePhase()
    {
        if (CurrentPhase == MatchPhase.Prep)
            StartBattlePhase();
        else if (CurrentPhase == MatchPhase.Battle)
            StartRoundEnd();
    }

    void StartPrepPhase()
    {
        CurrentPhase = MatchPhase.Prep;
        timer = prepDuration;
        Debug.Log("Prep Phase");
    }

    void StartBattlePhase()
    {
        CurrentPhase = MatchPhase.Battle;
        timer = battleDuration;
        Debug.Log("Battle Phase");
    }

    void StartRoundEnd()
    {
        CurrentPhase = MatchPhase.RoundEnd;
        Debug.Log("Round End");
    }
}
