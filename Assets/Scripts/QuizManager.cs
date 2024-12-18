using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;           // Question text
        public string[] options;              // Options for the question
        public int correctAnswerIndex;        // Index of the correct answer
    }

    public Question[] questions;             // Array to hold quiz questions
    private int currentQuestionIndex = 0;    // Index of the current question

    public TextMeshProUGUI questionTextUI;   // UI Text for the question
    public UnityEngine.UI.Button[] optionButtons;  // Buttons for MCQ options
    public Color correctColor = Color.green; // Color to indicate correct answer
    public Color wrongColor = Color.red;     // Color to indicate wrong answer

    private void Start()
    {
        if (questions.Length == 0)
        {
            Debug.LogError("No questions are set in the QuizManager!");
            return;
        }

        LoadQuestion();
    }

    /// <summary>
    /// Loads the current question and its options.
    /// </summary>
    void LoadQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            // Display the current question
            questionTextUI.text = questions[currentQuestionIndex].questionText;

            // Load options dynamically
            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (i < questions[currentQuestionIndex].options.Length)
                {
                    int index = i; // Capture index for callback
                    optionButtons[i].gameObject.SetActive(true);
                    optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[currentQuestionIndex].options[i];
                    optionButtons[i].onClick.RemoveAllListeners();
                    optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
                    optionButtons[i].image.color = Color.white; // Reset button color
                }
                else
                {
                    // Hide unused buttons
                    optionButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            EndQuiz();
        }
    }

    /// <summary>
    /// Called when an option is selected.
    /// </summary>
    /// <param name="selectedIndex">Index of the selected option</param>
    void OnOptionSelected(int selectedIndex)
    {
        StartCoroutine(HandleAnswer(selectedIndex));
    }

    /// <summary>
    /// Handles the answer logic and displays the correct answer if necessary.
    /// </summary>
    /// <param name="selectedIndex">Selected answer index</param>
    IEnumerator HandleAnswer(int selectedIndex)
    {
        int correctIndex = questions[currentQuestionIndex].correctAnswerIndex;

        // Highlight selected button
        if (selectedIndex == correctIndex)
        {
            Debug.Log("Correct Answer!");
            optionButtons[selectedIndex].image.color = correctColor;
        }
        else
        {
            Debug.Log("Wrong Answer!");
            optionButtons[selectedIndex].image.color = wrongColor;
            optionButtons[correctIndex].image.color = correctColor; // Highlight correct answer
        }

        // Wait for 1.5 seconds before loading the next question
        yield return new WaitForSeconds(1.5f);

        // Move to the next question
        currentQuestionIndex++;
        LoadQuestion();
    }

    /// <summary>
    /// Ends the quiz and displays a final message.
    /// </summary>
    void EndQuiz()
    {
        questionTextUI.text = "Quiz Completed!";
        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false); // Hide all buttons
        }
    }
}
