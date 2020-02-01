// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PlaySide/Amplify/FX_Aura_Scrolling_Add_MAT"
{
	Properties
	{
		_MainTex("Main Tex", 2D) = "white" {}
		_ScrollTex("Scroll Tex", 2D) = "white" {}
		_MainTexPow("Main Tex Pow", Float) = 0
		_ScrollTexPow("Scroll Tex Pow", Float) = 0
		_PanVal("Pan Val", Float) = 0
		_RotVal("Rot Val", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha One
		
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf StandardCustomLighting keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			half2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform half _MainTexPow;
		uniform sampler2D _ScrollTex;
		uniform half _PanVal;
		uniform half _RotVal;
		uniform half _ScrollTexPow;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			half4 tex2DNode4 = tex2D( _MainTex, uv_MainTex );
			half2 temp_cast_1 = (_PanVal).xx;
			float cos14 = cos( _RotVal );
			float sin14 = sin( _RotVal );
			float2 rotator14 = mul( float2( 0,0 ) - float2( 0,0 ) , float2x2( cos14 , -sin14 , sin14 , cos14 )) + float2( 0,0 );
			float2 panner15 = ( 1.0 * _Time.y * temp_cast_1 + rotator14);
			c.rgb = 0;
			c.a = ( pow( tex2DNode4.a , _MainTexPow ) * ( i.vertexColor.b * pow( tex2D( _ScrollTex, panner15 ).a , _ScrollTexPow ) ) );
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			half4 tex2DNode4 = tex2D( _MainTex, uv_MainTex );
			o.Emission = ( tex2DNode4 * i.vertexColor ).rgb;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16200
-1624;69;1490;938;3100.969;85.57658;1.447882;True;False
Node;AmplifyShaderEditor.RangedFloatNode;18;-2556.819,471.9804;Half;False;Property;_RotVal;Rot Val;6;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;14;-2259.297,339.9825;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2244.631,547.4081;Half;False;Property;_PanVal;Pan Val;5;0;Create;True;0;0;False;0;0;0.18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;15;-1886.351,386.0771;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1398.165,761.1196;Half;False;Property;_ScrollTexPow;Scroll Tex Pow;4;0;Create;True;0;0;False;0;0;-0.96;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-1509.636,374.6472;Float;True;Property;_ScrollTex;Scroll Tex;2;0;Create;True;0;0;False;0;None;314786e91c59e1043b50434905495ce7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;6;-1375.116,67.60476;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;11;-1029.408,553.6943;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-886.9324,279.2207;Half;False;Property;_MainTexPow;Main Tex Pow;3;0;Create;True;0;0;False;0;0;1.39;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-1106.312,-164.886;Float;True;Property;_MainTex;Main Tex;1;0;Create;True;0;0;False;0;None;9e689a5bad106c54486636164321599c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-666.9357,501.3132;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;8;-671.1257,237.3167;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-405.0343,42.46233;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-361.0351,316.9346;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;0,0;Half;False;True;2;Half;ASEMaterialInspector;0;0;CustomLighting;PlaySide/Amplify/FX_Aura_Scrolling_Add_MAT;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;8;5;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;2;18;0
WireConnection;15;0;14;0
WireConnection;15;2;17;0
WireConnection;13;1;15;0
WireConnection;11;0;13;4
WireConnection;11;1;12;0
WireConnection;10;0;6;3
WireConnection;10;1;11;0
WireConnection;8;0;4;4
WireConnection;8;1;7;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;9;0;8;0
WireConnection;9;1;10;0
WireConnection;2;2;5;0
WireConnection;2;9;9;0
ASEEND*/
//CHKSM=78B66204C993FADA2F798A92CB9FD7D791877A25