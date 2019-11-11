using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class gerLojaAutopecas : MonoBehaviour
{
    public carro_dados carro_atual;
    public bool tem_carro = true;
    public int indice_carro_atual;
    public GameObject pivot_carro_loja;
    public int item_selecionado = -1, item_tipo_selecionado = -1;

    public GameObject lista_loja;
    public Button botao_loja_confirmar_compra_moeda, botao_loja_comprar;
    public Button[] botao_indice;
    public TextMeshProUGUI[] textoLoja;
    public GameObject chas_, menu_confirmar_compra, menu_confirmar_compra_pivot, menu_comprar_moedas, menu_comprar_dolares;
    public GameObject[] preview_loja, camera_preview_loja;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instanciarCarro(){
        //Debug.Log("tik...");

        if(tem_carro){

            //Debug.Log("...tok");
            
            //car_ = Instantiate(carro_base, casa_pivot[i_].transform.position + new Vector3(0,0.35f,-2), Quaternion.Euler(0,0,0));

            //car_.transform.SetParent(casa_quintal.transform);

            //car_.name = "carro_0" + (i_);
            //Debug.Log("INSTANCIADO O CARRO LAH OH! id: " + i_);

            if(chas_ != null){
                Destroy(chas_);
            }

            chas_ = Instantiate(gerenciador.instancia.ChassiDeId(carro_atual.id_chassi).prefab,pivot_carro_loja.transform);
            chas_.layer = 9;

            //Debug.Log("-  -  -" + i_);

            //car_.GetComponent<veiculo>().entrada = chas_.GetComponent<chassi>().entradas;
            //car_.GetComponent<veiculo>().coll_roda = chas_.GetComponent<chassi>().rodas_coll;

            
            // intanciar rodas


            for (int rodaid_ = 0; rodaid_ < chas_.GetComponent<chassi>().rodas_tranf_.Length; rodaid_++)
            {
                GameObject rod_;
                if( rodaid_ % 2 == 0){
                    rod_ = Instantiate(gerenciador.instancia.RodaDeId(carro_atual.acessorios[4]).prefab,chas_.GetComponent<chassi>().rodas_tranf_[rodaid_].transform);
                }else
                {
                    rod_ = Instantiate(gerenciador.instancia.RodaDeId(carro_atual.acessorios[4]).prefab_2,chas_.GetComponent<chassi>().rodas_tranf_[rodaid_].transform);
                }

                //car_.GetComponent<veiculo>().transf_roda[rodaid_] = rod_.transform;

            }

            //instanciar carroceria, airvent teto, airvent capo e aerofolio

            GameObject obj_chassi_ = null;
            
            if (gerenciador.instancia.itemCarroDeId(carro_atual.acessorios[0],PosicaoItemCarro.Carroceria,carro_atual.id_chassi).prefab == null)
            {
                Debug.Log("CHASSI NULL---------------------------------------------");
            }else
            {
                if (chas_.GetComponent<chassi>().acessorios[0].transform == null)
                {
                    Debug.Log("CHASSI PARENT NULL---------------------------------------------");
                }else
                {
                    obj_chassi_ = Instantiate(gerenciador.instancia.itemCarroDeId(carro_atual.acessorios[0],PosicaoItemCarro.Carroceria,carro_atual.id_chassi).prefab,chas_.GetComponent<chassi>().acessorios[0].transform);
                }
            }
            
            Instantiate(gerenciador.instancia.itemCarroDeId(carro_atual.acessorios[1],PosicaoItemCarro.ArTeto).prefab,chas_.GetComponent<chassi>().acessorios[1].transform);
            Instantiate(gerenciador.instancia.itemCarroDeId(carro_atual.acessorios[2],PosicaoItemCarro.ArCapo).prefab,chas_.GetComponent<chassi>().acessorios[2].transform);
            Instantiate(gerenciador.instancia.itemCarroDeId(carro_atual.acessorios[3],PosicaoItemCarro.Aerofolio).prefab,chas_.GetComponent<chassi>().acessorios[3].transform);

            Material[] tempMats_ = obj_chassi_.GetComponent<MeshRenderer>().materials;



            tempMats_[0] = gerenciador.instancia.itemCorDeId(carro_atual.cor_id[0]).material;
            tempMats_[1] = gerenciador.instancia.itemCorDeId(carro_atual.cor_id[1]).material;

            obj_chassi_.GetComponent<MeshRenderer>().materials = tempMats_;


            foreach(GameObject porta_ in chas_.GetComponent<chassi>().portas){

                tempMats_ = porta_.GetComponent<MeshRenderer>().materials;

                tempMats_[0] = gerenciador.instancia.itemCorDeId(carro_atual.cor_id[0]).material;
                tempMats_[1] = gerenciador.instancia.itemCorDeId(carro_atual.cor_id[1]).material;

                porta_.GetComponent<MeshRenderer>().materials = tempMats_;
            }
            
            
            foreach(Transform tranf_ in chas_.transform.GetComponentsInChildren<Transform>()){
                tranf_.gameObject.layer = 9;
            }
        }else
        {
            if(chas_ != null){
                Destroy(chas_);
            }
        }
        
        
    
    }
    public void instanciarChassi(int id_chassi_){
        //Debug.Log("tik...");

            //Debug.Log("...tok");
            
            //car_ = Instantiate(carro_base, casa_pivot[i_].transform.position + new Vector3(0,0.35f,-2), Quaternion.Euler(0,0,0));

            //car_.transform.SetParent(casa_quintal.transform);

            //car_.name = "carro_0" + (i_);
            //Debug.Log("INSTANCIADO O CARRO LAH OH! id: " + i_);

            if(chas_ != null){
                Destroy(chas_);
            }

            chas_ = Instantiate(gerenciador.instancia.ChassiDeId(id_chassi_).prefab,pivot_carro_loja.transform);
            chas_.layer = 9;

            //Debug.Log("-  -  -" + i_);

            //car_.GetComponent<veiculo>().entrada = chas_.GetComponent<chassi>().entradas;
            //car_.GetComponent<veiculo>().coll_roda = chas_.GetComponent<chassi>().rodas_coll;

            
            // intanciar rodas


            for (int rodaid_ = 0; rodaid_ < chas_.GetComponent<chassi>().rodas_tranf_.Length; rodaid_++)
            {
                GameObject rod_;
                if( rodaid_ % 2 == 0){
                    rod_ = Instantiate(gerenciador.instancia.RodaDeId(new carro_dados().acessorios[4]).prefab,chas_.GetComponent<chassi>().rodas_tranf_[rodaid_].transform);
                }else
                {
                    rod_ = Instantiate(gerenciador.instancia.RodaDeId(new carro_dados().acessorios[4]).prefab_2,chas_.GetComponent<chassi>().rodas_tranf_[rodaid_].transform);
                }

                //car_.GetComponent<veiculo>().transf_roda[rodaid_] = rod_.transform;

            }

            //instanciar carroceria, airvent teto, airvent capo e aerofolio

            GameObject obj_chassi_ = null;
            
            if (gerenciador.instancia.itemCarroDeId(0,PosicaoItemCarro.Carroceria,id_chassi_).prefab == null)
            {
                Debug.Log("CHASSI NULL---------------------------------------------");
            }else
            {
                if (chas_.GetComponent<chassi>().acessorios[0].transform == null)
                {
                    Debug.Log("CHASSI PARENT NULL---------------------------------------------");
                }else
                {
                    obj_chassi_ = Instantiate(gerenciador.instancia.itemCarroDeId(0,PosicaoItemCarro.Carroceria,id_chassi_).prefab,chas_.GetComponent<chassi>().acessorios[0].transform);
                }
            }

            Material[] tempMats_ = obj_chassi_.GetComponent<MeshRenderer>().materials;



            tempMats_[0] = gerenciador.instancia.itemCorDeId(new carro_dados().cor_id[0]).material;
            tempMats_[1] = gerenciador.instancia.itemCorDeId(new carro_dados().cor_id[1]).material;

            obj_chassi_.GetComponent<MeshRenderer>().materials = tempMats_;


            foreach(GameObject porta_ in chas_.GetComponent<chassi>().portas){

                tempMats_ = porta_.GetComponent<MeshRenderer>().materials;

                tempMats_[0] = gerenciador.instancia.itemCorDeId(new carro_dados().cor_id[0]).material;
                tempMats_[1] = gerenciador.instancia.itemCorDeId(new carro_dados().cor_id[1]).material;

                porta_.GetComponent<MeshRenderer>().materials = tempMats_;
            }
            
            
            foreach(Transform tranf_ in chas_.transform.GetComponentsInChildren<Transform>()){
                tranf_.gameObject.layer = 9;
            }
        
        
    
    }

    public void botaoIndice(int i_){
        indice_carro_atual = i_;

        if(gerDados.instancia.dados_.carro[i_] != null){
            carro_atual = gerDados.instancia.dados_.carro[i_].CloneProfundo();
            tem_carro = true;
        }else
        {
            carro_atual = null;
            tem_carro = false;
        }
        
        instanciarCarro();

        gerarloja(0);
    }

    /*public void botaoGaramgem(int id_){
        if(id_ == 101){
            textoLoja[0].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).nome[gerDados.instancia.dados_.lingua];
            textoLoja[1].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).descricao[gerDados.instancia.dados_.lingua];

            textoLoja[2].text = "";
            textoLoja[3].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_dolares.ToString();

            textoLoja[4].text = "";
            textoLoja[5].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_dolares.ToString();

            item_selecionado = id_;

            botao_loja_comprar.interactable = true;

            botao_loja_confirmar_compra_moeda.interactable = false;

            gerenciador.instancia.instanciar_casa(2,true);
        }

        if(id_ == 102){
            textoLoja[0].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).nome[gerDados.instancia.dados_.lingua];
            textoLoja[1].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).descricao[gerDados.instancia.dados_.lingua];

            textoLoja[2].text = "";
            textoLoja[3].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_dolares.ToString();

            textoLoja[4].text = "";
            textoLoja[5].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_dolares.ToString();

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
            textoLoja[0].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).nome[gerDados.instancia.dados_.lingua];
            textoLoja[1].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).descricao[gerDados.instancia.dados_.lingua];

            textoLoja[2].text = "";
            textoLoja[3].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_dolares.ToString();

            textoLoja[4].text = "";
            textoLoja[5].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_dolares.ToString();

            item_selecionado = id_;

            botao_loja_comprar.interactable = true;

            gerenciador.instancia.instanciar_casa(3,true);

            botao_loja_confirmar_compra_moeda.interactable = false;

            
        }
    }*/

    public void botaoLoja(int id_, int tipo_){

        textoLoja[0].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).nome[gerDados.instancia.dados_.lingua];
        textoLoja[1].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).descricao[gerDados.instancia.dados_.lingua];

        textoLoja[2].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_moedas.ToString();
        textoLoja[3].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_dolares.ToString();

        textoLoja[4].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_moedas.ToString();
        textoLoja[5].text = gerenciador.instancia.itemCarroDeId(id_, (PosicaoItemCarro)tipo_).preco_dolares.ToString();

        item_selecionado = id_;
        item_tipo_selecionado = tipo_;

        botao_loja_comprar.interactable = true;

        botao_loja_confirmar_compra_moeda.interactable = true;

        if(tipo_ == 0){
            if(tem_carro){
                carro_atual.id_chassi = id_;
                carro_atual.acessorios[0] = 0;
            }else
            {
                instanciarChassi(id_);
            }
            
        }

        if(tipo_ == 1){
            carro_atual.acessorios[4] = id_;
        }

        if(tipo_ == 2){
            carro_atual.acessorios[2] = id_;
        }

        if(tipo_ == 3){
            carro_atual.acessorios[1] = id_;
        }

        if(tipo_ == 4){
            carro_atual.acessorios[0] = id_;
        }

        if(tipo_ == 5){
            carro_atual.acessorios[3] = id_;
        }

        if(tem_carro){
            instanciarCarro();
        }

        

        //gerenciador.instancia.instanciar_casa(id_);
    }

    public void botaoConfirmarCompra(int opcao_){
        if(opcao_ == 1){
            //compra em moedas

            if(gerDados.instancia.dados_.moedas >= gerenciador.instancia.itemCarroDeId(item_selecionado,(PosicaoItemCarro)item_tipo_selecionado).preco_moedas){

                if(item_tipo_selecionado == 0){
                    if(tem_carro){

                        gerDados.instancia.dados_.carro[indice_carro_atual].id_chassi = item_selecionado;
                        gerDados.instancia.dados_.carro[indice_carro_atual].acessorios = new int[4]{0,0,0,1};
                        gerDados.instancia.dados_.carro[indice_carro_atual].nivel = new int[4]{1,1,1,1};

                    }else{

                        gerDados.instancia.dados_.carro[indice_carro_atual] = new carro_dados();
                        gerDados.instancia.dados_.carro[indice_carro_atual].acessorios = new int[4]{0,0,0,1};
                        gerDados.instancia.dados_.carro[indice_carro_atual].nivel = new int[4]{1,1,1,1};

                    }
                    
                }

                if(item_tipo_selecionado == 1){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[4] = item_selecionado;
                }

                if(item_tipo_selecionado == 2){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[2] = item_selecionado;
                }

                if(item_tipo_selecionado == 3){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[1] = item_selecionado;
                }

                if(item_tipo_selecionado == 4){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[0] = item_selecionado;
                }

                if(item_tipo_selecionado == 5){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[3] = item_selecionado;
                }

                gerDados.instancia.dados_.moedas -= gerenciador.instancia.itemCarroDeId(item_selecionado,(PosicaoItemCarro)item_tipo_selecionado).preco_moedas;

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

            if(gerDados.instancia.dados_.dolares >= gerenciador.instancia.itemCarroDeId(item_selecionado,(PosicaoItemCarro)item_tipo_selecionado).preco_dolares){

                if(item_tipo_selecionado == 0){
                    if(tem_carro){

                        gerDados.instancia.dados_.carro[indice_carro_atual].id_chassi = item_selecionado;
                        gerDados.instancia.dados_.carro[indice_carro_atual].acessorios = new int[4]{0,0,0,1};
                        gerDados.instancia.dados_.carro[indice_carro_atual].nivel = new int[4]{1,1,1,1};

                    }else{

                        gerDados.instancia.dados_.carro[indice_carro_atual] = new carro_dados();
                        gerDados.instancia.dados_.carro[indice_carro_atual].acessorios = new int[4]{0,0,0,1};
                        gerDados.instancia.dados_.carro[indice_carro_atual].nivel = new int[4]{1,1,1,1};

                    }
                    
                }

                if(item_tipo_selecionado == 1){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[4] = item_selecionado;
                }

                if(item_tipo_selecionado == 2){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[2] = item_selecionado;
                }

                if(item_tipo_selecionado == 3){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[1] = item_selecionado;
                }

                if(item_tipo_selecionado == 4){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[0] = item_selecionado;
                }

                if(item_tipo_selecionado == 5){
                    gerDados.instancia.dados_.carro[indice_carro_atual].acessorios[3] = item_selecionado;
                }

                gerDados.instancia.dados_.dolares -= gerenciador.instancia.itemCarroDeId(item_selecionado,(PosicaoItemCarro)item_tipo_selecionado).preco_dolares;

                gerDados.instancia.salvar(true);

                gerarloja(0);

                menu_confirmar_compra.GetComponent<animar_UI>().mostrar_ocultar();

                menu_confirmar_compra_pivot.SetActive(false);

                

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

        foreach(GameObject prev_ in preview_loja){
            prev_.SetActive(false);
        }

        foreach(GameObject prev_ in camera_preview_loja){
            prev_.SetActive(false);
        }

    }

    public void gerarloja(int posi_){

        foreach(Button bot_ in botao_indice){
            bot_.interactable = false;
        }

        for(int i_ = 0; i_ < gerDados.instancia.dados_.quant_gar; i_++){
            botao_indice[i_].interactable = true;
        }

        /*
        for(int i = 0; i < lista_loja.transform.childCount;i++){
            Destroy(lista_loja.transform.GetChild(i));
        }
        */

        foreach(GameObject prev_ in preview_loja){
            prev_.SetActive(false);
        }

        preview_loja[posi_].SetActive(true);

        foreach(GameObject prev_ in camera_preview_loja){
            prev_.SetActive(false);
        }

        camera_preview_loja[posi_].SetActive(true);

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

            if(tem_carro){
                carro_atual = gerDados.instancia.dados_.carro[indice_carro_atual].CloneProfundo();
            }else
            {
                carro_atual = null;
            }
            
            
            /*
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
            */
        }else
        {
            if(tem_carro){
                carro_atual.id_chassi = gerDados.instancia.dados_.carro[indice_carro_atual].CloneProfundo().id_chassi;
            }
            
        }

        if(tem_carro){
            Debug.Log("TEM CARRO");
            foreach(item_carro item_ in gerenciador.instancia.itens_carro){
                if(item_ != null){

                    if(item_.listado && (int)item_.posicao == posi_){

                        if(posi_ != 4){
                            GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                            botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)item_.raridade];
                            botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                            botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id, (int)item_.posicao));
                        }else
                        {
                            item_carroceria item__ = item_ as item_carroceria;
                            if(item__.id_chassi == carro_atual.id_chassi){

                                GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                                botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)item_.raridade];
                                botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                                botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id, (int)item_.posicao));

                            }
                            
                        }

                        
                    }

                }
            }
        }else
        {
            Debug.Log("CARRO NULO");
            foreach(item_carro item_ in gerenciador.instancia.itens_carro){
                if(item_ != null){

                    if(item_.listado && (int)item_.posicao == posi_ && posi_ == 0){

                        GameObject botao_ = Instantiate(gerenciador.instancia.prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = gerenciador.instancia.raridade[(int)item_.raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id, (int)item_.posicao));
                        
                    }
                }
            }
        }

        
        
        
        /*else{
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
        }*/

        instanciarCarro();
    }

}
