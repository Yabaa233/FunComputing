using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] correctAnswers;
    }

    public Question[] questions;
    public TMP_Text questionText;
    public TMP_Text scoreText;

    public GameObject correctImage;
    public GameObject incorrectImage;

    private int currentQuestionIndex;
    private int score;
    private Coroutine feedbackCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        currentQuestionIndex = 0;
        score = 0;
        ShowQuestion();
        correctImage.SetActive(false);
        incorrectImage.SetActive(false);
    }

    private void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex].questionText;
        }
        else
        {
            Debug.Log("All Questions finished！");
        }
    }

    private void ShowScore()
    {
        scoreText.text = score.ToString();
    }

    public void OnImageClick(GameObject selectedImage)
    {
        string selectedAnswer = selectedImage.name;
        string[] correctAnswers = questions[currentQuestionIndex].correctAnswers;

        bool isCorrect = System.Array.Exists(correctAnswers, answer => answer == selectedAnswer);

        if (isCorrect)
        {
            score++;
            Debug.Log("Right！Cur Score：" + score);
            ShowFeedback(correctImage);
        }
        else
        {
            Debug.Log("Wrong！");
            ShowFeedback(incorrectImage);
        }

        ShowScore();
    }

    private void ShowFeedback(GameObject feedbackImage)
    {
        // 如果已经有一个反馈图片在显示中，则取消它
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
            correctImage.SetActive(false);
            incorrectImage.SetActive(false);
        }

        // 启动新的反馈协程
        feedbackImage.SetActive(true);
        feedbackCoroutine = StartCoroutine(HideFeedbackAfterDelay(feedbackImage, 0.5f));
    }

    private IEnumerator HideFeedbackAfterDelay(GameObject feedbackImage, float delay)
    {
        yield return new WaitForSeconds(delay);
        feedbackImage.SetActive(false);
        feedbackCoroutine = null;
    }
}
