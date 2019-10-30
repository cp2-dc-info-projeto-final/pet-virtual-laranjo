﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class gerenciador : MonoBehaviour
{
    public static gerenciador instancia;
    public string ambiente;
    public GameObject casa_interior, casa_quintal,spawn_fora,spawn_dentro, laranjo,laranjo_preview, prefab_botao, lista_loja, lista_armario;
    public GameObject[] cameras, carros, UIs_casa, UIs_fora;

    public TextMeshProUGUI[] textoLoja = new TextMeshProUGUI[3], textoArmario = new TextMeshProUGUI[3];
    public List<item> itens;

    public Material mat_lar_0, mat_lar_meio, mat_lar_1, mat_lar_final;

    public float nivel_lar = 0;
    public Slider slider_nivel;
    public Image[] cor_nivel;
    public Sprite[] raridade;

    public int render_area = 600;


    public GameObject primeiro_terreno;
    public peca_terreno[] pecas;
    public List<terreno> todosTerrenos;
    public List<int> ter_canto0, ter_canto1;
    public List<item_casa> casas;
    public GameObject[] casa_pivot;
    public GameObject carro_base;
    public List<item_carro> itens_carro;
    public List<item_cor> itens_cor;

    public GameObject rank_peca, rank_piv;

    public GameObject SHOWSHOW;
    public item_carro SHOWSHOWIC;

    //public List<int> lista1 = new List<int>(), lista2 = new List<int>(), lista3 = new List<int>();

    private void Awake() {
        instancia = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        

        
        


        restartTerrenos();

        instanciar_casa();
        for(int i_ = 1; i_ <= 3; i_++){
            instanciar_carro(i_);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //lista3 = lista1.Intersect(lista2).ToList();
        /*
        if(ambiente == "dentro"){
            laranjo.GetComponent<movimento>().cam_ = GameObject.Find("camera_dentro_1").GetComponent<Camera>();
        }

        if(ambiente == "fora"){
            laranjo.GetComponent<movimento>().cam_ = GameObject.Find("camera_fora_1").GetComponent<Camera>();
        }

        if(ambiente == "carro"){
            //GameObject.Find("camera_carro") .GetComponent<movimento>().cam_ = GameObject.Find("camera_fora_1").GetComponent<Camera>();
        }
        */

        if(gerDados.instancia.dados_.nivel < 0.5f){

            mat_lar_final.color = Color.Lerp(mat_lar_0.color,mat_lar_meio.color,gerDados.instancia.dados_.nivel * 2);

        }else
        {
            mat_lar_final.color = Color.Lerp(mat_lar_meio.color,mat_lar_1.color,(gerDados.instancia.dados_.nivel - 0.5f) * 2);
        }
        
        slider_nivel.value = gerDados.instancia.dados_.nivel;

        foreach(Image cor_ in cor_nivel){
            cor_.color = mat_lar_1.color;
        }
    }

    public void colocaritemLoja(int id_){
        
        laranjo_preview.GetComponent<design>().MudarMesh(itemDeId(id_));
        
        if((int)itemDeId(id_).posicao == 5){
            laranjo_preview.GetComponent<Animator>().SetBool("item",itemDeId(id_).seguraItem);
        }
    }

    public void colocaritemArmario(int id_){
        for(int i = 0; i < itens.Count - 1; i++){
            if(itens[i] != null){
                if(itens[i].id == id_){
                    laranjo_preview.GetComponent<design>().MudarMesh(itens[i]);
                    laranjo.GetComponent<design>().MudarMesh(itens[i]);

                    if((int)itens[i].posicao == 5){
                        laranjo_preview.GetComponent<Animator>().SetBool("item",itens[i].seguraItem);
                        laranjo.GetComponent<Animator>().SetBool("item",itens[i].seguraItem);
                    }
                }
            }
        }
    }

    public void botaoArmario(int id_){
        textoArmario[0].text = itemDeId(id_).nome[gerDados.instancia.dados_.lingua];
        textoArmario[1].text = itemDeId(id_).descricao[gerDados.instancia.dados_.lingua];

        colocaritemArmario(id_);
    }

    public void botaoLoja(int id_){
        textoLoja[0].text = itemDeId(id_).nome[gerDados.instancia.dados_.lingua];
        textoLoja[1].text = itemDeId(id_).descricao[gerDados.instancia.dados_.lingua];
        textoLoja[2].text = itemDeId(id_).preco.ToString();

        colocaritemLoja(id_);
    }

    public void gerarloja(int posi_){

        /*
        for(int i = 0; i < lista_loja.transform.childCount;i++){
            Destroy(lista_loja.transform.GetChild(i));
        }
        */

        for ( int i= lista_loja.transform.childCount-1; i>=0; --i )
        {
            GameObject child = lista_loja.transform.GetChild(i).gameObject;
            Destroy( child );
        }

        if(posi_ == 0){
            foreach(item item_ in itens)
            {
                if(item_ != null){
                    
                    

                    GameObject botao_ = Instantiate(prefab_botao,lista_loja.transform);
                    botao_.GetComponent<Image>().sprite = raridade[(int)item_.raridade];
                    botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                    botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id));

                    item_.id_ordem = item_.id;
                }
            }
            
        }else{
            foreach(item item_ in itens)
            {
                if(item_ != null){
                    if((int)item_.posicao == posi_){
                        GameObject botao_ = Instantiate(prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = raridade[(int)item_.raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id));
                    }
                }
            }
        }
    }

    public void gerarArmario(int posi_){

        /*
        for(int i = 0; i < lista_loja.transform.childCount;i++){
            Destroy(lista_loja.transform.GetChild(i));
        }
        */

        for ( int i= lista_armario.transform.childCount-1; i>=0; --i )
        {
            GameObject child = lista_armario.transform.GetChild(i).gameObject;
            Destroy( child );
        }

        if(posi_ == 0){
            foreach(item item_ in itens)
            {
                if(item_ != null){
                    
                    if(gerDados.instancia.temItem(item_.id)){

                        GameObject botao_ = Instantiate(prefab_botao,lista_armario.transform);
                        botao_.GetComponent<Image>().sprite = raridade[(int)item_.raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoArmario(item_.id));

                        //item_.id_ordem = item_.id;
                    }
                }
            }
            
        }else{
            foreach(item item_ in itens)
            {
                if(item_ != null){
                    if((int)item_.posicao == posi_){
                        if(gerDados.instancia.temItem(item_.id)){

                        GameObject botao_ = Instantiate(prefab_botao,lista_armario.transform);
                        botao_.GetComponent<Image>().sprite = raridade[(int)item_.raridade];
                        botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                        botao_.GetComponent<Button>().onClick.AddListener(() => botaoArmario(item_.id));
                    
                        }
                    }
                }
            }
        }
    }

    public item itemDeId(int id_){
        item it_ = new item();
        foreach(item item_ in itens){
            if(item_ != null){
                if(item_.id == id_){
                    it_ = item_;
                }
            }
        }
        return it_;
    }

    public void casa_entrar(GameObject lar_){
        casa_interior.SetActive(true);
        casa_quintal.SetActive(false);

        lar_.transform.position = spawn_dentro.transform.position;

        lar_.GetComponent<movimento>().destino = spawn_dentro;

        mudar_camera("camera_casa");

        
        //ambiente = "dentro";

        

        foreach(GameObject ui_ in UIs_fora){
            ui_.SetActive(false);
        }

        foreach(GameObject ui_ in UIs_casa){
            ui_.SetActive(true);
        }

        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void casa_sair(GameObject lar_){
        casa_interior.SetActive(false);
        casa_quintal.SetActive(true);

        lar_.transform.position = spawn_fora.transform.position;

        lar_.GetComponent<movimento>().destino = spawn_fora;

        mudar_camera("camera_fora");

        //ambiente = "fora";

        foreach(GameObject ui_ in UIs_casa){
            ui_.SetActive(false);
        }

        foreach(GameObject ui_ in UIs_fora){
            ui_.SetActive(true);
        }

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void mudar_camera(string nome_cam_){

        foreach(GameObject camera_ in cameras){
            if(camera_.name != nome_cam_){
                camera_.SetActive(false);
            }else
            {
                camera_.SetActive(true);
                laranjo.GetComponent<movimento>().cam_ = camera_.GetComponentInChildren<Camera>();
            }
            
        }

    }

    public terreno pegar_terreno(int x_, int z_){

        terreno terreno_ = null;
        foreach(terreno ter_ in todosTerrenos){
            if(ter_.posX == x_ && ter_.posZ == z_){
                terreno_ = ter_;
            }
        }

        return terreno_;
    }

    //                                                                 -------------------------------------Instacia as casas do save---------------------------
    public void instanciar_casa(){
        Instantiate(CasaDeId(gerDados.instancia.dados_.id_casa).prefab_casa, casa_pivot[0].transform);

        for(int i_ = 2; i_ <= gerDados.instancia.dados_.quant_gar; i_++){
            Instantiate(CasaDeId(gerDados.instancia.dados_.id_casa).prefab_garagem, casa_pivot[i_].transform);
        }
    }

    public void instanciar_carro(int i_){
        GameObject car_, chas_;
        if(gerDados.instancia.dados_.carro[i_] != null){

            
            car_ = Instantiate(carro_base, casa_pivot[i_].transform.position + new Vector3(0,0.35f,-2), Quaternion.Euler(0,0,0));

            car_.transform.SetParent(casa_quintal.transform);

            car_.name = "carro_0" + (i_);
            Debug.Log("INSTANCIADO O CARRO LAH OH! id: " + i_);

            chas_ = Instantiate(ChassiDeId(gerDados.instancia.dados_.carro[i_].id_chassi).prefab,car_.transform);

            Debug.Log("-  -  -" + i_);

            car_.GetComponent<veiculo>().entrada = chas_.GetComponent<chassi>().entradas;
            car_.GetComponent<veiculo>().coll_roda = chas_.GetComponent<chassi>().rodas_coll;

            
            // intanciar rodas


            for (int rodaid_ = 0; rodaid_ < car_.GetComponent<veiculo>().transf_roda.Length; rodaid_++)
            {
                GameObject rod_;
                if( rodaid_ % 2 == 0){
                    rod_ = Instantiate(RodaDeId(gerDados.instancia.dados_.carro[i_].acessorios[4]).prefab,chas_.GetComponent<chassi>().rodas_tranf_[rodaid_].transform);
                }else
                {
                    rod_ = Instantiate(RodaDeId(gerDados.instancia.dados_.carro[i_].acessorios[4]).prefab_2,chas_.GetComponent<chassi>().rodas_tranf_[rodaid_].transform);
                }

                car_.GetComponent<veiculo>().transf_roda[rodaid_] = rod_.transform;

            }
            

            SHOWSHOW = chas_.GetComponent<chassi>().acessorios[3];

            //instanciar carroceria, airvent teto, airvent capo e aerofolio

            GameObject obj_chassi_ = null;
            
            if (itemCarroDeId(gerDados.instancia.dados_.carro[i_].acessorios[0],PosicaoItemCarro.Carroceria,gerDados.instancia.dados_.carro[i_].id_chassi).prefab == null)
            {
                Debug.Log("CHASSI NULL---------------------------------------------");
            }else
            {
                if (chas_.GetComponent<chassi>().acessorios[0].transform == null)
                {
                    Debug.Log("CHASSI PARENT NULL---------------------------------------------");
                }else
                {
                    obj_chassi_ = Instantiate(itemCarroDeId(gerDados.instancia.dados_.carro[i_].acessorios[0],PosicaoItemCarro.Carroceria,gerDados.instancia.dados_.carro[i_].id_chassi).prefab,chas_.GetComponent<chassi>().acessorios[0].transform);
                }
            }
            
            Instantiate(itemCarroDeId(gerDados.instancia.dados_.carro[i_].acessorios[1],PosicaoItemCarro.ArTeto).prefab,chas_.GetComponent<chassi>().acessorios[1].transform);
            Instantiate(itemCarroDeId(gerDados.instancia.dados_.carro[i_].acessorios[2],PosicaoItemCarro.ArCapo).prefab,chas_.GetComponent<chassi>().acessorios[2].transform);
            Instantiate(itemCarroDeId(gerDados.instancia.dados_.carro[i_].acessorios[3],PosicaoItemCarro.Aerofolio).prefab,chas_.GetComponent<chassi>().acessorios[3].transform);

            Material[] tempMats_ = obj_chassi_.GetComponent<MeshRenderer>().materials;



            tempMats_[0] = itemCorDeId(gerDados.instancia.dados_.carro[i_].cor_id[0]).material;
            tempMats_[1] = itemCorDeId(gerDados.instancia.dados_.carro[i_].cor_id[1]).material;

            obj_chassi_.GetComponent<MeshRenderer>().materials = tempMats_;

            SHOWSHOW = obj_chassi_;
            foreach(GameObject porta_ in chas_.GetComponent<chassi>().portas){

                tempMats_ = porta_.GetComponent<MeshRenderer>().materials;

                tempMats_[0] = itemCorDeId(gerDados.instancia.dados_.carro[i_].cor_id[0]).material;
                tempMats_[1] = itemCorDeId(gerDados.instancia.dados_.carro[i_].cor_id[1]).material;

                porta_.GetComponent<MeshRenderer>().materials = tempMats_;
            }
            
            
            
        }
        
  
    }

    public item_casa CasaDeId(int id_){
        item_casa casa_ = null;

        foreach (item_casa cas_ in casas){
            if (cas_ != null)
            {
                if(cas_.id == id_){
                    casa_ = cas_;
                }
            }
        }

        return casa_;
    }

    public item_carro ChassiDeId(int id_){

        item_carro chassi_ = null;

        foreach (item_carro chas_ in itens_carro){
            if (chas_ != null){
                if(chas_.posicao == PosicaoItemCarro.Chassi){
                    if(chas_.id == id_){
                        chassi_ = chas_;
                    }
                }
            }
        }

        return chassi_;
    }

    public item_pneu RodaDeId(int id_){
        item_pneu roda_ = null;

        foreach (item_carro chas_ in itens_carro){
            if (chas_ != null){
                if(chas_.posicao == PosicaoItemCarro.Roda){
                    if(chas_.id == id_){
                        roda_ = chas_ as item_pneu;
                    }
                }
            }
        }

        return roda_;
    }

    public item_carro itemCarroDeId(int id_, PosicaoItemCarro pos_){
        item_carro item_ = null;

        foreach (item_carro chas_ in itens_carro){
            if (chas_ != null){
                if(chas_.posicao == pos_){
                    if(chas_.id == id_){
                        item_ = chas_;
                    }
                }
            }
        }

        return item_;
    }

    public item_carro itemCarroDeId(int id_, PosicaoItemCarro pos_, int idChassi_){
        item_carro item_ = null;
        item_carroceria temp_ = null;

        foreach (item_carro chas_ in itens_carro){
            if (chas_ != null){
                if(chas_.posicao == pos_){
                    
                    temp_ = chas_ as item_carroceria;

                    if(chas_.id == id_ && temp_.id_chassi == idChassi_){
                        item_ = chas_ as item_carro;
                    }
                }
            }
        }

        return item_;
    }

    public item_cor itemCorDeId (int id_){
        item_cor cor_ = null;

        foreach (item_cor itemcor_ in itens_cor){
            if (itemcor_ != null){
                if(itemcor_.id == id_){
                    cor_ = itemcor_;
                }
            }
        }

        return cor_;
    }

    public peca_terreno pecaDeId (int id_){
        
        peca_terreno peca_ = null;

        foreach(peca_terreno peca_i_ in pecas){
            if(peca_i_.id == id_){
                peca_ = peca_i_;
            }
        }

        return peca_;
    }

    public void restartTerrenos(){

        for ( int i= GameObject.Find("pivot_terreno").transform.childCount-1; i>=0; --i )
        {
            GameObject child = GameObject.Find("pivot_terreno").transform.GetChild(i).gameObject;
            Destroy(child );
        }

        GameObject prim_ter_ = Instantiate (primeiro_terreno,GameObject.Find("pivot_terreno").transform);

        todosTerrenos = new List<terreno>();

        todosTerrenos.Add(prim_ter_.GetComponent<terreno>());
    }
}
