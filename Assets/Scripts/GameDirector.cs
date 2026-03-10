using Unity.VisualScripting;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public HUDManager hudManager;

    public static GameDirector instancia;
    void Awake()
    {
        instancia = this;
    }
}
