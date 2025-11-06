using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    [Header("Linked Components")]
    [SerializeField] private TextMeshProUGUI speakerNameBox;
    [SerializeField] private TextMeshProUGUI dialogueTextBox;
    [SerializeField] private GameObject dialogueGameObject;


    [Header("Text Configuration")]
    [SerializeField] private float typingSpeed = 0.03f;


    [Header("Dialogue Status")]
    private bool isTyping = false;
    public bool dialogueFinished = false;


    [Header("Dialogue")]
    public DialogueLine[] dialogueLines; // à voir si possible de repasser cette variable en privé...

    private int currentIndex;
    private Coroutine typingCoroutine;
    private bool justStarted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (justStarted)
            {
                justStarted = false;
                return;
            }
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                ShowFullLine(dialogueLines[currentIndex]);
                isTyping = false;
            }
            else
            {
                currentIndex++;

                if (currentIndex < dialogueLines.Length)
                {
                    typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
                }
                else
                {
                    dialogueTextBox.text = "";
                    speakerNameBox.text = "";
                    dialogueFinished = true;
                    dialogueGameObject.SetActive(false);
                }
            }
        }
    }

    public void StartDialogue(DialogueLine[] newLines)
    {
        dialogueGameObject.SetActive(true);
        dialogueFinished = false;
        dialogueLines = newLines;
        currentIndex = 0;
        justStarted = true;
        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
    }

    IEnumerator TypeLine(DialogueLine line)
    {
        isTyping = true;

        dialogueTextBox.text = "";
        speakerNameBox.text = line.speakerName;

        foreach (char c in line.dialogueText)
        {
            dialogueTextBox.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void ShowFullLine(DialogueLine line)
    {
        dialogueTextBox.text = line.dialogueText;
        speakerNameBox.text = line.speakerName;
    }
}
