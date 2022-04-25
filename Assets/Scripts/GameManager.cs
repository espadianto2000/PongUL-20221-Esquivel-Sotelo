
using UnityEngine;
using UnityEngine.UI;
using System;

// Observador
public class GameManager : MonoBehaviour
{
    [Header("objetos")]
    public GameObject paddle1;
    public GameObject paddle2;
    public GameObject ball;
    [Header("elementosUI")]
    public Text tituloUI;
    public Text mensajeUI;
    public Text score1UI;
    public Text score2UI;
    public Text Modo;

    private bool mRunning = false;
    private bool ia = false;
    private BallMovementManager mBallMovementManager;

    private int mScoreJugador1 = 0;
    private int mScoreJugador2 = 0;

    private void Start()
    {
        mBallMovementManager = ball.GetComponent<BallMovementManager>();
        mBallMovementManager.AddGoalScoredDelegate(OnGoalScoredDelegate);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!mRunning && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            if (!ia)
            {
                Modo.text = "Modo P Vs. CPU";
                ia = true;
                if (mRunning)
                {
                    paddle2.GetComponent<PaddleIA>().activo = true;
                    paddle2.GetComponent<MovementManager>().moverse = false;
                }
            }
            else
            {
                Modo.text = "Modo P Vs. P";
                ia = false;
                if (mRunning)
                {
                    paddle2.GetComponent<PaddleIA>().activo = false;
                    paddle2.GetComponent<MovementManager>().moverse = true;
                }
            }
            Modo.GetComponent<tiempoVida>().cambioModo();
        }
    }

    private void StartGame()
    {
        ball.SetActive(true);
        ball.GetComponent<TrailRenderer>().enabled = true;
        //reiniciar movimiento de paddles
        paddle1.GetComponent<MovementManager>().reiniciar();
        if (ia)
        {
            paddle2.GetComponent<PaddleIA>().reiniciar();
        }
        else
        {
            paddle2.GetComponent<MovementManager>().reiniciar();
        }
        //
        mBallMovementManager.StartGame();
        tituloUI.gameObject.SetActive(false);
        mensajeUI.gameObject.SetActive(false);
        score1UI.gameObject.SetActive(false);
        score2UI.gameObject.SetActive(false);
        mRunning = true;
    }

    public void OnGoalScoredDelegate(object sender, EventArgs e)
    {
        Debug.Log("Goal");

        // Actualizar los score
        GoalScoredData data = e as GoalScoredData;
        if (data.jugador == "Paddle1")
        {
            mScoreJugador1++;
        }else
        {
            mScoreJugador2++;
        }


        tituloUI.text = "GOL!";
        mensajeUI.text = "Presione Espacio para continuar...";
        score1UI.text = mScoreJugador1.ToString();
        score2UI.text = mScoreJugador2.ToString();
        tituloUI.gameObject.SetActive(true);
        mensajeUI.gameObject.SetActive(true);
        score1UI.gameObject.SetActive(true);
        score2UI.gameObject.SetActive(true);
        mRunning = false;
        //detener paddles tras un gol
        paddle1.GetComponent<MovementManager>().moverse = false;
        if (ia)
        {
            paddle2.GetComponent<PaddleIA>().activo = false;
        }
        else
        {
            paddle2.GetComponent<MovementManager>().moverse = false;
        }
    }
}
