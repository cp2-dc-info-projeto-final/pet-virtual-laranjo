using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coletavel : MonoBehaviour
{
    public TipoDeColetavel tipo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        gerGames.instancia.coletar(tipo);

        Debug.Log("COLETOU: " + tipo.ToString());

        Destroy(gameObject);
    }
}
