// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color

Shader "KT/Mobile/DiffuseTintAlpha" {
	Properties{
		_MainTex("Base (RGB) Alpha (A)", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	}
		SubShader{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			LOD 100

		CGPROGRAM
			// Mobile improvement: noforwardadd
			// http://answers.unity3d.com/questions/1200437/how-to-make-a-conditional-pragma-surface-noforward.html
			// http://gamedev.stackexchange.com/questions/123669/unity-surface-shader-conditinally-noforwardadd
			#pragma surface surf Lambert alpha noforwardadd

			sampler2D _MainTex;
			fixed4 _Color;
			uniform float _Cutoff;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				if (c.a > _Cutoff)
				  o.Alpha = c.a;
				 else
				  o.Alpha = 0;
			}
			ENDCG
		}

			Fallback "Mobile/VertexLit"
}