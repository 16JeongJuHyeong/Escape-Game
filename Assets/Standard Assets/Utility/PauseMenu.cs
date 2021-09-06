using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool is_Pause = false;

    [SerializeField] private GameObject go_BaseUi;
    [SerializeField] private GameObject soundMenu;

    #region Default Values
    [Header("Default Menu Values")]
    [SerializeField] private float defaultVolume;
    #endregion

    #region Slider Linking
    [Header("Menu Sliders")]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider volumeSlider;
    #endregion

    // Update is called once per frame
    void Update()
    {
        TryOpenPauseMenu();
    }
    private void TryOpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            is_Pause = !is_Pause;

            if (is_Pause)
                CallMenu();
            else
                CloseMenu();
        }
    }

    private void CallMenu()
    {
        // Inventory.inventoryActivated = !Inventory.inventoryActivated;
        go_BaseUi.SetActive(true);
    }

    private void CloseMenu()
    {
        // Inventory.inventoryActivated = !Inventory.inventoryActivated;
        go_BaseUi.SetActive(false);
    }

    #region Menu Mouse Clicks
    public void MouseClick(string buttonType)
    {
        if(buttonType == "Resume")
        {
            is_Pause = false;
            go_BaseUi.SetActive(false);
        }
        if (buttonType == "Sound")
        {
            soundMenu.SetActive(true);
        }
        if (buttonType == "Exit")
        {
            Application.Quit();
        }
    }
    #endregion

    public void VolumeSlider(float volume)
    {
        AudioListener.volume = volume;
        volumeText.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        Debug.Log(PlayerPrefs.GetFloat("masterVolume"));
    }

    #region ResetButton
    public void ResetButton(string GraphicsMenu)
    {
        if (GraphicsMenu == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeText.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }
    #endregion
    public void GoBackToPauseMenu()
    {
        soundMenu.SetActive(false);
    }
}