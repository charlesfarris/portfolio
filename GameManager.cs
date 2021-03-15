// Charles Farris
// GameManager.cs for Unity
// Project Planet Discovery
// Level change and audio functions

// Built in classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Extend built in GameManager class
public class GameManager : MonoBehaviour
{
    // Global public variables that can be changed in the Unity editor
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip pickUpSound;
    [SerializeField] AudioClip levelUpSound;

    // Untagged variables are private
    AudioSource worldAudio;

    // Called before the first frame
    void Start()
    {
        // Initialize worldAudio
        worldAudio = GetComponent<AudioSource>();
	}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Lost()
    {
        // Reload the level after a delay
        Invoke("ReLoadLevel", levelLoadDelay); 
    }

    private void ReLoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }


    public void LevelUp()
    {
        worldAudio.PlayOneShot(levelUpSound);
        Invoke("LoadNextLevel", levelLoadDelay); 
    }

    public void ItemPickUp ()
    {
        worldAudio.PlayOneShot(pickUpSound);
    }

}
