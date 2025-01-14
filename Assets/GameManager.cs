using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public TextMeshProUGUI droneActivationStatusText;
    public TextMeshProUGUI parriedShotsNumberText;
    public TextMeshProUGUI lifesLeftNumberText;
    public AudioSource backgroundMusicAudioSource;
    public AudioClip normalAmbienceMusic;
    public AudioClip parryingAmbienceMusic;

    public event Action OnGameStart;
    public event Action OnGameStop;

    public int initialLifes = 3;
    private int lifes = 3;
    private int parriedShots = 0;

    public static GameManager Instance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(this);

        instance = this;

        OnGameStart += () => _OnGameStart();
        OnGameStop += () => _OnGameStop();
        lifes = initialLifes;
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

    private void _OnGameStart()
    {
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
        backgroundMusicAudioSource.Stop();
        backgroundMusicAudioSource.PlayOneShot(normalAmbienceMusic);
        droneActivationStatusText.text = "False";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
