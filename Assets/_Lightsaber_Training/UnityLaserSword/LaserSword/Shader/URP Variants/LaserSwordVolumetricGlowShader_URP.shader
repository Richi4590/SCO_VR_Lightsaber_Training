Shader "LaserSword/LaserSwordVolumetricGlowShader_URP"
{
    Properties
    {
        _Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _CapsuleStart("Start", Vector) = (0.0, 0.0, 0.0, 0.0)
        _CapsuleEnd("End", Vector) = (0.0, 10.0, 0.0, 0.0)
        _CapsuleRadius("Radius", Float) = 0.5
        _GlowIntensity("Intensity", Range(0.0, 10.0)) = 3.0
        _GlowFalloff("Glow Power", Range(0.01, 8.0)) = 1.5
        _GlowCenterFalloff("Glow Center Power", Range(0.01, 1.0)) = 0.15
        _GlowDither("Glow Dither", Range(0.0, 1.0)) = 0.1
        _GlowMax("Max Glow", Range(0.0, 3.0)) = 1.0
        _GlowInvFade("Glow inv fade", Range(0.0, 3.0)) = 0.5
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" "LightMode" = "UniversalForward" "Queue" = "Transparent+1" }
        Pass
        {
            Blend One One
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Uniforms
            float4 _Color;
            float3 _CapsuleStart;
            float3 _CapsuleEnd;
            float _CapsuleRadius;
            float _CapsuleRadiusInv;
            float _GlowIntensity;
            float _GlowFalloff;
            float _GlowCenterFalloff;
            float _GlowDither;
            float _GlowMax;
            float _GlowInvFade;

            struct VertexInput
            {
                float4 vertex : POSITION;
            };

            struct VertexOutput
            {
                float4 position : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 rayDir : TEXCOORD1;
            };

            // Capsule intersection
            float CapsuleIntersect(float3 ro, float3 rd, float3 pa, float3 pb, float r)
            {
                float3 ba = pb - pa;
                float3 oa = ro - pa;

                float baba = dot(ba, ba);
                float bard = dot(ba, rd);
                float baoa = dot(ba, oa);
                float rdoa = dot(rd, oa);
                float oaoa = dot(oa, oa);

                float a = baba - bard * bard;
                float b = baba * rdoa - baoa * bard;
                float c = baba * oaoa - baoa * baoa - r * r * baba;
                float h = b * b - a * c;

                if (h < 0.0) return -1.0; // no intersection
                h = sqrt(h);
                float t = (-b - h) / a;

                float y = baoa + t * bard;
                if (y > 0.0 && y < baba)
                {
                    return t; // body
                }

                float3 oc = y <= 0.0 ? oa : ro - pb;
                b = dot(rd, oc);
                c = dot(oc, oc) - r * r;
                h = b * b - c;
                if (h > 0.0) return -b - sqrt(h);
                return -1.0; // no intersection
            }

            // Line-point distance
            float LinePointDistance(float3 lineStart, float3 lineDir, float3 p)
            {
                float3 lineToPoint = p - lineStart;
                float t = dot(lineToPoint, lineDir);
                float3 closestPoint = lineStart + t * lineDir;
                return length(closestPoint - p);
            }

            // Vertex shader
            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.position = TransformObjectToHClip(v.vertex.xyz);
                float3 worldPos = TransformObjectToWorld(v.vertex.xyz);
                o.worldPos = worldPos;
                o.rayDir = normalize(worldPos - _WorldSpaceCameraPos);
                return o;
            }

            // Fragment shader
            half4 frag(VertexOutput i) : SV_Target
            {
                float toCapsule = CapsuleIntersect(_WorldSpaceCameraPos, i.rayDir, _CapsuleStart, _CapsuleEnd, _CapsuleRadius);
                if (toCapsule < 0.0) return half4(0.0, 0.0, 0.0, 0.0); // No intersection

                float3 hitPoint = _WorldSpaceCameraPos + i.rayDir * toCapsule;
                float lineDist = 1.0 - saturate(LinePointDistance(_CapsuleStart, normalize(_CapsuleEnd - _CapsuleStart), hitPoint) / _CapsuleRadius);
                float glow = pow(saturate(lineDist * _GlowIntensity), _GlowFalloff);
                return half4(_Color.rgb * glow, 1.0);
            }
            ENDHLSL
        }
    }
}
