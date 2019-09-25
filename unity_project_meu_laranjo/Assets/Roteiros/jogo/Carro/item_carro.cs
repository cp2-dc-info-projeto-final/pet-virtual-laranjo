using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item_carro_", menuName = "Laranjo/Inventario/Item de Carro")]
public class item_carro : ScriptableObject
{
    public int id, id_ordem;
    public string[] nome;
    public string[] descricao;
    public int preco;
    public PosicaoItemCarro posicao;
    public GameObject prefab;
    public Sprite imagem;
    public raridadeItemCarro raridade;


}

    public enum PosicaoItemCarro
    {
        Chassi = 0,
        Roda,
        ArCapo,
        ArTeto,
        Carroceria,
        Aerofolio
    }
        public enum raridadeItemCarro
    {
        SemNivel = 0,
        Nivel1,
        Nivel2,
        Nivel3,
        Nivel4,
        Nivel5,
        Nivel6
    }