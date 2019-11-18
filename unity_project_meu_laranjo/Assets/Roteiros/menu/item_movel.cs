using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Laranjo/Moveis/Item de Roupa")]
public class item_movel : ScriptableObject
{
    public int id, id_ordem;
    public string[] nome;
    public string[] descricao;
    public bool listado = true;
    public int preco_moedas;
    public int preco_dolares;
    public PosicaoItemMovel posicao;
    public GameObject prefab;
    public Sprite imagem;
    public raridadeItemRoupa raridade;

    
}

public enum PosicaoItemMovel
    {
        Porta = 10,
        Sofa,
        Estante,
        Televisao,
        Cama,
        Escrivaninha,
        Cadeira,
        Material,
        Vaso,
        Pia_Banheiro,
        Chuveiro,
        Fogao,
        Pia_Cozinha,
        Geladeira
    }

    public enum raridadeItemRoupa
    {
        SemNivel = 0,
        Nivel1,
        Nivel2,
        Nivel3,
        Nivel4,
        Nivel5,
        Nivel6
    }