using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource backgroundMusicAudioSource;
    public AudioClip normalAmbienceMusic;
    public AudioClip parryingAmbienceMusic;
    public int initialLifes = 3;
    public bool startGame = false;

    public event Action OnGameStart;
    public event Action OnGameStop;

    public event Action<bool> OnDroneActivation;
    public event Action<int> OnParriedShot;
    public event Action<int> OnLifeLost;

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

    public bool GameStarted() => gameStarted;

    public void ProjectileParried()
    {
        parriedShots++;
        OnParriedShot.Invoke(parriedShots);
    }

    public void PlayerHit()
    {
        lifes--;
        OnLifeLost.Invoke(lifes);

        if (lifes <= 0) 
            StopGame();
    }


    private void _OnGameStart()
    {
        gameStarted = true;
        backgroundMusicAudioSource.Stop();
        backgroundMusicAudioSource.PlayOneShot(parryingAmbienceMusic);
        parriedShots = 0;
        lifes = initialLifes;

        OnDroneActivation.Invoke(true);
        OnParriedShot.Invoke(parriedShots);
        OnLifeLost.Invoke(lifes);
    }

    private void _OnGameStop()
    {
        gameStarted = false;
        backgroundMusicAudioSource.Stop();
        backgroundMusicAudioSource.PlayOneShot(normalAmbienceMusic);
        OnDroneActivation.Invoke(false);
    }

}
