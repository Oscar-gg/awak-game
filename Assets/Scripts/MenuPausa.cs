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
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Renaudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Glosario()
    {
        Time.timeScale = 0f;
        botonGlosario.SetActive(false);
        menuGlosario.SetActive(true);
    }

    public void RenaudarGlosario()
    {
        Time.timeScale = 1f;
        botonGlosario.SetActive(true);
        menuGlosario.SetActive(false);
    }


    public void SalirMundoComunicacion()
    {
        SceneManager.LoadScene("MundoComunicacion");
    }

    public void SalirMundoSeguridad()
    {
        SceneManager.LoadScene("MundoSeguridad");
    }

    public void SalirMundoEtica()
    {
        SceneManager.LoadScene("MundoEtica");
    }

    public void Reiniciar ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar()
    {
        Application.Quit();
    }

    private void Start()
    {
        // Asegurarse de que el menú de pausa esté desactivado al iniciar
        menuPausa.SetActive(false);
        menuGlosario.SetActive(false);
    }
}
