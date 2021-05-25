
Shader "PolyGun/Gun Shader" {
    Properties {
        _CamoTexture ("Camo Texture", 2D) = "white" {}
        _CamoColor ("Camo Color", Color) = (1,1,1,1)
        _CamoMetallic ("Camo Metallic", Range(0, 1)) = 0.3
        _CamoGloss ("Camo Gloss", Range(0, 1)) = 0.2
        _Color1 ("Color 1", Color) = (0.8,0.8,0.8,1)
        _Color1Metallic ("Color 1 Metallic", Range(0, 1)) = 0.9
        _Color1Gloss ("Color 1 Gloss", Range(0, 1)) = 0.7
        _Color2 ("Color 2", Color) = (0.5137255,0.5137255,0.5137255,1)
        _Color2Metallic ("Color 2 Metallic", Range(0, 1)) = 1
        _Color2Gloss ("Color 2 Gloss", Range(0, 1)) = 0.05
        _Color3 ("Color 3", Color) = (0,0,0,1)
        _Color3Metallic ("Color 3 Metallic", Range(0, 1)) = 0
        _Color3Gloss ("Color 3 Gloss", Range(0, 1)) = 0.15
        _Color4 ("Color 4", Color) = (0.1176471,0.1176471,0.1176471,1)
        _Color4Metallic ("Color 4 Metallic", Range(0, 1)) = 0
        _Color4Gloss ("Color 4 Gloss", Range(0, 1)) = 0.15
        _Color5 ("Color 5", Color) = (0.3019608,0.3019608,0.3019608,1)
        _Color5Metallic ("Color 5 Metallic", Range(0, 1)) = 0.75
        _Color5Gloss ("Color 5 Gloss", Range(0, 1)) = 0.6
        _Color6 ("Color 6", Color) = (0.5137255,0.5137255,0.5137255,1)
        _Color6Metallic ("Color 6 Metallic", Range(0, 1)) = 0.75
        _Color6Gloss ("Color 6 Gloss", Range(0, 1)) = 0.6
        _Emission1 ("Emission 1", Color) = (1,0,0,1)
        _Emission2 ("Emission 2", Color) = (0,1,0,1)
        _Emission3 ("Emission 3", Color) = (0,0,1,1)
        [HideInInspector]_TextureMask ("Texture Mask", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform float4 _Color3;
            uniform float4 _Color4;
            uniform sampler2D _CamoTexture; uniform float4 _CamoTexture_ST;
            uniform float4 _Color5;
            uniform float4 _Color6;
            uniform float4 _Emission1;
            uniform float4 _Emission2;
            uniform float4 _Emission3;
            uniform float _Color4Metallic;
            uniform float _Color5Metallic;
            uniform float _Color6Metallic;
            uniform float _Color6Gloss;
            uniform float4 _CamoColor;
            uniform float _Color1Gloss;
            uniform float _Color2Gloss;
            uniform float _Color3Gloss;
            uniform float _CamoGloss;
            uniform float _Color1Metallic;
            uniform float _Color2Metallic;
            uniform float _Color3Metallic;
            uniform float _CamoMetallic;
            uniform float _Color4Gloss;
            uniform float _Color5Gloss;
            uniform sampler2D _TextureMask; uniform float4 _TextureMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 node_8203 = tex2D(_TextureMask,TRANSFORM_TEX(i.uv0, _TextureMask));
                float3 Mask1 = node_8203.rgb;
                float3 node_1295 = Mask1;
                float node_5061 = 1.0;
                float node_6050 = 1.9339;
                float2 node_9876_tc_rcp = float2(1.0,1.0)/float2( node_5061, node_5061 );
                float node_9876_ty = floor(node_6050 * node_9876_tc_rcp.x);
                float node_9876_tx = node_6050 - node_5061 * node_9876_ty;
                float2 node_9876 = (i.uv0 + float2(node_9876_tx, node_9876_ty)) * node_9876_tc_rcp;
                float4 node_3891 = tex2D(_TextureMask,TRANSFORM_TEX(node_9876, _TextureMask));
                float Mask4 = (1.0 - (node_3891.rgb.r+node_3891.rgb.g+node_3891.rgb.b));
                float node_2650 = Mask4;
                float2 node_8833_tc_rcp = float2(1.0,1.0)/float2( node_5061, node_5061 );
                float node_8833_ty = floor(node_6050 * node_8833_tc_rcp.x);
                float node_8833_tx = node_6050 - node_5061 * node_8833_ty;
                float2 node_8833 = (i.uv0 + float2(node_8833_tx, node_8833_ty)) * node_8833_tc_rcp;
                float4 node_3179 = tex2D(_TextureMask,TRANSFORM_TEX(node_8833, _TextureMask));
                float3 Mask2 = node_3179.rgb;
                float3 node_7207 = Mask2;
                float gloss = (((lerp( lerp( lerp( _CamoGloss, _Color1Gloss, node_1295.r ), _Color2Gloss, node_1295.g ), _Color3Gloss, node_1295.b ))*node_2650)+(node_7207.r*_Color4Gloss + node_7207.g*_Color5Gloss + node_7207.b*_Color6Gloss));
                float perceptualRoughness = 1.0 - (((lerp( lerp( lerp( _CamoGloss, _Color1Gloss, node_1295.r ), _Color2Gloss, node_1295.g ), _Color3Gloss, node_1295.b ))*node_2650)+(node_7207.r*_Color4Gloss + node_7207.g*_Color5Gloss + node_7207.b*_Color6Gloss));
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = (((lerp( lerp( lerp( _CamoMetallic, _Color1Metallic, node_1295.r ), _Color2Metallic, node_1295.g ), _Color3Metallic, node_1295.b ))*node_2650)+(node_7207.r*_Color4Metallic + node_7207.g*_Color5Metallic + node_7207.b*_Color6Metallic));
                float specularMonochrome;
                float4 _CamoTexture_var = tex2D(_CamoTexture,TRANSFORM_TEX(i.uv0, _CamoTexture));
                float node_7543 = 100.0;
                float node_8569 = 0.0;
                float2 node_5326_tc_rcp = float2(1.0,1.0)/float2( node_7543, 2.0 );
                float node_5326_ty = floor(node_8569 * node_5326_tc_rcp.x);
                float node_5326_tx = node_8569 - node_7543 * node_5326_ty;
                float2 node_5326 = (i.uv0 + float2(node_5326_tx, node_5326_ty)) * node_5326_tc_rcp;
                float4 node_2062 = tex2D(_TextureMask,TRANSFORM_TEX(node_5326, _TextureMask));
                float3 diffuseColor = ((lerp( lerp( lerp( (_CamoTexture_var.rgb*_CamoColor.rgb*(1.0 - node_2062.r.r)), _Color1.rgb, node_1295.r ), _Color2.rgb, node_1295.g ), _Color3.rgb, node_1295.b ))+(node_7207.r*_Color4.rgb + node_7207.g*_Color5.rgb + node_7207.b*_Color6.rgb)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_5467 = 1.868;
                float2 node_7068_tc_rcp = float2(1.0,1.0)/float2( node_5061, node_5061 );
                float node_7068_ty = floor(node_5467 * node_7068_tc_rcp.x);
                float node_7068_tx = node_5467 - node_5061 * node_7068_ty;
                float2 node_7068 = (i.uv0 + float2(node_7068_tx, node_7068_ty)) * node_7068_tc_rcp;
                float4 node_3478 = tex2D(_TextureMask,TRANSFORM_TEX(node_7068, _TextureMask));
                float3 Mask3 = node_3478.rgb;
                float3 node_5255 = Mask3;
                float3 emissive = (node_5255.r*_Emission1.rgb + node_5255.g*_Emission2.rgb + node_5255.b*_Emission3.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform float4 _Color3;
            uniform float4 _Color4;
            uniform sampler2D _CamoTexture; uniform float4 _CamoTexture_ST;
            uniform float4 _Color5;
            uniform float4 _Color6;
            uniform float4 _Emission1;
            uniform float4 _Emission2;
            uniform float4 _Emission3;
            uniform float _Color4Metallic;
            uniform float _Color5Metallic;
            uniform float _Color6Metallic;
            uniform float _Color6Gloss;
            uniform float4 _CamoColor;
            uniform float _Color1Gloss;
            uniform float _Color2Gloss;
            uniform float _Color3Gloss;
            uniform float _CamoGloss;
            uniform float _Color1Metallic;
            uniform float _Color2Metallic;
            uniform float _Color3Metallic;
            uniform float _CamoMetallic;
            uniform float _Color4Gloss;
            uniform float _Color5Gloss;
            uniform sampler2D _TextureMask; uniform float4 _TextureMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 node_8203 = tex2D(_TextureMask,TRANSFORM_TEX(i.uv0, _TextureMask));
                float3 Mask1 = node_8203.rgb;
                float3 node_1295 = Mask1;
                float node_5061 = 1.0;
                float node_6050 = 1.9339;
                float2 node_9876_tc_rcp = float2(1.0,1.0)/float2( node_5061, node_5061 );
                float node_9876_ty = floor(node_6050 * node_9876_tc_rcp.x);
                float node_9876_tx = node_6050 - node_5061 * node_9876_ty;
                float2 node_9876 = (i.uv0 + float2(node_9876_tx, node_9876_ty)) * node_9876_tc_rcp;
                float4 node_3891 = tex2D(_TextureMask,TRANSFORM_TEX(node_9876, _TextureMask));
                float Mask4 = (1.0 - (node_3891.rgb.r+node_3891.rgb.g+node_3891.rgb.b));
                float node_2650 = Mask4;
                float2 node_8833_tc_rcp = float2(1.0,1.0)/float2( node_5061, node_5061 );
                float node_8833_ty = floor(node_6050 * node_8833_tc_rcp.x);
                float node_8833_tx = node_6050 - node_5061 * node_8833_ty;
                float2 node_8833 = (i.uv0 + float2(node_8833_tx, node_8833_ty)) * node_8833_tc_rcp;
                float4 node_3179 = tex2D(_TextureMask,TRANSFORM_TEX(node_8833, _TextureMask));
                float3 Mask2 = node_3179.rgb;
                float3 node_7207 = Mask2;
                float gloss = (((lerp( lerp( lerp( _CamoGloss, _Color1Gloss, node_1295.r ), _Color2Gloss, node_1295.g ), _Color3Gloss, node_1295.b ))*node_2650)+(node_7207.r*_Color4Gloss + node_7207.g*_Color5Gloss + node_7207.b*_Color6Gloss));
                float perceptualRoughness = 1.0 - (((lerp( lerp( lerp( _CamoGloss, _Color1Gloss, node_1295.r ), _Color2Gloss, node_1295.g ), _Color3Gloss, node_1295.b ))*node_2650)+(node_7207.r*_Color4Gloss + node_7207.g*_Color5Gloss + node_7207.b*_Color6Gloss));
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = (((lerp( lerp( lerp( _CamoMetallic, _Color1Metallic, node_1295.r ), _Color2Metallic, node_1295.g ), _Color3Metallic, node_1295.b ))*node_2650)+(node_7207.r*_Color4Metallic + node_7207.g*_Color5Metallic + node_7207.b*_Color6Metallic));
                float specularMonochrome;
                float4 _CamoTexture_var = tex2D(_CamoTexture,TRANSFORM_TEX(i.uv0, _CamoTexture));
                float node_7543 = 100.0;
                float node_8569 = 0.0;
                float2 node_5326_tc_rcp = float2(1.0,1.0)/float2( node_7543, 2.0 );
                float node_5326_ty = floor(node_8569 * node_5326_tc_rcp.x);
                float node_5326_tx = node_8569 - node_7543 * node_5326_ty;
                float2 node_5326 = (i.uv0 + float2(node_5326_tx, node_5326_ty)) * node_5326_tc_rcp;
                float4 node_2062 = tex2D(_TextureMask,TRANSFORM_TEX(node_5326, _TextureMask));
                float3 diffuseColor = ((lerp( lerp( lerp( (_CamoTexture_var.rgb*_CamoColor.rgb*(1.0 - node_2062.r.r)), _Color1.rgb, node_1295.r ), _Color2.rgb, node_1295.g ), _Color3.rgb, node_1295.b ))+(node_7207.r*_Color4.rgb + node_7207.g*_Color5.rgb + node_7207.b*_Color6.rgb)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform float4 _Color3;
            uniform float4 _Color4;
            uniform sampler2D _CamoTexture; uniform float4 _CamoTexture_ST;
            uniform float4 _Color5;
            uniform float4 _Color6;
            uniform float4 _Emission1;
            uniform float4 _Emission2;
            uniform float4 _Emission3;
            uniform float _Color4Metallic;
            uniform float _Color5Metallic;
            uniform float _Color6Metallic;
            uniform float _Color6Gloss;
            uniform float4 _CamoColor;
            uniform float _Color1Gloss;
            uniform float _Color2Gloss;
            uniform float _Color3Gloss;
            uniform float _CamoGloss;
            uniform float _Color1Metallic;
            uniform float _Color2Metallic;
            uniform float _Color3Metallic;
            uniform float _CamoMetallic;
            uniform float _Color4Gloss;
            uniform float _Color5Gloss;
            uniform sampler2D _TextureMask; uniform float4 _TextureMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_5061 = 1.0;
                float node_5467 = 1.868;
                float2 node_7068_tc_rcp = float2(1.0,1.0)/float2( node_5061, node_5061 );
                float node_7068_ty = floor(node_5467 * node_7068_tc_rcp.x);
                float node_7068_tx = node_5467 - node_5061 * node_7068_ty;
                float2 node_7068 = (i.uv0 + float2(node_7068_tx, node_7068_ty)) * node_7068_tc_rcp;
                float4 node_3478 = tex2D(_TextureMask,TRANSFORM_TEX(node_7068, _TextureMask));
                float3 Mask3 = node_3478.rgb;
                float3 node_5255 = Mask3;
                o.Emission = (node_5255.r*_Emission1.rgb + node_5255.g*_Emission2.rgb + node_5255.b*_Emission3.rgb);
                
                float4 node_8203 = tex2D(_TextureMask,TRANSFORM_TEX(i.uv0, _TextureMask));
                float3 Mask1 = node_8203.rgb;
                float3 node_1295 = Mask1;
                float4 _CamoTexture_var = tex2D(_CamoTexture,TRANSFORM_TEX(i.uv0, _CamoTexture));
                float node_7543 = 100.0;
                float node_8569 = 0.0;
                float2 node_5326_tc_rcp = float2(1.0,1.0)/float2( node_7543, 2.0 );
                float node_5326_ty = floor(node_8569 * node_5326_tc_rcp.x);
                float node_5326_tx = node_8569 - node_7543 * node_5326_ty;
                float2 node_5326 = (i.uv0 + float2(node_5326_tx, node_5326_ty)) * node_5326_tc_rcp;
                float4 node_2062 = tex2D(_TextureMask,TRANSFORM_TEX(node_5326, _TextureMask));
                float node_6050 = 1.9339;
                float2 node_8833_tc_rcp = float2(1.0,1.0)/float2( node_5061, node_5061 );
                float node_8833_ty = floor(node_6050 * node_8833_tc_rcp.x);
                float node_8833_tx = node_6050 - node_5061 * node_8833_ty;
                float2 node_8833 = (i.uv0 + float2(node_8833_tx, node_8833_ty)) * node_8833_tc_rcp;
                float4 node_3179 = tex2D(_TextureMask,TRANSFORM_TEX(node_8833, _TextureMask));
                float3 Mask2 = node_3179.rgb;
                float3 node_7207 = Mask2;
                float3 diffColor = ((lerp( lerp( lerp( (_CamoTexture_var.rgb*_CamoColor.rgb*(1.0 - node_2062.r.r)), _Color1.rgb, node_1295.r ), _Color2.rgb, node_1295.g ), _Color3.rgb, node_1295.b ))+(node_7207.r*_Color4.rgb + node_7207.g*_Color5.rgb + node_7207.b*_Color6.rgb));
                float specularMonochrome;
                float3 specColor;
                float2 node_9876_tc_rcp = float2(1.0,1.0)/float2( node_5061, node_5061 );
                float node_9876_ty = floor(node_6050 * node_9876_tc_rcp.x);
                float node_9876_tx = node_6050 - node_5061 * node_9876_ty;
                float2 node_9876 = (i.uv0 + float2(node_9876_tx, node_9876_ty)) * node_9876_tc_rcp;
                float4 node_3891 = tex2D(_TextureMask,TRANSFORM_TEX(node_9876, _TextureMask));
                float Mask4 = (1.0 - (node_3891.rgb.r+node_3891.rgb.g+node_3891.rgb.b));
                float node_2650 = Mask4;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, (((lerp( lerp( lerp( _CamoMetallic, _Color1Metallic, node_1295.r ), _Color2Metallic, node_1295.g ), _Color3Metallic, node_1295.b ))*node_2650)+(node_7207.r*_Color4Metallic + node_7207.g*_Color5Metallic + node_7207.b*_Color6Metallic)), specColor, specularMonochrome );
                float roughness = 1.0 - (((lerp( lerp( lerp( _CamoGloss, _Color1Gloss, node_1295.r ), _Color2Gloss, node_1295.g ), _Color3Gloss, node_1295.b ))*node_2650)+(node_7207.r*_Color4Gloss + node_7207.g*_Color5Gloss + node_7207.b*_Color6Gloss));
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
