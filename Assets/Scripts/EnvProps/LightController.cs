using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightingControl : MonoBehaviour
{
    public Light2D mainLight;  // The main directional light in the scene
    public float flickerSpeed = 1f;  // Speed of the light flicker
    public float maxIntensity = 2f;  // Maximum intensity of the light
    public float minIntensity = 0.5f;  // Minimum intensity of the light
    public float shadowIntensity = 0.8f; // Shadow intensity
    private float targetIntensity;

    private void Start()
    {
        // Set initial intensity to a random value between min and max
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Update()
    {
        // Flicker effect: Change the light intensity over time
        mainLight.intensity = Mathf.Lerp(mainLight.intensity, targetIntensity, Time.deltaTime * flickerSpeed);
        
        // Randomly change target intensity for flickering effect
        if (Time.time % 1f < 0.1f)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }

        // Adjust shadow intensity (if using soft shadows)
        SetShadowIntensity(shadowIntensity);
    }

    void SetShadowIntensity(float intensity)
    {
        // Access the Universal Render Pipeline's settings to modify shadows
        var settings = mainLight.GetComponent<Light>();
        if (settings)
        {
            // Shadow intensity scaling
            settings.shadowStrength = intensity;
        }
    }
}
