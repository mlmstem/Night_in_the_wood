using UnityEngine;
[ExecuteAlways]
// Modified code from unity tutorial: https://www.youtube.com/watch?v=m9hj9PdO328
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField] private float TransitionDuration = 300.0f; // 300 seconds = 5 minutes
    private float currentTime = 0.0f;
    private float phaseOffset = 0.5f; // Start halfway through the cycle

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            // Increase currentTime gradually over 5 minutes
            currentTime += Time.deltaTime;

            // Calculate the timePercent based on the TransitionDuration
            float timePercent = Mathf.Clamp01(currentTime / TransitionDuration);

            // Ensure it's always between 0-24
            timePercent %= 1.0f;

            UpdateLighting(timePercent, phaseOffset);
        }
        else
        {
            UpdateLighting(currentTime / TransitionDuration, phaseOffset);
        }
    }

    private void UpdateLighting(float timePercent, float offset)
    {
        // Calculate the adjusted timePercent with the phase offset
        float adjustedTimePercent = (timePercent + offset) % 1.0f;

        // Set ambient light
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(adjustedTimePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(adjustedTimePercent);

            // Rotate the directional light
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((adjustedTimePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
