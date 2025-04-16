using System;
using MiniGame.LightsSwitching;
using UnityEngine;

public class ResetLightGame : MonoBehaviour
{
    private void OnMouseDown()
    {
        LightsGame.GetInstance().ResetLights();
    }
}
