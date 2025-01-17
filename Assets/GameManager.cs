using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI droneActivationStatusText;
    public TextMeshProUGUI parriedShotsNumberText;
    public TextMeshProUGUI lifesLeftNumberText;
    public AudioSource backgroundMusicAudioSource;
    public AudioClip normalAmbienceMusic;
    public AudioClip parryingAmbienceMusic;
    public int initialLifes = 3;
    public bool startGame = false;

    public event Action OnGameStart;
    public event Action OnGameStop;

    private static GameManager instance;
    private int lifes = 3;
    private int parriedShots = 0;
    private bool gameStarted = false;

    public static GameManager Instance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnGameStart += () => _OnGameStart();
        OnGameStop += () => _OnGameStop();
        lifes = initialLifes;
    }


    // Update is called once per frame
    void Update()
    {
        if (startGame && !gameStarted)
        {
            startGame = false;
            StartGame();
        }
    }

    public void StartGame()
    {
        OnGameStart.Invoke();
    }

    public void StopGame()
    {
        OnGameStop.Invoke();
    }

    public void ProjectileParried()
    {
        parriedShots++;
        parriedShotsNumberText.text = parriedShots.ToString();
    }

    public void PlayerHit()
    {
        lifes--;
        lifesLeftNumberText.text = lifes.ToString();

        if (lifes <= 0) 
            StopGame();
    }

    public bool GameStarted() => gameStarted;

    private void _OnGameStart()
    {
        gameStarted = true;
        backgroundMusicAudioSource.Stop();
        backgroundMusicAudioSource.PlayOneShot(parryingAmbienceMusic);
        parriedShots = 0;
        lifes = initialLifes;
        droneActivationStatusText.text = "True";
        parriedShotsNumberText.text = "0";
        lifesLeftNumberText.text = lifes.ToString();
    }

    private void _OnGameStop()
    {
        gameStarted = false;
        backgroundMusicAudioSource.Stop();
        backgroundMusicAudioSource.PlayOneShot(normalAmbienceMusic);
        droneActivationStatusText.text = "False";
    }

}
