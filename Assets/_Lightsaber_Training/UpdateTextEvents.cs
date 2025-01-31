using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTextEvents : MonoBehaviour
{
    public TextMeshProUGUI droneActivationStatusText;
    public TextMeshProUGUI parriedShotsNumberText;
    public TextMeshProUGUI lifesLeftNumberText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance().OnDroneActivation += OnDroneActivationChanged;
        GameManager.Instance().OnParriedShot += OnParriedShot;
        GameManager.Instance().OnLifeLost += OnLifesLeftChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance().OnDroneActivation -= OnDroneActivationChanged;
        GameManager.Instance().OnParriedShot -= OnParriedShot;
        GameManager.Instance().OnLifeLost -= OnLifesLeftChanged;
    }

    private void OnDroneActivationChanged(bool droneStatus)
    {
        droneActivationStatusText.text = droneStatus.ToString();
    }

    private void OnParriedShot(int newAmountOfParriedShots)
    {
        parriedShotsNumberText.text = newAmountOfParriedShots.ToString();
    }

    private void OnLifesLeftChanged(int newLifesAmount)
    {
        lifesLeftNumberText.text = newLifesAmount.ToString();
    }
}
