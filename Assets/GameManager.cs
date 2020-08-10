using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Image timeBar;
    public float totalTime = 10;
    public Animator scoreLabelAnimator;
    public TMP_Text scoreLabel;
    public TMP_Text questionLabel;
    public TMP_Text[] optionLabel;
    public QuestionData[] questions;
    private int currentQuestionindex;
    private int currentScore;
    private float Timer;
    private bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        scoreLabel.text = "0";
        questionLabel.text = questions[0].question;
        for (int i = 0; i < questions[0].options.Length; i++)
        {
            optionLabel[i].text = questions[0].options[i].option;
        }
    }
    private void RestartTimer()
    {
        Timer = totalTime;
        timeBar.fillAmount = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isGameActive)
            return;
        Timer -= Time.deltaTime;
        timeBar.fillAmount = Timer / totalTime;
        if (Timer<0)
        {
            NextQuestion();
        }
    }

    public void NextQuestion()
    {
        currentQuestionindex++;
        if (currentQuestionindex<questions.Length)
        {
            RestartTimer();
            
            questionLabel.text = questions[currentQuestionindex].question;
            for (int i = 0; i < questions[currentQuestionindex].options.Length; i++)
            {
                optionLabel[i].text = questions[currentQuestionindex].options[i].option;
            }
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    public void OptionSelected(int index)
    {
        if (questions[currentQuestionindex].options[index].isCorrect)
        {
            currentScore += 10;
            scoreLabel.text = currentScore.ToString();
            scoreLabelAnimator.SetTrigger("score");
        }
        else
        {
            currentScore -= 10;
            scoreLabel.text = currentScore.ToString();
        }
        NextQuestion();
    }
}

[System.Serializable]
public struct QuestionData
{
    public string question;
    public Option[] options;
}


[System.Serializable]
public struct Option
{
    public string option;
    public bool isCorrect;
}