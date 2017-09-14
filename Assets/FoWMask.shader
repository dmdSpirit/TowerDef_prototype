Shader "Custom/FoWMask" {
	Properties {
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType" = "Transparent" "LightMode" = "ForwardBase"}
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

		CGPROGRAM
		#pragma surface surf NoLighting Lambert noambient
		Lighting LightingNoLighting(SurfaceOutput s, fixed3 lightDir, float aten){
			fixed4 color;
			color.rgb = s.Albedo;
			color.a = s.Alpha;
			return color;

		}

		fixed4 _Color;
		sampler2D _MainTex;

		struct Input{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o){
			half4 baseColor = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = baseColor.rgb;
			o.Alpha = _Color.a - baseColor.g;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
	
