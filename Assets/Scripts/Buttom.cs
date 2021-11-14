using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buttom : MonoBehaviour, IPointerDownHandler
{
    public Parameter Action;

    public enum Parameter
    {
        Attack,
        Magic,
        Skip
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }

}
