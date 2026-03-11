using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string VERSAO = "1.0";
    public static float tempoJogo;

    private void Update()
    {
        tempoJogo += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.F12))
        {
            System.Diagnostics.Process.Start("explorer.exe", Application.persistentDataPath.Replace("/", "\\"));
        }
    }
}
