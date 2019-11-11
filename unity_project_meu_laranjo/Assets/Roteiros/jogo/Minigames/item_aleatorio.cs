using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_aleatorio : MonoBehaviour
{
    public bool tem_gasolina = false, setar_parente = true;

    public GameObject prefab_moeda, prefab_dolar, prefab_gasolina;
    public int prob_moeda, prob_dolar, prob_gasolina;
    // Start is called before the first frame update
    void Start()
    {
        int sorteado = Random.Range(1,1001);

        if(sorteado <= prob_dolar){
            GameObject inst_ = Instantiate(prefab_dolar,transform.position, transform.rotation);

            inst_.transform.SetParent(gameObject.transform);
        }else
        {
            if(sorteado <= prob_dolar + prob_moeda){
                GameObject inst_ = Instantiate(prefab_moeda,transform.position, transform.rotation);

                inst_.transform.SetParent(gameObject.transform);
            }else
            {
                if(sorteado <= prob_dolar + prob_moeda + prob_gasolina && tem_gasolina){
                    GameObject inst_ = Instantiate(prefab_gasolina,transform.position, transform.rotation);

                    inst_.transform.SetParent(gameObject.transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
