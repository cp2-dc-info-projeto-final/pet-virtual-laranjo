using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menuReceberDolares : MonoBehaviour
{
    public int quant_moedas, quant_dolares;

    public GameObject moeda_prefab, moeda_destino, dolar_prefab, dolar_destino, pivot_animacao;
    public List<GameObject> dolares = new List<GameObject>();
    public Button botao_confirmar;
    public TextMeshProUGUI texto_quant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void receber(int quant_dolares_){

        quant_dolares = quant_dolares_;

        texto_quant.text = "x " + quant_dolares_.ToString();
        GetComponent<animar_UI>().mostrar_ocultar();
        botao_confirmar.interactable = true;
    }

    public void botaoConfirmar(){
        botao_confirmar.interactable = false;
        StartCoroutine(confirmado());
    }

    public void receberMoeda(int quant_moedas_){

        quant_moedas = quant_moedas_;

        texto_quant.text = "x " + quant_moedas_.ToString();
        GetComponent<animar_UI>().mostrar_ocultar();
        botao_confirmar.interactable = true;
    }

    public void botaoConfirmarMoeda(){
        botao_confirmar.interactable = false;
        StartCoroutine(confirmadoMoeda());
    }

    

    public IEnumerator confirmado(){

        for(int i_ = 0; i_ < quant_dolares / 4; i_++){
            StartCoroutine(instanciarDolar());
        }

        yield return new WaitForSeconds(1.2f);

        GetComponent<animar_UI>().mostrar_ocultar();
        
        yield return new WaitForSeconds(0.4f);

        gerDados.instancia.dados_.dolares += ulong.Parse(quant_dolares.ToString());

        gerDados.instancia.salvar(true);
    }

     public IEnumerator confirmadoMoeda(){

        for(int i_ = 0; i_ < (quant_moedas < 1000 ? 40 : quant_moedas < 10000 ? 90 : 220); i_++){
            StartCoroutine(instanciarMoeda());
        }

        yield return new WaitForSeconds(1.2f);

        GetComponent<animar_UI>().mostrar_ocultar();
        
        yield return new WaitForSeconds(0.4f);

        gerDados.instancia.dados_.moedas += ulong.Parse(quant_moedas.ToString());

        gerDados.instancia.salvar(true);
    }

    public IEnumerator instanciarDolar(){

        yield return new WaitForSeconds(Random.Range(0.0f,0.4f));
        GameObject inst = Instantiate(dolar_prefab,new Vector3(botao_confirmar.transform.position.x + Random.Range(-50,50),botao_confirmar.transform.position.y + Random.Range(-50,50),0), Quaternion.Euler(0,0,0), pivot_animacao.transform);
        inst.transform.localScale = new Vector3(0,0,0);

        LeanTween.scale(inst, new Vector3(1,1,1),0.3f).setEaseInCubic();

        yield return new WaitForSeconds(0.5f);

        LeanTween.move(inst, dolar_destino.transform.position,0.8f).setEaseInCubic().setDestroyOnComplete(true);


    }

    public IEnumerator instanciarMoeda(){

        yield return new WaitForSeconds(Random.Range(0.0f,0.4f));
        GameObject inst = Instantiate(moeda_prefab,new Vector3(botao_confirmar.transform.position.x + Random.Range(-50,50),botao_confirmar.transform.position.y + Random.Range(-50,50),0), Quaternion.Euler(0,0,0), pivot_animacao.transform);
        inst.transform.localScale = new Vector3(0,0,0);

        LeanTween.scale(inst, new Vector3(1,1,1),0.3f).setEaseInCubic();

        yield return new WaitForSeconds(0.5f);

        LeanTween.move(inst, moeda_destino.transform.position,0.8f).setEaseInCubic().setDestroyOnComplete(true);


    }
}
