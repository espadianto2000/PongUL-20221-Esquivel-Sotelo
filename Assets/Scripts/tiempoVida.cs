using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tiempoVida : MonoBehaviour
{
    private float alpha = 1;
    private bool desaparecer = false;
    private float vida = 2;

    private void Start()
    {
    }
    private void Update()
    {
        if(vida > 0)
        {
            vida -= Time.deltaTime;
        }
        else
        {
            if (!desaparecer)
            {
                desaparecer = true;
            }
        }
        if (desaparecer)
        {
            Color col = GetComponent<Text>().color;
            alpha -= 0.75f*Time.deltaTime;
            if(alpha <= 0)
            {
                col.a = 0;
                GetComponent<Text>().color = col;
                desaparecer = false;
            }
            else
            {
                col.a = alpha;
                GetComponent<Text>().color = col;
            }
        }
    }
    public void cambioModo()
    {
        desaparecer = false;
        alpha = 1;
        Color col = GetComponent<Text>().color;
        col.a = alpha;
        GetComponent<Text>().color = col;
        vida = 2;
    }
}
