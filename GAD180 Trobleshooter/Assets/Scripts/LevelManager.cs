using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject deathMenu;
    public GameObject pauseMenu;
    public GameObject player;
    public GameObject playerCamWeaponPoint;

    public bool isPaused;
    public bool playerIsDead;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerIsDead)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);

            isPaused = true;
            if (player)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                playerCamWeaponPoint.SetActive(false);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);

            isPaused = false;
            if (player)
            {
                player.GetComponent<PlayerMovement>().enabled = true;
                playerCamWeaponPoint.SetActive(true);
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0;
        deathMenu.SetActive(true);

        if (player)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            playerCamWeaponPoint.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerIsDead = true;
    }
}
