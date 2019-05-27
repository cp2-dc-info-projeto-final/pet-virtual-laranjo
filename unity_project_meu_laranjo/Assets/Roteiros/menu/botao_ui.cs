using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class botao_ui : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public bool pressionado;

    public void OnPointerDown(PointerEventData eventData){
        pressionado = true;
    }

    public void OnPointerUp(PointerEventData eventData){
        pressionado = false;
    }
}
