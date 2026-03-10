using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public HUDManager hudManager;

    public static GameDirector instancia;
    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(instancia);
        }
    }
}
