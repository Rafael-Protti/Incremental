using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI textoDinheiro;
    [Space]
    public TextMeshProUGUI textoValorMultiplicador;
    public TextMeshProUGUI textoMultiplicadorAtual;
    string textoOriginaMultiplicador;
    [Space]
    public TextMeshProUGUI textoValorGanhoPassivo;
    public TextMeshProUGUI textoGanhoPassivoAtual;
    string textoOriginaGanhoPassivo;
    private void OnEnable()
    {
        LevelManager.OnDinheiroChange += AtualizarDinheiro;
    }

    private void Start()
    {
        textoOriginaMultiplicador = textoMultiplicadorAtual.text;
        textoOriginaGanhoPassivo = textoGanhoPassivoAtual.text;

        Invoke("AtualizarInterface", 0.1f);
    }

    void AtualizarInterface()
    {
        AtualizarDinheiro();
        AtualizarMultiplicador();
        AtualizarGanhoPassivo();
    }

    public void AtualizarDinheiro()
    {
        textoDinheiro.text = GameDirector.instancia.levelManager.dinheiro.ToString();
    }

    public void AtualizarMultiplicador()
    {
        textoValorMultiplicador.text = GameDirector.instancia.levelManager.ObtemPrecoMultiplicador().ToString();
        float ganho = GameDirector.instancia.levelManager.qntMultiplicador * GameDirector.instancia.levelManager.valorMultiplicador;
        textoMultiplicadorAtual.text = textoOriginaMultiplicador.Replace("{X}", (ganho - 1).ToString());
    }

    public void AtualizarGanhoPassivo()
    {
        textoValorGanhoPassivo.text = GameDirector.instancia.levelManager.ObtemPrecoGanhoPassivo().ToString();
        float ganho = GameDirector.instancia.levelManager.qntGanhosPassivos * GameDirector.instancia.levelManager.valorGanhoPassivo;
        textoGanhoPassivoAtual.text = textoOriginaGanhoPassivo.Replace("{X}", (ganho - 1).ToString());
    }
}