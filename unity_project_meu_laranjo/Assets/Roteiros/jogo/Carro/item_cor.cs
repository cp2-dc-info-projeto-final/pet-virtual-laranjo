using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item_cor_", menuName = "Laranjo/Inventario/Item de Cor")]
public class item_cor : ScriptableObject
{
    public int id;
    public string[] nome;
    public string[] descricao;
    public bool listado = true;
    public int preco_moedas, preco_dolares;
    public Material material;
    public Sprite imagem;
}
