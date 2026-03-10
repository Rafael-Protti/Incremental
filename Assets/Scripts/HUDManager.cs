using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI textoDinheiro;
    private void OnEnable()
    {
        LevelManager.OnDinheiroChange += AtualizarDinheiro;
    }

    public void AtualizarDinheiro()
    {
        textoDinheiro.text = GameDirector.instancia.levelManager.dinheiro.ToString();
    }
}