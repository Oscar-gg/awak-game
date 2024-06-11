using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject botonGlosario;
    [SerializeField] private GameObject menuGlosario;
    private bool juegoPausado = false;
    private bool juegoGlosario = false;

    private TextOverlayController textOverlayController;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(juegoPausado)
            {
                Renaudar();
            }
            else
            {
                Pausa();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoGlosario)
            {
                Renaudar();
            }
            else
            {
                Glosario();
            }
        }
    }
    public void Pausa()
    {
        // // Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        // Time.timeScale = 0;
    }

    public void Renaudar()
    {
        // Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        // Time.timeScale = 1;
    }

    public void Glosario()
    {
        // Time.timeScale = 0f;
        //botonGlosario.SetActive(false);
        menuGlosario.SetActive(true);
        // Time.timeScale = 0;
    }

    public void RenaudarGlosario()
    {
        // Time.timeScale = 1f;
        botonGlosario.SetActive(true);
        menuGlosario.SetActive(false);
        // Time.timeScale = 1;
    }


    public void SalirMundoComunicacion()
    {
        // Time.timeScale = 1;
        SceneManager.LoadScene("MundoComunicacion");
    }

    public void SalirMundoSeguridad()
    {
        // Time.timeScale = 1;
        SceneManager.LoadScene("MundoSeguridad");
    }

    public void SalirMundoEtica()
    {
        // Time.timeScale = 1;
        SceneManager.LoadScene("MundoEtica");
    }

    public void Reiniciar ()
    {
        // Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar()
    {
        // Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
        //Application.Quit();
    }

    private void Start()
    {
        // Asegurarse de que el men� de pausa est� desactivado al iniciar
        menuPausa.SetActive(false);
        
        if (menuGlosario != null)
            menuGlosario.SetActive(false);
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene(SceneNames.LOGIN);
    }
}
