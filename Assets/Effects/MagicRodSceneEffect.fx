sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float4 FilterMyShader(float2 coords : TEXCOORD0) : COLOR0
{
    // Sample the base color from the texture
    float4 baseColor = tex2D(uImage0, coords);

    // Calculate the aspect ratio
    float aspectRatio = uScreenResolution.x / uScreenResolution.y;

    // Normalize coordinates to maintain circular Fresnel effect
    float2 normalizedCoords = coords;
    normalizedCoords.x = (coords.x - 0.5) * aspectRatio + 0.5;

    // Calculate the distance from the center to simulate the Fresnel effect
    float2 center = float2(0.5, 0.5);
    float2 position = normalizedCoords - center;
    float distance = length(position) * 2.0; // Adjust multiplier to control edge size

    // Calculate the Fresnel term
    float fresnel = pow(1.0 - saturate(distance), 3.0) * uIntensity;

    // Mix the base color with the Fresnel effect color
    float4 fresnelColor = float4(uSecondaryColor, 1.0) * fresnel;

    // Combine the base color with the Fresnel effect color
    float4 finalColor = baseColor + fresnelColor;

    // Apply progress factor
    finalColor *= max(1.0 - uProgress, 0.25);

    // Apply opacity
    finalColor.a *= uOpacity; // Ensure opacity is in the range [0.0, 1.0]

    return baseColor * fresnelColor;
}

technique Technique1
{
    pass FilterMyShader
    {
        PixelShader = compile ps_2_0 FilterMyShader();
    }
}