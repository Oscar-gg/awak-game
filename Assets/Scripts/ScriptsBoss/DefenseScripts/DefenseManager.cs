using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

using System.Linq;
using UnityEngine.SceneManagement;
public class DefenseManager : MonoBehaviour
{
    private GameObject hero;

    private GameObject boss;

    [SerializeField]
    private GameObject respetoPrefab;

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    [SerializeField]
    private GameObject defenseMenu;

    [SerializeField]
    private GameObject trueButton;

    [SerializeField]
    private GameObject falseButton;

    [SerializeField]
    private GameObject battleMenu;


    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text TrueAnswerText;

    [SerializeField]
    private Text FalseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    public string Answer;

    public Animator AnswerAnimatorController;



    private void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        boss = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Start()
    {
        if(unansweredQuestions == null || unansweredQuestions.Count ==0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;//hacer que sea random las preguntas

        if (currentQuestion.isTrue)
        {
            TrueAnswerText.text = "Correcto";
            FalseAnswerText.text = "Incorrecto";
        } else
        {
            TrueAnswerText.text = "Incorrecto";
            FalseAnswerText.text = "Correcto";
        }

    }

    public enum AnswerAnimations
    {
        TrueA, FalseA, IdleA
    }

    IEnumerator TransitionToNextQuestion()
    {

        unansweredQuestions.Remove(currentQuestion); //evitar que se repitan las preguntas

        yield return new WaitForSeconds(timeBetweenQuestions);

        Debug.Log("ResetAnswer");
        this.trueButton.SetActive(true);
        this.falseButton.SetActive(true);
        this.defenseMenu.SetActive(false);
      //this.battleMenu.SetActive(true);

        yield return new WaitForSeconds(timeBetweenQuestions);
        SetCurrentQuestion();    
    }

    public void UserSelectTrue()
    {
        this.falseButton.SetActive(false);
        Debug.Log("SelectedTrue");
        if (currentQuestion.isTrue)
        {
            Debug.Log("Correcto");
            Answer = "Correcto";
            respetoPrefab.GetComponent<AttackScript>().NoAttack(hero);
            //this.battleMenu.SetActive(true);
        }
        else
        {
            ScoreSystem.scoreValue -= 100;
            Debug.Log("Incorrecto");
            Answer = "Incorrecto";
            respetoPrefab.GetComponent<AttackScript>().Attack(hero);

        }
        Invoke("ResetAnswerSelect", 2);
        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        this.trueButton.SetActive(false);
        Debug.Log("SelectedFalse");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Correcto");
            Answer = "Correcto";
            respetoPrefab.GetComponent<AttackScript>().NoAttack(hero);
           //this.battleMenu.SetActive(true);
        }
        else
        {
            ScoreSystem.scoreValue -= 100;
            Debug.Log("Incorrecto");
            Answer = "Incorrecto";
            respetoPrefab.GetComponent<AttackScript>().Attack(hero);
        }
        Invoke("ResetAnswerSelect", 2);
        StartCoroutine(TransitionToNextQuestion());
    }

    public string GetAnswerQuestion()
    {
        return Answer;
    }

    public void UpdateAnswerAnimation(AnswerAnimations nameAnswerAnimations)
    {
        switch(nameAnswerAnimations)
        {
            case AnswerAnimations.TrueA:
                AnswerAnimatorController.SetBool("TrueA", true);
                AnswerAnimatorController.SetBool("IdleA", false);
                break;
            case AnswerAnimations.FalseA:
                AnswerAnimatorController.SetBool("FalseA", true);
                AnswerAnimatorController.SetBool("IdleA", false);
                break;
        }
    }

    public void ResetAnswerSelect()
    {
        this.trueButton.SetActive(true);
        this.trueButton.SetActive(true);
    }

}
