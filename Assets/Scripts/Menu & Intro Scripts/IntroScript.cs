using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    public string message = "Upon the seven seas, Davy Jones discovered an island of gold. He pillaged it all and loaded it upon his ship in hopes of infinite riches. However, fate had other plans. As his ship sunk beneath the waves in a monsterous storm, his gold was never seen again... UNTIL! A single treasure chest washed ashore, and now The Great Diving Era has begun...";
    public float typeSpeed = 0.05f; // seconds between each letter
    public string nextScene = "WorldHub"; // name of scene to load
    public GameObject enterPrompt; // prompt the player to press enter after dialogue finishes

    private int randInt;
    private int charCounter = 0;
    private int modChar = 3;

    TMP_Text textBox;
    bool doneTyping = false;

    public GameObject introComic;

    void Start()
    {
        textBox = GetComponent<TMP_Text>();
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText() {
        textBox.text = "";

        foreach (char letter in message) {
            textBox.text += letter;

            charCounter++; // prevent sound from being played too often
            if (typeSpeed >= 0.05f && (charCounter % modChar == 0)) RandomSound();
            yield return new WaitForSeconds(typeSpeed);
        }
        doneTyping = true;
    }

    void Update()
    {
        if (doneTyping)
        {
            enterPrompt.SetActive(true);
        }

        if (doneTyping && Keyboard.current.enterKey.wasPressedThisFrame) {
            introComic.SetActive(true);
            gameObject.SetActive(false);
        }

        if (!doneTyping && Keyboard.current.enterKey.wasPressedThisFrame) {
            typeSpeed -= 0.45f;
        }
    }

    void RandomSound()
    {
        int temp = Random.Range(0, audioSources.Length);

        // prevent same audio from being played multiple times in a row
        while(temp == randInt) temp = Random.Range(0, audioSources.Length - 1);

        randInt = temp;

        audioSources[randInt].Play();
        Debug.Log("Audio Source " + randInt + " played.");
    }
}
