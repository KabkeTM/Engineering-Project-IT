/*
 * #if UNITY_EDITOR
using Assets.Scripts.Cam.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;


public class GameSceneControllerWithEditorAPI : MonoBehaviour
{
    private GameObject varGameObject;
    private GameObject presetGameObj;
    private Object targetObject;
    private Preset importPreset;
    private Object _targetComponent;

    public void DitherSwitch(GameObject varGameObject)
    {
        if (GraphicVariables.isDitherOn == true)
        {
            varGameObject.GetComponent<Dither>().enabled = true;
        }
        else
        {
            varGameObject.GetComponent<Dither>().enabled = false;
        }
    }

    public void RColorsSwitch(GameObject varGameObject, GameObject presetGameObj, Object targetObject)
    {
        if (GraphicVariables.isRColorsOn == true)
        {
            varGameObject.GetComponent<RetroPixelPalette>().enabled = true;
            targetObject = varGameObject.GetComponent<RetroPixelPalette>();
            switch (GraphicVariables.presetIndex)
            {
                case 0:
                    presetGameObj = GameObject.Find("RColors_Green");
                    _targetComponent = presetGameObj.GetComponent<RetroPixelPalette>();
                    importPreset = new Preset(_targetComponent);
                    importPreset.ApplyTo(targetObject);
                    break;
                case 1:
                    presetGameObj = GameObject.Find("RColors_Red");
                    _targetComponent = presetGameObj.GetComponent<RetroPixelPalette>();
                    importPreset = new Preset(_targetComponent);
                    importPreset.ApplyTo(targetObject);
                    break;
                default:
                    break;
            }
        }
        else
        {
            varGameObject.GetComponent<RetroPixelPalette>().enabled = false;
        }
    }

    private void Start()
    {
        varGameObject = GameObject.Find("FPS Player/PlayerCamera");



        DitherSwitch(varGameObject);

        RColorsSwitch(varGameObject, presetGameObj, targetObject);

    }
}
#endif
*/