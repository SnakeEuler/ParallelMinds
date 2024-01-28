/**
 */
#include  "UnityCG.cginc"
// Function to create dynamic flickering effect
float dynamic_flicker(float3 position, float time)
{
	// Parameters for flicker effect
	const float speed = 5.0;     // Speed of flickering
	const float intensity = 0.5; // Intensity of flicker effect

	// Generating noise based on position and time
	float noise_value = UNITY_NOISE(position * speed + time);

	// Modifying the noise value to create a flickering effect
	float flicker = abs(sin(time * speed)) * noise_value;

	// Returning the flickered intensity
	return lerp(1.0, intensity, flicker);
}
