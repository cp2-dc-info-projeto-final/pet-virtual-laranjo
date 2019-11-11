using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class gerLojaCasas : MonoBehaviour
{

    public int item_selecionado = -1;

    public GameObject lista_loja;
    public Button botao_loja_confirmar_compra_moeda, botao_loja_comprar;
    public TextMeshProUGUI[] textoLoja;
    public GameObject menu_confirmar_compra, menu_confirmar_compra_pivot, menu_comprar_moedas, menu_comprar_dolares, preview_loja, camera_preview_loja;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botaoGaramgem(int id_){
        if(id_ == 101){
            textoLoja[0].text = gerenciador.instancia.itemCasaDeId(id_).nome[gerDados.instancia.dados_.lingua];
            textoLoja[1].text = gerenciador.instancia.itemCasaDeId(id_).descricao[gerDados.instancia.dados_.lingua];

            textoLoja[2].text = "";
            textoLoja[3].text = gerenciador.instancia.itemCasaDeId(id_).preco_dolares.ToString();

            textoLoja[4].text = "";
            textoLoja[5].text = gerenciador.instancia.itemCasaDeId(id_).preco_dolares.ToString();

            item_selecionado = id_;

            botao_loja_comprar.interactable = true;

            botao_loja_confirmar_compra_moeda.interactable = false;

            gerenciador.instancia.instanciar_casa(2,true);
        }

        if(id_ == 102){
            textoLoja[0].text = gerenciador.instancia.itemCasaDeId(id_).nome[gerDados.instancia.dados_.lingua];
            textoLoja[1].text = gerenciador.instancia.itemCasaDeId(id_).descricao[gerDados.instancia.dados_.lingua];

            textoLoja[2].text = "";
            textoLoja[3].text = gerenciador.instancia.itemCasaDeId(id_).preco_dolares.ToString();

            textoLoja[4].text = "";
            textoLoja[5].text = gerenciador.instancia.itemCasaDeId(id_).preco_dolares.ToString();

            item_selecionado = id_;

            if(gerDados.instancia.dados_.quant_gar < 2){
                botao_loja_comprar.interactable = false;
            }else
            {
                botao_loja_comprar.interactable = true;
            }

            gerenciador.instancia.instanciar_casa(3,true);

            botao_loja_confirmar_compra_moeda.interactable = false;

            
        }

        if(id_ == 103){
            textoLoja[0].text = gerenciador.instancia.itemCasaDeId(id_).nome[gerDados.instancia.dados_.lingua];
            textoLoja[1].text = gerenciador.instancia.itemCasaDeId(id_).descricao[gerDados.instancia.dados_.lingua];

            textoLoja[2].text = "";
            textoLoja[3].text = gerenciador.instancia.itemCasaDeId(id_).preco_dolares.ToString();

            textoLoja[4].text = "";
            textoLoja[5].text = gerenciador.instancia.itemCasaDeId(id_).preco_dolares.ToString();

            item_selecionado = id_;

            botao_loja_comprar.interactable = true;

            gerenciador.instancia.instanciar_casa(3,true);

            botao_loja_confirmar_compra_moeda.interactable = false;

            
        }
    }

    public void botaoLoja(int id_){

        textoLoja[0].text = gerenciador.instancia.itemCasaDeId(id_).nome[gerDados.instancia.dados_.lingua];
        textoLoja[1].text = gerenciador.instancia.itemCasaDeId(id_).descricao[gerDados.instancia.dados_.lingua];

        textoLoja[2].text = gerenciador.instancia.itemCasaDeId(id_).preco_moedas.ToString();
        textoLoja[3].text = gerenciador.instancia.itemCasaDeId(id_).preco_dolares.ToString();

        textoLoja[4].text = gerenciador.instancia.itemCasaDeId(id_).preco_moedas.ToString();
        textoLoja[5].text = gerenciador.instancia.itemCasaDeId(id_).preco_dolares.ToString();

        item_selecionado = id_;

        botao_loja_comprar.interactable = true;

        botao_loja_confirmar_compra_moeda.interactable = true;

        gerenciador.instancia.instanciar_casa(id_);
    }

    public void botaoConfirmarCompra(int opcao_){
        if(opcao_ == 1){
            //compra em moedas

            if(gerDados.instancia.dados_.moedas >= gerenciador.instancia.itemCasaDeId(item_selecionado).preco_moedas){

                gerDados.instancia.dados_.id_casa = item_selecionado;

                gerDados.instancia.dados_.moedas -= gerenciador.instancia.itemCasaDeId(item_selecionado).preco_moedas;

                gerDados.instancia.salvar(true);

                gerarloja(0);

                menu_confirmar_compra.GetComponent<animar_UI>().mostrar_ocultar();

                menu_confirmar_compra_pivot.SetActive(false);

            }else
            {

                menu_comprar_moedas.GetComponent<animar_UI>().mostrar_ocultar();
            }
        }

        if(opcao_ == 2){
            //compra em dolares

            if(gerDados.instancia.dados_.dolares >= gerenciador.instancia.itemCasaDeId(item_selecionado).preco_dolares){

                if(item_selecionado < 101 || item_selecionado > 103){

                    gerDados.instancia.dados_.id_casa = item_selecionado;

                    gerDados.instancia.dados_.dolares -= gerenciador.instancia.itemCasaDeId(item_selecionado).preco_dolares;

                    gerDados.instancia.salvar(true);

                    gerarloja(0);

                    menu_confirmar_compra.GetComponent<animar_UI>().mostrar_ocultar();

                    menu_confirmar_compra_pivot.SetActive(false);

                }else
                {
                    if(item_selecionado == 101){

                        gerDados.instancia.dados_.quant_gar = 2;
                        
                        gerDados.instancia.dados_.dolares -= gerenciador.instancia.itemCasaDeId(item_selecionado).preco_dolares;

                        gerDados.instancia.salvar(true);

                        gerarloja(0);

                        menu_confirmar_compra.GetComponent<animar_UI>().mostrar_ocultar();

                        menu_confirmar_compra_pivot.SetActive(false);
                    }

                    if(item_selecionado == 102){

                        gerDados.instancia.dados_.quant_gar = 3;
                        
                        gerDados.instancia.dados_.dolares -= gerenciador.instancia.itemCasaDeId(item_selecionado).preco_dolares;

                        gerDados.instancia.salvar(true);

                        gerarloja(0);

                        menu_confirmar_compra.GetComponent<animar_UI>().mostrar_ocultar();

                        menu_confirmar_compra_pivot.SetActive(false);
                    }

                    if(item_selecionado == 103){

                        gerDados.instancia.dados_.quant_gar = 3;
                        
                        gerDados.instancia.dados_.dolares -= gerenciador.instancia.itemCasaDeId(item_selecionado).preco_dolares;

                        gerDados.instancia.salvar(true);

                        gerarloja(0);

                        menu_confirmar_compra.GetComponent<animar_UI>().mostrar_ocultar();

                        menu_confirmar_compra_pivot.SetActive(false);
                    }
                }

            }else
            {

                menu_comprar_dolares.GetComponent<animar_UI>().mostrar_ocultar();
            }


        }
    }

    public void botaoAbrirLoja(){
        gerenciador.instancia.casa_quintal.SetActive(true);
    }

    public void botaoFecharLoja(){
        gerenciador.instancia.instanciar_casa();
        gerenciador.instancia.casa_quintal.SetActive(false);

        preview_loja.SetActive(false);
        camera_preview_loja.SetActive(false);
    }

    public void gerarloja(int posi_){

        /*
        for(int i = 0; i < lista_loja.transform.childCount;i++){
            Destroy(lista_loja.transform.GetChild(i));
        }
        */

        preview_loja.SetActive(true);
        camera_preview_loja.SetActive(true);

        

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

        if(posi_ == 0){
            gerenciador.instancia.instanciar_casa();
            
            foreach(item_casa item_ in gerenciador.instancia.casas)
            {
                if(item_ != null){

                    if(item_.listado && gerDados.instancia.dados_.id_casa != item_.id && (item_.id < 101 || item_.id > 103)){

                        GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)item_.raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id));

                        item_.id_ordem = item_.id;
                    }

                }
            }
            
        }else{
            if(posi_ != 3){

            
                foreach(item_casa item_ in gerenciador.instancia.casas)
                {
                    if(item_ != null){
                        if((int)item_.tipo == posi_ && item_.listado && gerDados.instancia.dados_.id_casa != item_.id){
                            GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                            botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)item_.raridade];
                            botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                            botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id));

                            if(posi_ == 3){
                               
                            }
                            
                        }
                    }
                }
            }else{
                if(gerDados.instancia.dados_.quant_gar == 1){

                        GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)gerenciador.instancia.itemCasaDeId(101).raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = gerenciador.instancia.itemCasaDeId(101).imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoGaramgem(101));
                        
                        botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)gerenciador.instancia.itemCasaDeId(102).raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = gerenciador.instancia.itemCasaDeId(102).imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoGaramgem(102));

                        botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)gerenciador.instancia.itemCasaDeId(103).raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = gerenciador.instancia.itemCasaDeId(103).imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoGaramgem(103));
                }

                if(gerDados.instancia.dados_.quant_gar == 2){
                        GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)gerenciador.instancia.itemCasaDeId(102).raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = gerenciador.instancia.itemCasaDeId(102).imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoGaramgem(102));
                }
            }
        }
    }

}
