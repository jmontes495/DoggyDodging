using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }

    [SerializeField] private Coin coin;
    [SerializeField] private GameObject dog;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject goodEnding;
    [SerializeField] private GameObject badEnding;
    [SerializeField] private TextMeshProUGUI coinCount;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip woof;
    [SerializeField] private AudioClip bling;
    private Vector3 dogPosition;
    private Vector3 ballPosition;
    private int count;

    enum GameState { Intro, Playing, GoodEnding, BadEnding }
    private GameState currentState;
    public bool ShouldMove { get { return currentState == GameState.Playing; } }
    public bool InMouth { get { return currentState == GameState.BadEnding; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            dogPosition = dog.transform.position;
            ballPosition = ball.transform.position;
            intro.SetActive(true);
            goodEnding.SetActive(false);
            badEnding.SetActive(false);
            currentState = GameState.Intro;
        }
        else
            DestroyImmediate(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                case GameState.Intro:
                    currentState = GameState.Playing;
                    intro.SetActive(false);
                    coinCount.text = count + "";
                    break;
                case GameState.BadEnding:
                    count = 0;
                    coinCount.text = count + "";
                    currentState = GameState.Playing;
                    badEnding.SetActive(false);
                    dog.transform.position = dogPosition;
                    ball.transform.position = ballPosition;
                    coin.Restart();
                    break;
                case GameState.GoodEnding:
                    currentState = GameState.Playing;
                    goodEnding.SetActive(false);
                    break;
            }
        }
    }

    public void IncreaseCount()
    {
        count++;
        coinCount.text = count + "";
        audioSource.PlayOneShot(bling);

        if (count == 10)
        {
            currentState = GameState.GoodEnding;
            goodEnding.SetActive(true);
        }
    }

    public void Lose()
    {
        coinCount.text = count + "";
        currentState = GameState.BadEnding;
        badEnding.SetActive(true);
        audioSource.PlayOneShot(woof);
    }
}
