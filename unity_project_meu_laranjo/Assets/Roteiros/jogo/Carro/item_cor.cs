using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item_cor_", menuName = "Laranjo/Inventario/Item de Cor")]
public class item_cor : ScriptableObject
{
    public int id;
    public string nome;
    public string descricao;
    public int preco;
    public Material material;
    public Sprite imagem;
}
