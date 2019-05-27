using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porta_casa : MonoBehaviour
{
    public int num_porta;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {

        //Debug.Log("PORTA " + other.name);
        
        if(other.gameObject.tag == "Player"){
            if(num_porta == 1){
                gerenciador.instancia.casa_sair(other.gameObject);
            }
            if(num_porta ==2){
                gerenciador.instancia.casa_entrar(other.gameObject);
            }
        }
    }
}
