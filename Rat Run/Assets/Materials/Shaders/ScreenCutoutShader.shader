// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/ScreenCutoutShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Lighting Off
		Cull Back
		ZWrite On
		ZTest Less
		
		Fog{ Mode Off }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag	//fragment = pixel
			
			#include "UnityCG.cginc"

			//	Vertex Shader Input
			struct appdata
			{
				float4 vertex : POSITION;	//vertex position
				float2 uv : TEXCOORD0;
			};

			//	Vertex Shader Output
			struct v2f	//v2f = vertex to fragment
			{
				//float2 uv : TEXCOORD0;	//Don't want UV
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
			};


			//	Convert appdata 'v' (input) to v2f 'o' (output)
			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = UnityWorldToClipPos(v.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);	// Project 3D vertex 'v' onto 2D clipspace as vertex 'o'
				
				//o.screenPos = ComputeScreenPos(o.vertex);	// What is the screen coordinates of vertex 'o'
				o.screenPos = ComputeNonStereoScreenPos(o.vertex);
				
				return o;
			}
			
			sampler2D _MainTex;
			//half4 _MainTex_ST;

			//	Convert vertex shader 'v2f' to fragment shader
			fixed4 frag (v2f i) : SV_Target
			{
				
				i.screenPos /= i.screenPos.w;
				fixed4 col = tex2D(_MainTex, float2(i.screenPos.x, i.screenPos.y));
				//fixed4 col = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(float2(i.screenPos.x, i.screenPos.y), _MainTex_ST));
				
				return col;
			}
			ENDCG
		}
	}
}
