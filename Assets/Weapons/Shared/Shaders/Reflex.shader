Shader "PolyGun/Reflex Sight"
{
    Properties
    {
		_LensColor ("Lens Color", Color) = (1,1,1,0.05)
		_SightColor ("Sight Color", Color) = (255,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _TexScale("Texture Scale", Range(1, 10)) = 1
        _Depth("Depth", Range(0, 100)) = 50
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
		Pass  //Colors in the Lens
		{
			CGPROGRAM
			#pragma vertex vert
            #pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

			float4 _LensColor;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
			fixed4 frag(v2f i) : SV_Target {
                float4 col = _LensColor;
				return col;
            }
            ENDCG
		}
        Pass  // Draws the dot
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float3 tangent : TANGENT;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 pos : TEXCOORD1;
                float3 normal : NORMAL;
                float3 tangent : TANGENT;
            };

            sampler2D _MainTex;
			float4 _LensColor;
			float4 _SightColor;
            float4 _MainTex_ST;
            float _TexScale;
            float _Depth;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.pos = UnityObjectToViewPos(v.vertex); 
                o.normal = mul(UNITY_MATRIX_IT_MV, v.normal);  
                o.tangent = mul(UNITY_MATRIX_IT_MV, v.tangent);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                float3 normal = normalize(i.normal);
                float3 tangent = normalize(i.tangent);
                float3 cameraDir = normalize(i.pos);

                float3 offset = cameraDir + normal;

                float3x3 mat = float3x3(
                    tangent,
                    cross(normal, tangent),
                    normal
                );

                offset = mul(mat, offset);

                float2 uv = (i.uv + (offset.xy * _Depth) - float2(0.5, 0.5)) / _TexScale;
                return tex2D(_MainTex, uv + float2(0.5, 0.5)) * _SightColor;
            }
            ENDCG
        }
    }
}
