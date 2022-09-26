using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public static class GraphicVariables
{
    //Dither
    [HideInInspector] public static bool isDitherOn = false;
    [HideInInspector] public static float thresholdVal = 0.45f;
    [HideInInspector] public static float strengthVal = 0.45f;
    [HideInInspector] public static Slider tSliderValue = null;
    [HideInInspector] public static TMP_Text tSliderTextVal = null;
    [HideInInspector] public static Slider sSliderValue = null;
    [HideInInspector] public static TMP_Text sSliderTextVal = null;

    //Posterize
    [HideInInspector] public static int redComp = 0;
    [HideInInspector] public static TMP_InputField rCmpInp = null;
    [HideInInspector] public static int greenComp = 0;
    [HideInInspector] public static TMP_InputField gCmpInp = null;
    [HideInInspector] public static int blueComp = 0;
    [HideInInspector] public static TMP_InputField bCmpInp = null;
    [HideInInspector] public static bool isPosterizeOn = false;

    //Retro Colors
    [HideInInspector] public static bool isRColorsOn = false;
    [HideInInspector] public static int presetIndex = 0;
    [HideInInspector] public static TMP_Dropdown pDropC = null;

    //Retro Size
    [HideInInspector] public static bool isRSizeOn = false;
    [HideInInspector] public static int xInput = 1024;
    [HideInInspector] public static TMP_InputField xInp = null;
    [HideInInspector] public static int yInput = 1024;
    [HideInInspector] public static TMP_InputField yInp = null;
}

public class AdvancedGraphicsController : MonoBehaviour
{
    [Header("Dither settings")]
    [SerializeField] private Slider thresholdSlider = null;
    [SerializeField] private TMP_Text thresholdTextValue = null;
    private float defaultThresholdValue = 0.45f;
    private float tempThreshVal = 0.45f;
    
    [SerializeField] private Slider strengthSlider = null;
    [SerializeField] private TMP_Text strengthTextValue = null;
    private float defaultStrengthValue = 0.45f;
    private float tempStrVal = 0.45f;
    
    [SerializeField] private Toggle ditherToggle = null;
    private bool tmpDitherOnOff = false;

    [Header("Posterize settings")]
    [SerializeField] private TMP_InputField redCompInput = null;
    [SerializeField] private TMP_InputField greenCompInput = null;
    [SerializeField] private TMP_InputField blueCompInput = null;
    private int defaultCompValue = 0;
    private int tmpRedCompVal = 0;
    private int tmpGreenCompVal = 0;
    private int tmpBlueCompVal = 0;

    [SerializeField] private Toggle posterizeToggle = null;
    private bool tmpPosterizeOnOff = false;

    [Header("Retro Colors settings")]
    [SerializeField] private Toggle rColorsToggle = null;
    [SerializeField] private TMP_Dropdown presetDropdown = null;
    private int defaultPreset = 0;
    private int tmpPreset = 0;
    private bool tmpRColorsOnOff = false;

    [Header("Retro Size settings")]
    [SerializeField] private TMP_InputField xInput = null;
    [SerializeField] private TMP_InputField yInput = null;
    private int defaultCord = 1024;
    private int tmpXInput = 1024;
    private int tmpYInput = 1024;

    [SerializeField] private Toggle rSizeToggle = null;
    private bool tmpRSizeOnOff = false;

    [Header("Confirmation button")]
    [SerializeField] private GameObject confirmationPrompt = null;

    public void Start()
    {
        if(MenuVariables.wasThereStart == true)
        {
            //Dither
            if(GraphicVariables.isDitherOn == true)
            {
                thresholdSlider.value = GraphicVariables.tSliderValue.value;
                thresholdTextValue.text = GraphicVariables.tSliderTextVal.text;

                strengthSlider.value = GraphicVariables.sSliderValue.value;
                strengthTextValue.text = GraphicVariables.sSliderTextVal.text;

                ditherToggle.isOn = GraphicVariables.isDitherOn;
            }

            //Posterize
            if (GraphicVariables.isPosterizeOn == true)
            {
                redCompInput.text = GraphicVariables.rCmpInp.text;
                greenCompInput.text = GraphicVariables.gCmpInp.text;
                blueCompInput.text = GraphicVariables.bCmpInp.text;

                posterizeToggle.isOn = GraphicVariables.isPosterizeOn;
            }
            
            //Retro colors
            if(GraphicVariables.isRColorsOn == true)
            {
                presetDropdown.value = GraphicVariables.pDropC.value;
                rColorsToggle.isOn = GraphicVariables.isRColorsOn;
            }

            //Retro size
            if(GraphicVariables.isRSizeOn == true)
            {
                xInput.text = GraphicVariables.xInp.text;
                yInput.text = GraphicVariables.yInp.text;

                rSizeToggle.isOn = GraphicVariables.isRSizeOn;
            }
        }
    }

    public void ThresholdSet(float threshold)
    {
        tempThreshVal = threshold;
        thresholdTextValue.text = threshold.ToString("0.00");
    }

    public void StrengthSet(float strength)
    {
        tempStrVal = strength;
        strengthTextValue.text = strength.ToString("0.00");
    }

    public void DitherSet(bool dither)
    {
        tmpDitherOnOff = dither;
    }

    public void DitherAppply()
    {
        GraphicVariables.thresholdVal = tempThreshVal;
        GraphicVariables.tSliderValue = thresholdSlider;
        GraphicVariables.tSliderTextVal = thresholdTextValue;

        GraphicVariables.strengthVal = tempStrVal;
        GraphicVariables.sSliderValue = strengthSlider;
        GraphicVariables.sSliderTextVal = strengthTextValue;

        GraphicVariables.isDitherOn = tmpDitherOnOff;
 
        StartCoroutine(ConfirmationBox());
    }

    public void RedCompSet(string red)
    {
        tmpRedCompVal = int.Parse(red);
    }

    public void GreenCompSet(string green)
    {
        tmpGreenCompVal = int.Parse(green);
    }

    public void BlueCompSet(string blue)
    {
        tmpBlueCompVal = int.Parse(blue);
    }
    
    public void PosterizeSet(bool posterize)
    {
        tmpPosterizeOnOff = posterize;
    }

    public void PosterizeApply()
    {
        GraphicVariables.redComp = tmpRedCompVal;
        GraphicVariables.rCmpInp = redCompInput;

        GraphicVariables.greenComp = tmpGreenCompVal;
        GraphicVariables.gCmpInp = greenCompInput;

        GraphicVariables.blueComp = tmpBlueCompVal;
        GraphicVariables.bCmpInp = blueCompInput;

        GraphicVariables.isPosterizeOn = tmpPosterizeOnOff;

        StartCoroutine(ConfirmationBox());
    }

    public void RColorsSet(bool rColors)
    {
        tmpRColorsOnOff = rColors;
    }

    public void PresetSet(int presetIndex)
    {
        tmpPreset = presetIndex;
    }

    public void RColorsApply()
    {
        GraphicVariables.isRColorsOn = tmpRColorsOnOff;
        GraphicVariables.presetIndex = tmpPreset;
        GraphicVariables.pDropC = presetDropdown;

        StartCoroutine(ConfirmationBox());
    }

    public void XSet(string x)
    {
        tmpXInput = int.Parse(x);
    }

    public void YSet(string y)
    {
        tmpYInput = int.Parse(y);
    }

    public void RSizeSet(bool rSize)
    {
        tmpRSizeOnOff = rSize;
    }

    public void RSizeApply()
    {
        GraphicVariables.xInput = tmpXInput;
        GraphicVariables.xInp = xInput;

        GraphicVariables.yInput = tmpYInput;
        GraphicVariables.yInp = yInput;

        GraphicVariables.isRSizeOn = tmpRSizeOnOff;

        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if(MenuType == "Dither")
        {
            GraphicVariables.strengthVal = defaultStrengthValue;
            strengthSlider.value = defaultStrengthValue;
            strengthTextValue.text = defaultStrengthValue.ToString("0.00");

            GraphicVariables.thresholdVal = defaultThresholdValue;
            thresholdSlider.value = defaultThresholdValue;
            thresholdTextValue.text = defaultThresholdValue.ToString("0.00");

            ditherToggle.isOn = false;
            GraphicVariables.isDitherOn = false;

            StartCoroutine(ConfirmationBox());
        }

        if(MenuType == "Posterize")
        {
            GraphicVariables.redComp = defaultCompValue;
            redCompInput.text = defaultCompValue.ToString("0");

            GraphicVariables.greenComp = defaultCompValue;
            greenCompInput.text = defaultCompValue.ToString("0");

            GraphicVariables.blueComp = defaultCompValue;
            blueCompInput.text = defaultCompValue.ToString("0");

            GraphicVariables.isPosterizeOn = false;
            posterizeToggle.isOn = false;

            StartCoroutine(ConfirmationBox());
        }

        if(MenuType == "Retro Colors")
        {
            GraphicVariables.presetIndex = defaultPreset;
            presetDropdown.value = defaultPreset;

            rColorsToggle.isOn = false;
            GraphicVariables.isRColorsOn = false;

            StartCoroutine(ConfirmationBox());
        }

        if(MenuType == "Retro Size")
        {
            GraphicVariables.xInput = defaultCord;
            xInput.text = defaultCord.ToString("0000");

            GraphicVariables.yInput = defaultCord;
            yInput.text = defaultCord.ToString("0000");

            GraphicVariables.isRSizeOn = false;
            rSizeToggle.isOn = false;
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmationPrompt.SetActive(false);
    }
}
