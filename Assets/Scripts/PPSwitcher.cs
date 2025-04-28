using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ToggleVolumeOverride : MonoBehaviour
{
    public Volume globalVolume;
    private Fog fog;
    public bool isFogEnabled = true;

    private void Start()
    {
        if (globalVolume.profile.TryGet(out fog))
        {
            fog.active = isFogEnabled;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isFogEnabled = !isFogEnabled;
            if (fog != null)
            {
                fog.active = isFogEnabled;
            }
        }
    }
}