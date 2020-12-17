using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class PlayerTimeController : MonoBehaviour
{
    public float maxTimeJuice = 5;
    public float coolDownTime = 30;
    private float currentTimeJuice;

    public AudioClip timeSlowSound;
    private AudioSource audioSource;

    public TMP_Text timeSlowText;

    public PostProcessVolume ppVolume;
    public PostProcessProfile defultppfx;
    public PostProcessProfile timeSlowppfx;

    public bool timeIsSlow = false;

    void Start()
    {
        currentTimeJuice = maxTimeJuice;

        if (gameObject.GetComponent<AudioSource>())
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SlowDownTime();
        }

        if(!timeIsSlow && currentTimeJuice < maxTimeJuice)
        {
            currentTimeJuice += (maxTimeJuice / coolDownTime) * Time.deltaTime;
        }
        else if (!timeIsSlow && currentTimeJuice > maxTimeJuice)
        {
            currentTimeJuice = maxTimeJuice;

            UpdateSlowText();
        }

        if (timeIsSlow && currentTimeJuice > 0)
        {
            currentTimeJuice -= Time.deltaTime;
        }
        else if (currentTimeJuice < 0 || (currentTimeJuice == 0 & Time.timeScale == 0.5f))
        {
            currentTimeJuice = 0;

            Time.timeScale = 1f;

            ppVolume.profile = defultppfx;

            if (audioSource)
            {
                audioSource.Stop();
            }

            timeIsSlow = false;
        }

        if(currentTimeJuice < maxTimeJuice)
        {
            UpdateSlowText();
        }
    }

    void UpdateSlowText()
    {
        float roundedNumber;
        roundedNumber = Mathf.Round(currentTimeJuice * 100) / 100;

        timeSlowText.text = "Time Slow = " + roundedNumber + " / " + maxTimeJuice;
    }

    void SlowDownTime()
    {
        if(currentTimeJuice == maxTimeJuice)
        {
            Time.timeScale = 0.5f;
            timeIsSlow = true;

            ppVolume.profile = timeSlowppfx;

            PlaySound();
        }
        else if (currentTimeJuice < maxTimeJuice)
        {
            if (!timeIsSlow)
            {
                Time.timeScale = 0.5f;
                timeIsSlow = true;

                ppVolume.profile = timeSlowppfx;

                PlaySound();
            }
            else
            {
                Time.timeScale = 1f;
                timeIsSlow = false;

                if (audioSource)
                {
                    audioSource.Stop();
                }

                ppVolume.profile = defultppfx;
            }
        }
    }

    public void PlaySound()
    {
        if (audioSource && timeIsSlow)
        {
            audioSource.clip = timeSlowSound;

            audioSource.Play();
        }
    }

    public void AddTimeJuice()
    {
        currentTimeJuice += 2;

        if(currentTimeJuice > maxTimeJuice)
        {
            currentTimeJuice = maxTimeJuice;
        }

        UpdateSlowText();
    }
}
