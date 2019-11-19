using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class gerLojaMoveis : MonoBehaviour
{
    public int[] moveis_atuais = new int[24];
    public int item_selecionado = -1, item_tipo_selecionado = -1;

    public GameObject lista_loja;
    public Button botao_loja_confirmar_compra_moeda, botao_loja_comprar;
    public TextMeshProUGUI[] textoLoja;
    public GameObject menu_confirmar_compra, menu_confirmar_compra_pivot, menu_comprar_moedas, menu_comprar_dolares;

    public GameObject[] preview_loja, camera_preview_loja;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botaoLoja(int id_, int tipo_){

        item_selecionado = id_;
        item_tipo_selecionado = tipo_;

        if(tipo_ < 10){
            textoLoja[0].text = gerMoveis.instancia.itemPinturaMovelDeId(id_).nome[gerDados.instancia.dados_.lingua];
            textoLoja[1].text = gerMoveis.instancia.itemPinturaMovelDeId(id_).descricao[gerDados.instancia.dados_.lingua];

            textoLoja[2].text = gerMoveis.instancia.itemPinturaMovelDeId(id_).preco_moedas.ToString();
            textoLoja[3].text = gerMoveis.instancia.itemPinturaMovelDeId(id_).preco_dolares.ToString();

            textoLoja[4].text = gerMoveis.instancia.itemPinturaMovelDeId(id_).preco_moedas.ToString();
            textoLoja[5].text = gerMoveis.instancia.itemPinturaMovelDeId(id_).preco_dolares.ToString();


        }else
        {
            textoLoja[0].text = gerMoveis.instancia.itemMovelDeId(id_, (PosicaoItemMovel)tipo_).nome[gerDados.instancia.dados_.lingua];
            textoLoja[1].text = gerMoveis.instancia.itemMovelDeId(id_, (PosicaoItemMovel)tipo_).descricao[gerDados.instancia.dados_.lingua];

            textoLoja[2].text = gerMoveis.instancia.itemMovelDeId(id_, (PosicaoItemMovel)tipo_).preco_moedas.ToString();
            textoLoja[3].text = gerMoveis.instancia.itemMovelDeId(id_, (PosicaoItemMovel)tipo_).preco_dolares.ToString();

            textoLoja[4].text = gerMoveis.instancia.itemMovelDeId(id_, (PosicaoItemMovel)tipo_).preco_moedas.ToString();
            textoLoja[5].text = gerMoveis.instancia.itemMovelDeId(id_, (PosicaoItemMovel)tipo_).preco_dolares.ToString();


            moveis_atuais[tipo_] = id_;
            
        }

        moveis_atuais[tipo_] = id_;

        gerMoveis.instancia.aplicarMoveis(moveis_atuais);

        

        //gerenciador.instancia.instanciar_casa(id_);
    }

    public void botaoFecharLoja(){
        gerMoveis.instancia.aplicarMoveis(gerDados.instancia.dados_.moveis);
    }

    public void gerarloja(int posi_){



        foreach (GameObject cam_ in camera_preview_loja)
        {
            if(cam_ != null){
                cam_.SetActive(false);
            }
        }

        camera_preview_loja[posi_].SetActive(true);

        foreach (GameObject prev_ in preview_loja)
        {
            if(prev_ != null){
                prev_.SetActive(false);
            }
        }

        preview_loja[posi_].SetActive(true);



        if(posi_ == 1){
            moveis_atuais = gerDados.instancia.dados_.moveis;
        }

        textoLoja[0].text = "";
        textoLoja[1].text = "";

        textoLoja[2].text = "";
        textoLoja[3].text = "";

        textoLoja[4].text = "";
        textoLoja[5].text = "";

        botao_loja_comprar.interactable = false;

        for ( int i= lista_loja.transform.childCount-1; i>=0; --i )
        {
            GameObject child = lista_loja.transform.GetChild(i).gameObject;
            Destroy( child );
        }

        if(posi_ < 10){
            
            foreach(item_pintura_movel item_ in gerMoveis.instancia.itens_pintura_movel)
            {
                if(item_ != null){

                    if(item_.listado && item_.id > 11 && !gerDados.instancia.temItem(item_.id)){

                        GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)item_.raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id, posi_));

                        item_.id_ordem = item_.id;
                    }

                }
            }
            
        }else{
            foreach(item_movel item_ in gerMoveis.instancia.itens_movel)
            {
                if(item_ != null){
                    if((int)item_.posicao == posi_ && item_.listado){
                        GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)item_.raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id, (int) item_.posicao));
                    }
                }
            }
        }
    }
}