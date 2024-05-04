using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado = false;

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
    }
}
