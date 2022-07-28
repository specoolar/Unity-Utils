Shader "game/Template"
{
    Properties
    {
        [MainColor] _BaseColor("Color", Color) = (0.5,0.5,0.5,1)
        _BaseMap("Base Map", 2D) = "white"
        // _Specular("Specular", FLOAT) = 0.5
        // _Smoothness("Smoothness", FLOAT) = 30
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            
            // #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            // #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            // #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            // #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            // #pragma multi_compile _ _SHADOWS_SOFT
            // #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl" 

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
                // half3 normalOS : NORMAL;
                // half4 tangentOS : TANGENT;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
                // half3  normalWS                 : NORMAL;
                // half3 tangentWS                 : TANGENT;
                // half3 bitangentWS               : BINORMAL;

                float fogFactor     : TEXCOORD4;
                // float3 viewVec     : TEXCOORD5;
// #ifdef _MAIN_LIGHT_SHADOWS
//                 float4 shadowCoord              : TEXCOORD6;
// #endif
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
                // half _Specular;
                // half _Smoothness;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionHCS = vertexInput.positionCS;
                // OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.fogFactor = ComputeFogFactor(OUT.positionHCS.z);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                
                // VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(IN.normalOS, IN.tangentOS);
                // output.normalWS = vertexNormalInput.normalWS;
                // output.tangentWS = vertexNormalInput.tangentWS;
                // output.bitangentWS = vertexNormalInput.bitangentWS;
                
                // OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);

                // OUT.viewVec = vertexInput.positionWS - _WorldSpaceCameraPos;
// #ifdef _MAIN_LIGHT_SHADOWS
//                 OUT.shadowCoord = GetShadowCoord(vertexInput);
// #endif
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // half3 normalWS = TransformTangentToWorld(normalTS,
                //     half3x3(input.tangentWS, input.bitangentWS, input.normalWS));

                // half3 normalWS = normalize(IN.normalWS);
                // half diffuse = dot(normalWS, _MainLightPosition.xyz);
// #ifdef _MAIN_LIGHT_SHADOWS
//                 Light mainLight = GetMainLight(IN.shadowCoord);
//                 half fade = saturate(dot(IN.viewVec, IN.viewVec) * _MainLightShadowParams.z + _MainLightShadowParams.w);
//                 mainLight.shadowAttenuation = lerp(mainLight.shadowAttenuation, 1, fade);
//                 diffuse *= mainLight.shadowAttenuation;
// #endif
                // diffuse = max(0, diffuse);
                // half specular = pow(max(0, dot(normalWS, normalize(_MainLightPosition.xyz - normalize(IN.viewVec)))), _Smoothness) * _Specular;
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _BaseColor;
                half4 OUT = color;// * diffuse + specular;
                OUT.rgb = MixFog(OUT.rgb, IN.fogFactor);
                return OUT;
            }
            ENDHLSL
        }

        // Used for rendering shadowmaps
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"

        // // Used for depth prepass
        // // If shadows cascade are enabled we need to perform a depth prepass. 
        // // We also need to use a depth prepass in some cases camera require depth texture
        // // (e.g, MSAA is enabled and we can't resolve with Texture2DMS
        // UsePass "Universal Render Pipeline/Lit/DepthOnly"
        
        // // Used for Baking GI. This pass is stripped from build.
        // UsePass "Universal Render Pipeline/Lit/Meta"
    }
}