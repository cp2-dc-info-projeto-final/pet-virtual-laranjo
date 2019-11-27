using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item_skin_00", menuName = "Laranjo/Inventario/Item de Skin")]
public class item_skin : item
{
    public Material[] material = new Material[3];

    public Material materialResultado(float nivel_){
        Material mat_ = new Material(Shader.Find("Standard"));

        mat_.mainTexture = material[0].mainTexture;

        
        mat_.SetFloat("_Metallic", material[2].GetFloat("_Metallic"));
        mat_.SetFloat("_Glossiness", material[2].GetFloat("_Glossiness"))   ;

        if(nivel_ < 0.5f){

            mat_.color = Color.Lerp(material[0].color,material[1].color,gerDados.instancia.dados_.nivel * 2);

        }else
        {
            mat_.color = Color.Lerp(material[1].color,material[2].color,(nivel_ - 0.5f) * 2);
        }

        return mat_;
    }
}
