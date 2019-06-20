using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terreno : MonoBehaviour
{
    public int id;
    public int posX, posZ;
    public int tamanho = 100;
    public bool canto = false, dentro = false, fora = false;
    public bool[] temlado = new bool[]{false,false,false,false};
    public GameObject cenario, gatilho, pivotTerreno;
    public GameObject[] vizinho = new GameObject[4];


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
            Debug.Log("TA true");
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
                        /*

                        --- gerador sendo melhorado ---

                        foreach (peca_terreno peca_ in gerenciador.instancia.pecas)
                        {
                            List<int> lista_;

                            if(i == 1){
                                foreach (int i_ in peca_.conexao_4)
                                {
                                    if(i_ == id){
                                        //lista_.Add(i_);
                                    }
                                }
                            }
                            
                            if(i == 2){

                            }

                            if(i == 3){

                            }

                            if(i == 4){

                            }
                        }

                        */
                        
                        // --- GERANDOR ALEATORIO CAPENGA (so pra cumprir o cronograma porque estou formatando o pc pra resolver um problema no Unity)

                        vizinho[i] = Instantiate(gerenciador.instancia.terrenosPrefabs[Random.Range(0,gerenciador.instancia.terrenosPrefabs.lenght)],new Vector3(1000,1000,1000),Quaternion.Euler(0,0,0), pivotTerreno.transform);

                        //vizinho[i] = Instantiate(gerenciador.instancia.terrenosPrefabs[0],new Vector3(1000,1000,1000),Quaternion.Euler(0,0,0), pivotTerreno.transform);
                        Debug.Log(vizinho[i].transform.position.y.ToString());
                        vizinho[i].GetComponent<terreno>().setar_dados(i,i == 0 || i == 3? posX : i == 1 ? posX - 1 : posX + 1, i == 1 || i == 2? posZ : i == 0 ? posZ + 1 : posZ - 1, gameObject);
                        vizinho[i].transform.position = new Vector3( - vizinho[i].GetComponent<terreno>().posX * tamanho,0, - tamanho - vizinho[i].GetComponent<terreno>().posZ * tamanho);
                        //vizinho[i].GetComponent<terreno>().gatilho.SetActive(true);
                        vizinho[i].GetComponent<terreno>().cenario.SetActive(false);
                        gerenciador.instancia.todosTerrenos.Add(vizinho[i].GetComponent<terreno>());
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

    public void setar_dados (int lado_, int posX_, int posZ_, GameObject instaciador_){
        posX = posX_;
        posZ = posZ_;

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
}
