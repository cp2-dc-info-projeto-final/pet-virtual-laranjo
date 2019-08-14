using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "peca", menuName = "Laranjo/Terreno/Peca")]
public class peca_terreno : ScriptableObject
{
    public int id, probabilidade;

    public List<int> conexao_1, conexao_2, conexao_3, conexao_4;

    public GameObject[] prefabs;
}
