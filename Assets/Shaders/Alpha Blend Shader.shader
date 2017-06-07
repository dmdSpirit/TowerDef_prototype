// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shaders101/Alpha Blend" {
	Properties{
		_MainTex("Texture",2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}

	Subshader{
		Tags{
			"Queue" = "Transparent"
		}

		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
			//Blend One One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v){
				v2f o;
				// Translave local position to World 
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			float4 frag(v2f i) : SV_Target{
				float4 color = tex2D(_MainTex, i.uv);
				return color;
			}
			ENDCG
		}
	}
}
