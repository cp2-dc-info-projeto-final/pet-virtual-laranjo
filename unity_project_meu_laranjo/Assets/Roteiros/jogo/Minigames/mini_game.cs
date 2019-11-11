using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "minigame", menuName = "Laranjo/MiniGame/Novo Minigame")]
public class mini_game : ScriptableObject
{
    public int id;
    public string[] nome = new string[]{"Nome","Name","Nombre"};
    public string[] desc = new string[]{"Descricao do minigame","Game description","Descripción del minijuego"};
    public TipoDePontuacao tipoDePont = TipoDePontuacao.Ponto;
}

public enum TipoDePontuacao
{
    Ponto,
    Tempo
}