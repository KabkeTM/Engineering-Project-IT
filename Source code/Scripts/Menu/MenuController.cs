using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public static class MenuVariables
{
    [HideInInspector] public static int mainSen = 4;
    [HideInInspector] public static bool wasThereStart = false;

    //Graphics
    [HideInInspector] public static Slider brightSl = null;
    [HideInInspector] public static TMP_Text brightTexSl = null;

    [HideInInspector] public static TMP_Dropdown qDrop;

    [HideInInspector] public static TMP_Dropdown rDrop;

    [HideInInspector] public static bool _isFullScreen = false;

    [HideInInspector] public static bool graphicsWasApplied = false;

    [HideInInspector] public static int resIndex = 1;

    //Audio
    [HideInInspector] public static Slider volSl = null;
    [HideInInspector] public static TMP_Text volTexSl;

    [HideInInspector] public static bool volumeWasApplied = false;

    //Gameplay
    [HideInInspector] public static Slider senSl = null;
    [HideInInspector] public static TMP_Text senTexSl;

    [HideInInspector] public static bool _isInvertY = false;

    [HideInInspector] public static bool gameplayWasApplied = false;
}

public class MenuController : MonoBehaviour
{
    [Header("Graphics settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brigtnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1f;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    
    private float _brightnessLevel;

    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text senTextValue = null;
    [SerializeField] private Slider senSlider = null;
    [SerializeField] private int defaultSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;
    

    [Header("Volume settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 50f;

    [Header("Confirmation button")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [HideInInspector] public string _sceneSelectLevel;

    //Resolution method made using Start()
    private void Start()
    {
        ResolutionStart();

        if (MenuVariables.wasThereStart == false)
        {
            GraphicsStartApply();
            MenuVariables.wasThereStart = true;
        }
        else
        {
            //Graphics
            if (MenuVariables.graphicsWasApplied == true)
            {
                brightnessSlider.value = MenuVariables.brightSl.value;
                brigtnessTextValue.text = MenuVariables.brightTexSl.text;

                qualityDropdown.value = MenuVariables.qDrop.value;

                resolutionDropdown.value = MenuVariables.rDrop.value;

                fullScreenToggle.isOn = MenuVariables._isFullScreen;
            }

            //Gameplay
            if (MenuVariables.gameplayWasApplied == true)
            {
                senSlider.value = MenuVariables.senSl.value;
                senTextValue.text = MenuVariables.senTexSl.text;

                invertYToggle.isOn = MenuVariables._isInvertY;
            }

            //Audio
            if(MenuVariables.volumeWasApplied == true)
            {
                volumeSlider.value = MenuVariables.volSl.value;
                volumeTextValue.text = MenuVariables.volTexSl.text;
            }     
        }
    }

    public void ResolutionStart()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();


        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        MenuVariables.resIndex = resolutionIndex;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Scene select methods
    public void SceneSelectDialogYes(string sceneSelectLevel)
    {
        SceneManager.LoadScene(sceneSelectLevel);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    //Volume methods
    public void VolumeSet(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        MenuVariables.volSl = volumeSlider;
        MenuVariables.volTexSl = volumeTextValue;

        MenuVariables.volumeWasApplied = true;

        StartCoroutine(ConfirmationBox());
    }

    //Gameplay methods
    public void SetSensitivity(float sensitivity)
    {
        MenuVariables.mainSen = Mathf.RoundToInt(sensitivity);
        senTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            MenuVariables._isInvertY = true;
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }

        PlayerPrefs.SetInt("masterSen", MenuVariables.mainSen);
        MenuVariables.senSl = senSlider;
        MenuVariables.senTexSl = senTextValue;

        MenuVariables.gameplayWasApplied = true;

        StartCoroutine(ConfirmationBox());
    }

    //Reset button method used in every individual popup window
    public void ResetButton(string MenuType)
    {
        if(MenuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brigtnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;
            MenuVariables._isFullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;

            MenuVariables.graphicsWasApplied = false;

            GraphicsApply();
        }

        if(MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");

            MenuVariables.volumeWasApplied = false;

            VolumeApply();
        }

        if(MenuType == "Gameplay")
        {
            senTextValue.text = defaultSen.ToString("0");
            senSlider.value = defaultSen;
            MenuVariables.mainSen = defaultSen;
            invertYToggle.isOn = false;
            MenuVariables._isInvertY = false;

            MenuVariables.gameplayWasApplied = false;

            GameplayApply();
        }

    }

    //Graphic methods
    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brigtnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        MenuVariables._isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsStartApply()
    {
        brightnessSlider.value = defaultBrightness;
        brigtnessTextValue.text = defaultBrightness.ToString("0.0");

        qualityDropdown.value = 1;
        QualitySettings.SetQualityLevel(1);

        fullScreenToggle.isOn = false;
        Screen.fullScreen = false;

        Resolution currentResolution = Screen.currentResolution;
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
        resolutionDropdown.value = resolutions.Length;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        MenuVariables.brightSl = brightnessSlider;
        MenuVariables.brightTexSl = brigtnessTextValue;

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);
        MenuVariables.qDrop = qualityDropdown;

        PlayerPrefs.SetInt("masterFullscreen", (MenuVariables._isFullScreen ? 1 : 0));
        Screen.fullScreen = MenuVariables._isFullScreen;

        MenuVariables.rDrop = resolutionDropdown;

        MenuVariables.graphicsWasApplied = true;

        StartCoroutine(ConfirmationBox());

    }

    //Confirmation method
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
