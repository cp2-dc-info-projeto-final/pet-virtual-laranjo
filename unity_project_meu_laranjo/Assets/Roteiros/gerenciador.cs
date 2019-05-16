using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gerenciador : MonoBehaviour
{
    public GameObject laranjo,laranjo_preview, prefab_botao, lista_loja, lista_armario;

    public TextMeshProUGUI[] textoLoja = new TextMeshProUGUI[3], textoArmario = new TextMeshProUGUI[3];
    public List<item> itens;

    public Material mat_lar_0, mat_lar_meio, mat_lar_1, mat_lar_final;

    public float nivel_lar = 0;
    public Slider slider_nivel;
    public Image[] cor_nivel;
    public Sprite[] raridade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nivel_lar < 0.5f){

            mat_lar_final.color = Color.Lerp(mat_lar_0.color,mat_lar_meio.color,nivel_lar * 2);

        }else
        {
            mat_lar_final.color = Color.Lerp(mat_lar_meio.color,mat_lar_1.color,(nivel_lar - 0.5f) * 2);
        }
        
        slider_nivel.value = nivel_lar;

        foreach(Image cor_ in cor_nivel){
            cor_.color = mat_lar_1.color;
        }
    }

    public void colocaritemLoja(int id_){
        for(int i = 0; i < itens.Count - 1; i++){
            if(itens[i] != null){
                if(itens[i].id == id_){
                    laranjo_preview.GetComponent<design>().MudarMesh(itens[i]);

                    if(itens[i].posicao == 5){
                        laranjo_preview.GetComponent<Animator>().SetBool("item",itens[i].seguraItem);
                    }
                }
            }
        }
    }

    public void colocaritemArmario(int id_){
        for(int i = 0; i < itens.Count - 1; i++){
            if(itens[i] != null){
                if(itens[i].id == id_){
                    laranjo_preview.GetComponent<design>().MudarMesh(itens[i]);
                    laranjo.GetComponent<design>().MudarMesh(itens[i]);

                    if(itens[i].posicao == 5){
                        laranjo_preview.GetComponent<Animator>().SetBool("item",itens[i].seguraItem);
                        laranjo.GetComponent<Animator>().SetBool("item",itens[i].seguraItem);
                    }
                }
            }
        }
    }

    public void botaoArmario(int id_){
        textoArmario[0].text = itemDeId(id_).nome;
        textoArmario[1].text = itemDeId(id_).descricao;

        colocaritemArmario(id_);
    }

    public void botaoLoja(int id_){
        textoLoja[0].text = itemDeId(id_).nome;
        textoLoja[1].text = itemDeId(id_).descricao;
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
                    botao_.GetComponent<Image>().sprite = raridade[item_.raridade];
                    botao_.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item_.imagem;
                    botao_.GetComponent<Button>().onClick.AddListener(() => botaoLoja(item_.id));

                    item_.id_ordem = item_.id;
                }
            }
            
        }else{
            foreach(item item_ in itens)
            {
                if(item_ != null){
                    if(item_.posicao == posi_){
                        GameObject botao_ = Instantiate(prefab_botao,lista_loja.transform);
                        botao_.GetComponent<Image>().sprite = raridade[item_.raridade];
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
                        botao_.GetComponent<Image>().sprite = raridade[item_.raridade];
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
                    if(item_.posicao == posi_){
                        if(gerDados.instancia.temItem(item_.id)){

                        GameObject botao_ = Instantiate(prefab_botao,lista_armario.transform);
                        botao_.GetComponent<Image>().sprite = raridade[item_.raridade];
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
}
