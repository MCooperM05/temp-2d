using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class IntroScript : MonoBehaviour
{
    public string message = "Upon the seven seas, Davy Jones discovered an island of gold. He pillaged it all and loaded it upon his ship in hopes of infinite riches. However, fate had other plans. As his ship sunk beneath the waves in a monsterous storm, his gold was never seen again... UNTIL! A single treasure chest washed ashore, and now The Great Diving Era has begun...";
    public float typeSpeed = 0.05f; // seconds between each letter
    public string nextScene = "prototype-boss-fight"; // name of scene to load

    TMP_Text textBox;
    bool doneTyping = false;

    void Start()
    {
        textBox = GetComponent<TMP_Text>();
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText() {
        textBox.text = "";

        foreach (char letter in message) {
            textBox.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        doneTyping = true;
    }

    void Update()
    {
        if (doneTyping && Keyboard.current.enterKey.wasPressedThisFrame) {
            SceneManager.LoadScene(nextScene);
        }

        if (!doneTyping && Keyboard.current.enterKey.wasPressedThisFrame) {
            typeSpeed -= 0.05f;
        }
    }
}
