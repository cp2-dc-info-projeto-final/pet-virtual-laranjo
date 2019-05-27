using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_casa : MonoBehaviour
{
    public int lugar, destino;

    public bool poderodar = true;
    Animator ani_;
    // Start is called before the first frame update
    void Start()
    {
        ani_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(poderodar){
            if(destino > lugar){
                ani_.SetTrigger("rodarmais");
                //destino--;
                poderodar = false;
            }
            if(destino < lugar){
                ani_.SetTrigger("rodarmenos");
                //destino++;
                poderodar = false;
            }
        }
    }

    public void pode(int incremento){
        destino += incremento;
        poderodar = true;
    }

    public void viracamera(int incremento){
        destino += incremento;
    }
}



