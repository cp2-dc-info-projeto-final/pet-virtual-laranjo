
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class terreno : MonoBehaviour
{
    public int id;
    public int posX, posZ;
    public int tamanho = 100;
    public bool canto = false, dentro = false, fora = false;
    public bool[] temlado = new bool[]{false,false,false,false};
    public GameObject cenario, gatilho, pivotTerreno;
    public GameObject[] vizinho = new GameObject[4];
    public List<int> ListaInstancia_;


/*
    public terreno constructor (int lado_, int posX_, int posZ_, GameObject gatilho_){
        posX = posX_;
        posZ = posZ_;

        if(posZ == 0){
            canto = true;
        }

        if(lado_ == 0){
            vizinho[3] = gameObject;
            temlado[3] = true;
        }
        if(lado_ == 1){
            vizinho[2] = gameObject;
            temlado[2] = true;
        }
        if(lado_ == 2){
            vizinho[1] = gameObject;
            temlado[1] = true;
        }
        if(lado_ == 3){
            vizinho[0] = gameObject;
            temlado[0] = true;
        }

        gatilho = gatilho_;

        return null;
    }

    */
    // Start is called before the first frame update
    void Start()
    {

        pivotTerreno = transform.parent.gameObject;
        gatilho.transform.localScale = new Vector3(gerenciador.instancia.render_area,100,gerenciador.instancia.render_area);
    }

    // Update is called once per frame
    void Update()
    {
        if(dentro){
            //Debug.Log("TA true");
            for(int i = canto ? 2 : 3; i >= 0 ; i--){
                if(vizinho[i] != null){
                    //vizinho[i].SetActive(true);
                    //vizinho[i].GetComponent<terreno>().cenario.SetActive(true);
                    cenario.SetActive(true);
                }else
                {
                    if(gerenciador.instancia.pegar_terreno(i == 0 || i == 3? posX : i == 1 ? posX - 1 : posX + 1, i == 1 || i == 2? posZ : i == 0 ? posZ + 1 : posZ - 1) != null){
                        vizinho[i] = gerenciador.instancia.pegar_terreno(i == 0 || i == 3? posX : i == 1 ? posX - 1 : posX + 1, i == 1 || i == 2? posZ : i == 0 ? posZ + 1 : posZ - 1).gameObject;
                    }else
                    {
                        

                        //--- gerador sendo melhorado ---

                        ListaInstancia_ = new List<int>();

                        foreach (peca_terreno peca_ in gerenciador.instancia.pecas)
                        {
                            
                            
                            if(i == 0){

                                ListaInstancia_ = listaDePosicao(posX,posZ + 1);

                                /*
                                foreach (int id_ in peca_.conexao_4)
                                {
                                    // ----------------------------------------------------------------------------------------------------------- PAUSA AQUI Debug.Log()
                                    //Debug.Log("TAQUI: " + id + " - " + id_);
                                    if(id_ == id){

                                        bool podeAdd = true;
                                        //Debug.Log("TAQUI1111111111111111111111111111111111111111 " + id + " - " + peca_.id);
                                        foreach (int num_ in ListaInstancia_)
                                        {
                                            //Debug.Log("TAQUI222222222222222222222222222222222222222" + num_);
                                            if(peca_.id == num_){
                                                //Debug.Log("*****REMOVIDO EM POS1: " + peca_.id);
                                                podeAdd = false;
                                            }
                                        }
                                        if(podeAdd){
                                            
                                            ListaInstancia_.Add(peca_.id);
                                            //Debug.Log("-----ADICIONADO EM POS1: " + peca_.id);
                                        }
                                        
                                    }
                                }*/
                            }
                            
                            if(i == 1){

                                ListaInstancia_ = listaDePosicao(posX - 1,posZ);

                                /*
                                foreach (int id_ in peca_.conexao_3)
                                {
                                    if(id_ == id){

                                        bool podeAdd = true;
                                        foreach (int num_ in ListaInstancia_)
                                        {
                                            if(peca_.id == num_){
                                                //Debug.Log("*****REMOVIDO EM POS2: " + peca_.id);
                                                podeAdd = false;
                                            }
                                        }
                                        if(podeAdd){
                                            ListaInstancia_.Add(peca_.id);
                                            //Debug.Log("-----ADICIONADO EM POS2: " + peca_.id);
                                        }
                                        
                                    }
                                }*/

                                if(posZ == 0){
                                    ListaInstancia_ = ListaInstancia_.Intersect(gerenciador.instancia.ter_canto0).ToList();
                                }else
                                {
                                    ListaInstancia_ = ListaInstancia_.Distinct().ToList();
                                    
                                    
                                    foreach (int i_i_ in gerenciador.instancia.ter_canto0)
                                    {
                                        ListaInstancia_.Remove(i_i_);
                                    }
                                }

                                if(posZ == 1){
                                    ListaInstancia_ = ListaInstancia_.Intersect(gerenciador.instancia.ter_canto1).ToList();
                                }

                            }

                            if(i == 2){
                                ListaInstancia_ = listaDePosicao(posX + 1,posZ);

                                /*
                                foreach (int id_ in peca_.conexao_2)
                                {
                                    if(id_ == id){

                                        bool podeAdd = true;
                                        foreach (int num_ in ListaInstancia_)
                                        {
                                            if(peca_.id == num_){
                                                //Debug.Log("*****REMOVIDO EM POS3: " + peca_.id);
                                                podeAdd = false;
                                            }
                                        }
                                        if(podeAdd){
                                            ListaInstancia_.Add(peca_.id);
                                            //Debug.Log("-----ADICIONADO EM POS3: " + peca_.id);
                                        }
                                        
                                    }
                                }*/

                                if(posZ == 0){
                                    ListaInstancia_ = ListaInstancia_.Intersect(gerenciador.instancia.ter_canto0).ToList();
                                }else
                                {
                                    ListaInstancia_ = ListaInstancia_.Distinct().ToList();
                                    
                                    foreach (int i_i_ in gerenciador.instancia.ter_canto0)
                                    {
                                        ListaInstancia_.Remove(i_i_);
                                    }
                                }

                                if(posZ == 1){
                                    ListaInstancia_ = ListaInstancia_.Intersect(gerenciador.instancia.ter_canto1).ToList();
                                }
                            }

                            if(i == 3){

                                ListaInstancia_ = listaDePosicao(posX,posZ - 1);

                                /*
                                foreach (int id_ in peca_.conexao_1)
                                {
                                    if(id_ == id){

                                        bool podeAdd = true;
                                        foreach (int num_ in ListaInstancia_)
                                        {
                                            if(peca_.id == num_){
                                                //Debug.Log("*****REMOVIDO EM POS4: " + peca_.id);
                                                podeAdd = false;
                                            }
                                        }
                                        if(podeAdd){
                                            ListaInstancia_.Add(peca_.id);
                                            //Debug.Log("-----ADICIONADO EM POS4: " + peca_.id);
                                        }
                                        
                                    }
                                }*/
                                if(posZ == 1){
                                    ListaInstancia_ = ListaInstancia_.Intersect(gerenciador.instancia.ter_canto0).ToList();
                                }else
                                {
                                    ListaInstancia_ = ListaInstancia_.Distinct().ToList();

                                    foreach (int i_i_ in gerenciador.instancia.ter_canto0)
                                    {
                                        ListaInstancia_.Remove(i_i_);
                                    }
                                    
                                }

                                if(posZ == 2){
                                    ListaInstancia_ = ListaInstancia_.Intersect(gerenciador.instancia.ter_canto1).ToList();
                                }
                                
                            }
                        }

                        //numero divisor da problabilidade
                        
                        foreach (int id_ in ListaInstancia_)
                        {
                            //Debug.Log("tah na lista" + id_);
                        }

                        int maxRand_ = 0;

                        //criando uma probabilidade maxima com a soma de todos os valores de probabilidade

                        foreach (int id_ in ListaInstancia_)
                        {
                            
                            foreach (peca_terreno peca in gerenciador.instancia.pecas)
                            {
                                if(peca.id == id_){
                                    maxRand_ += peca.probabilidade;
                                }
                            }
                        }

                        //sorteando um numero no range da probabilidade maxima

                        int numAleatorio = Random.Range(0,maxRand_) + 1;

                        maxRand_ = 0;

                        bool parar_foreach = false;

                        //instanciando o prefab correspondente a peca de terreno sorteada
                        
                        int numId;

                        //Debug.Log("SORTEADO: " + numAleatorio);

                        foreach (int id_ in ListaInstancia_)
                        {
                            
                            if(!parar_foreach){
                                foreach (peca_terreno peca in gerenciador.instancia.pecas)
                                {
                                    if(peca.id == id_){
                                        maxRand_ += peca.probabilidade;
                                        if(numAleatorio <= maxRand_){
                                            //Debug.Log("TAH AQUI PO");
                                            numId = id_;
                                            vizinho[i] = Instantiate(gerenciador.instancia.pecas[numId].prefabs[Random.Range(0,gerenciador.instancia.pecas[numId].prefabs.Length)],new Vector3(1000,1000,1000),Quaternion.Euler(0,0,0), pivotTerreno.transform);

                                            parar_foreach = true;

                                            // --- GERANDOR ALEATORIO CAPENGA (so pra cumprir o cronograma porque estou formatando o pc pra resolver um problema no Unity)

                                            //vizinho[i] = Instantiate(gerenciador.instancia.terrenosPrefabs[Random.Range(0,gerenciador.instancia.terrenosPrefabs.lenght)],new Vector3(1000,1000,1000),Quaternion.Euler(0,0,0), pivotTerreno.transform);

                                            //vizinho[i] = Instantiate(gerenciador.instancia.terrenosPrefabs[0],new Vector3(1000,1000,1000),Quaternion.Euler(0,0,0), pivotTerreno.transform);
                                            //Debug.Log(vizinho[i].transform.position.y.ToString());
                                            vizinho[i].GetComponent<terreno>().setar_dados(numId,i,i == 0 || i == 3? posX : i == 1 ? posX - 1 : posX + 1, i == 1 || i == 2? posZ : i == 0 ? posZ + 1 : posZ - 1, gameObject);
                                            vizinho[i].transform.position = new Vector3( - vizinho[i].GetComponent<terreno>().posX * tamanho,0, - tamanho - vizinho[i].GetComponent<terreno>().posZ * tamanho);
                                            //vizinho[i].GetComponent<terreno>().gatilho.SetActive(true);
                                            vizinho[i].GetComponent<terreno>().cenario.SetActive(false);
                                            gerenciador.instancia.todosTerrenos.Add(vizinho[i].GetComponent<terreno>());
                                        }
                                    }
                                }
                            }
                        }
                        
                        
                    }
                    
                }
            }
            dentro = false;
        }
        if(fora)
        {
            cenario.SetActive(false);
            /*
            for(int i = canto ? 2 : 3; i >= 0 ; i--){

                if(vizinho[i] != null){
                    vizinho[i].GetComponent<terreno>().cenario.SetActive(false);
                    cenario.SetActive(false);
                }else
                {
                    //vizinho[i] = Instantiate(terrenosPrefabs[0], pivotTerreno.transform);
                    //vizinho[i].GetComponent<terreno>().setar_dados(0,i == 0 || i == 3 ? posX : i == 1? posX - 1 : posX + 1,i == 1 || i == 2? posZ : i == 0 ? posZ + 1 : posZ - 1);
                    //vizinho[i].transform.position = new Vector3( - vizinho[i].GetComponent<terreno>().posX * tamanho,0, - tamanho - vizinho[i].GetComponent<terreno>().posZ * tamanho);
                }
            }
            */
            fora = false;
        }
    }

    public void setar_dados (int id_, int lado_, int posX_, int posZ_, GameObject instaciador_){
        posX = posX_;
        posZ = posZ_;
        id = id_;

        if(posZ == 0){
            canto = true;
        }

        if(lado_ == 0){
            vizinho[3] = instaciador_;
            temlado[3] = true;
        }
        if(lado_ == 1){
            vizinho[2] = instaciador_;
            temlado[2] = true;
        }
        if(lado_ == 2){
            vizinho[1] = instaciador_;
            temlado[1] = true;
        }
        if(lado_ == 3){
            vizinho[0] = instaciador_;
            temlado[0] = true;
        }

        //gatilho = gatilho_;
    }

    public List<int> listaDePosicao(int pX, int pZ){
        List<int> lista_ = new List<int>();

        foreach (peca_terreno peca_ in gerenciador.instancia.pecas)
        {
            lista_.Add(peca_.id);
        }

        if(gerenciador.instancia.pegar_terreno(pX+1,pZ) != null){
            
            lista_ = lista_.Intersect(gerenciador.instancia.pecaDeId(gerenciador.instancia.pegar_terreno(pX+1,pZ).GetComponent<terreno>().id).conexao_2).ToList();
        }

        if(gerenciador.instancia.pegar_terreno(pX-1,pZ) != null){
            
            lista_ = lista_.Intersect(gerenciador.instancia.pecaDeId(gerenciador.instancia.pegar_terreno(pX-1,pZ).GetComponent<terreno>().id).conexao_3).ToList();
        }

        if(gerenciador.instancia.pegar_terreno(pX,pZ+1) != null){
            
            lista_ = lista_.Intersect(gerenciador.instancia.pecaDeId(gerenciador.instancia.pegar_terreno(pX,pZ+1).GetComponent<terreno>().id).conexao_4).ToList();
        }

        if(gerenciador.instancia.pegar_terreno(pX,pZ-1) != null){
            
            lista_ = lista_.Intersect(gerenciador.instancia.pecaDeId(gerenciador.instancia.pegar_terreno(pX,pZ-1).GetComponent<terreno>().id).conexao_1).ToList();
        }

        return lista_;

    }
}
