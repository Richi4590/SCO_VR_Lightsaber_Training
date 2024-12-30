Shader "LaserSword/LaserSwordShader_URP"
{
    Properties
    {
        [PerRendererData] _MainTex("Main Texture", 2D) = "white" {}
        [PerRendererData] _TintColor("Tint Color", Color) = (1, 1, 1, 1)
        [PerRendererData] _Intensity("Intensity", Float) = 1
        [PerRendererData] _RimColor("Rim Color", Color) = (1, 1, 1, 1)
        [PerRendererData] _RimPower("Rim Power", Float) = 2
        [PerRendererData] _RimIntensity("Rim Intensity", Float) = 1
        [PerRendererData] _InvFade("Inv Fade", Range(0.01, 3.0)) = 0.5
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float4 _TintColor;
            float _Intensity;
            float4 _RimColor;
            float _RimPower;
            float _RimIntensity;
            float _InvFade;

            struct VertexInput
            {
                float4 position : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct VertexOutput
            {
                float4 position : SV_POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
            };

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;

                // Transform vertex to clip space
                o.position = TransformObjectToHClip(v.position);

                // Calculate world position and view direction
                float3 worldPos = TransformObjectToWorld(v.position.xyz);
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);

                // Calculate world normal
                o.worldNormal = TransformObjectToWorldNormal(v.normal);

                // Pass color and transform UV coordinates
                o.color = v.color;
                o.texcoord = v.texcoord; // Directly passing the original UVs

                return o;
            }

            float4 frag(VertexOutput i) : SV_Target
            {
                // Rim lighting
                float rimFactor = saturate(1.0 - abs(dot(i.viewDir, normalize(i.worldNormal))));
                float3 rimLight = _RimIntensity * pow(rimFactor, _RimPower) * _RimColor.rgb;

                // Sample base texture
                float3 baseColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord).rgb;

                // Combine base color and rim lighting
                float3 finalColor = baseColor * i.color.rgb * _TintColor.rgb * _Intensity;
                finalColor += rimLight;

                return float4(finalColor, _TintColor.a);
            }

            ENDHLSL
        }
    }
}
