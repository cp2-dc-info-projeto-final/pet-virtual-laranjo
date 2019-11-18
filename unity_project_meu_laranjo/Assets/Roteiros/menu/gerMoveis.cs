using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gerMoveis : MonoBehaviour
{
    public static gerMoveis instancia;
    public List<GameObject> moveis;

    public List<item_movel> itens_movel;
    public List<item_pintura_movel> itens_pintura_movel;

    public GameObject pivot_moveis;

    // Start is called before the first frame update

    void Awake() {
        instancia = this;
    }

    void Start()
    {
        aplicarMoveis(gerDados.instancia.dados_.moveis);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void aplicarMoveis(int[] moveis_){

        moveis[1].GetComponent<MeshRenderer>().sharedMaterials[0].mainTexture = itemPinturaMovelDeId(moveis_[1]).textura;
        moveis[2].GetComponent<MeshRenderer>().sharedMaterials[0].mainTexture = itemPinturaMovelDeId(moveis_[2]).textura;
        moveis[3].GetComponent<MeshRenderer>().sharedMaterials[0].mainTexture = itemPinturaMovelDeId(moveis_[3]).textura;
        moveis[4].GetComponent<MeshRenderer>().sharedMaterials[0].mainTexture = itemPinturaMovelDeId(moveis_[4]).textura;

        moveis[5].GetComponent<MeshRenderer>().sharedMaterials = new Material[5]{new Material(Shader.Find("Standard")),new Material(Shader.Find("Standard")),new Material(Shader.Find("Standard")),new Material(Shader.Find("Standard")),new Material(Shader.Find("Standard"))};

        moveis[5].GetComponent<MeshRenderer>().sharedMaterials[0].mainTexture = itemPinturaMovelDeId(moveis_[5]).textura;
        moveis[5].GetComponent<MeshRenderer>().sharedMaterials[1].mainTexture = itemPinturaMovelDeId(moveis_[6]).textura;
        moveis[5].GetComponent<MeshRenderer>().sharedMaterials[2].mainTexture = itemPinturaMovelDeId(moveis_[7]).textura;
        moveis[5].GetComponent<MeshRenderer>().sharedMaterials[3].mainTexture = itemPinturaMovelDeId(moveis_[8]).textura;
        moveis[5].GetComponent<MeshRenderer>().sharedMaterials[4].mainTexture = itemPinturaMovelDeId(moveis_[9]).textura;

        for(int i_ = 10; i_ < moveis_.Length; i_++){
            Vector3 pos_;
            Quaternion rot_;

            pos_ = moveis[i_].transform.position;
            rot_ = moveis[i_].transform.rotation;

            Destroy(moveis[i_]);

            moveis[i_] = Instantiate(itemMovelDeId(moveis_[i_], (PosicaoItemMovel) i_).prefab,pos_,rot_,pivot_moveis.transform);
        }

    }

    public item_movel itemMovelDeId(int id_, PosicaoItemMovel tipo_){

        item_movel it_ = new item_movel();

        foreach(item_movel item_ in itens_movel){
            if(item_ != null){
                if(item_.id == id_ && item_.posicao == tipo_){
                    it_ = item_;
                }
            }
        }

        return it_;
    }

    public item_pintura_movel itemPinturaMovelDeId(int id_){

        item_pintura_movel it_ = new item_pintura_movel();

        foreach(item_pintura_movel item_ in itens_pintura_movel){
            if(item_ != null){
                if(item_.id == id_){
                    it_ = item_;
                }
            }
        }

        return it_;
    }
}
