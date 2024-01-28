#include "UnityCG.cginc"
#include "Library/PackageCache/com.unity.textmeshpro@3.0.6/Editor Resources/Shaders/TMP_Properties.cginc"


float2 uv = IN.uv_MainTex; // Your main texture coordinates
float aberrationOffset = 0.01; // The amount of RGB split

// Offset the UVs slightly for each color channel
float2 uvR = uv + float2(aberrationOffset, 0);
float2 uvG = uv;
float2 uvB = uv - float2(aberrationOffset, 0);

// Sample the texture with the offset UVs
float  colorR = tex2D(_MainTex, uvR).r;
float colorG = tex2D(_MainTex, uvG).g;
float colorB = tex2D(_MainTex, uvB).b;

// Combine the colors again
float3 finalColor = float3(colorR, colorG, colorB);
