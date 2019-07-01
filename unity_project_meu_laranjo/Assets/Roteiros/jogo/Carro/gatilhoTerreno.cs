using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatilhoTerreno : MonoBehaviour
{
    public bool entrou = false;
    public terreno ter_;

    public GameObject ot;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "carro"){
            entrou = true;
            ter_.dentro = true;
            Debug.Log("setou true");
            ot = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "carro"){
            ter_.fora = true;
            Debug.Log("setou false");
        };
        
    }
}
