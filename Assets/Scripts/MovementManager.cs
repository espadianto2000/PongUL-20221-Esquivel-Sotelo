using UnityEngine;

public enum TipoJugador
{
    JUGADOR1, JUGADOR2
}

public class MovementManager : MonoBehaviour
{
    public float speed;
    public TipoJugador tipo;
    public bool moverse = false;
    public float limite=8;
    public float timer =0;
    public float cooldown = 0;
    public bool poder = true;
    public bool enCooldown = false;

    private void Update()
    {
        if (moverse)
        {
            float movement;
            if (tipo == TipoJugador.JUGADOR1)
            {
                // Leer el input del usuario
                movement = Input.GetAxis("Vertical");
            }
            else
            {
                movement = Input.GetAxis("Mouse Y");
            }

            Vector3 actualPos = GetComponent<Transform>().position;
            GetComponent<Transform>().position = new Vector3(
                    actualPos.x,
                    Mathf.Clamp(actualPos.y + (speed * movement * Time.deltaTime), -limite, limite),
                    actualPos.z
            );
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (!enCooldown && !poder)
                {
                    desactivarPoder();
                    cooldown = 30;
                    enCooldown = true;
                }
            }
            if(cooldown > 0 && enCooldown)
            {
                cooldown -= Time.deltaTime;
            }
            else if(cooldown<=0 && enCooldown)
            {
                if (!poder)
                {
                    enCooldown = false;
                    poder = true;
                }
            }
        }
        
    }
    public void reiniciar()
    {
        timer = 0;
        cooldown = 0;
        poder = true;
        enCooldown=false;
        desactivarPoder();
        transform.position = new Vector3(transform.position.x, 0, 0);
        moverse = true;
    }
    public void activarPoder()
    {
        if (poder)
        {
            poder = false;
            speed = 35;
            transform.localScale = new Vector3(transform.localScale.x, 6, 1);
            limite = 7;
            timer = 5;
        }
    }
    public void desactivarPoder()
    {
        speed = 25;
        transform.localScale = new Vector3(transform.localScale.x, 4, 1);
        limite = 8;
    }
}
