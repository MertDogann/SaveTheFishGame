Shader "Muhammed/MC_Liquid"
{
	Properties
	{
		[Header(Main Properties)] _MainTex("Texture",2D) ="white"{}
		_Color("Color",Color)=(1,1,1,1)
		_Alpha("Alpha",Range(0,1))=1
		_SpecColor("Specular Color", Color) = (1,1,1,0)
		_Spec("Specular Value",Range(0,1)) = 0.5
		_Gloss("Gloss Value",Range(0,1)) = 0.5
		[Header(Outline)]_OutlineWidht("Outline Widht",Range(0,1))=0
		_OutlineColor("Outline Color",Color)=(1,1,1,1)
		_RimCutoff("Rim Arranger",Range(0,1))=0.5
		_RimFactor("Rim Factor",Range(0,10))=1
		[Header(Flow)]_Freq("Frequency",Range(0,10)) = 0
		_Speed("Speed",Range(0,300)) = 0
		_Amp("Amplitude",Range(0,2)) = 0
	}

	SubShader
	{

		Pass
		{
			Zwrite on
			ColorMask 0
		}

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha:fade

		half _OutlineWidht;
		float4 _OutlineColor;
		half _RimCutoff;
		half _RimFactor;
		half _Freq;
		half _Speed;
		half _Amp;

		struct appdata {
			float4 vertex :POSITION;
			float3 normal: NORMAL;
			float4 texcoord: TEXCOORD0;
		};

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		void vert(inout appdata v) {
			v.vertex.xyz += v.normal * _OutlineWidht;
			float t = _Time * _Speed;
			float wh = pow(sin(t + v.vertex.y * _Freq), 3) * _Amp;
			float whf = sin(t + v.vertex.y * _Freq);
			v.vertex.x += v.vertex.y > 0.99 ? 0 : whf > 0.5 ? v.normal.x * wh : 0;
			v.vertex.z += v.vertex.y > 0.99 ? 0 : whf > 0.5 ? v.normal.z * wh : 0;
			v.normal = normalize(float3(v.normal.x, v.normal.y + wh, v.normal.z));
		}

		void surf(Input IN, inout SurfaceOutput o) {
			half rim = 1 - dot(IN.viewDir, o.Normal);
			half rimf = rim > _RimCutoff ? rim : 0;
			o.Emission = _OutlineColor.rgb *pow(rimf,_RimFactor);
			o.Alpha = rim > _RimCutoff ? pow(rimf,_RimFactor) : 0;
			
		}
		ENDCG

		Pass
		{
			Zwrite on
			ColorMask 0
		}
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert alpha:fade

		half _Freq;
		half _Speed;
		half _Amp;

		struct Input {
			float2 uv_MainTex;
		};

		struct appdata {
			float4 vertex :POSITION;
			float3 normal: NORMAL;
			float4 texcoord: TEXCOORD0;
		};
		
		void vert(inout appdata v) {
			float t = _Time * _Speed;
			float wh = pow(sin(t + v.vertex.y * _Freq), 3) * _Amp;
			float whf = sin(t + v.vertex.y * _Freq);
			v.vertex.x += v.vertex.y > 0.99 ? 0 : whf > 0.5 ? v.normal.x * wh : 0;
			v.vertex.z += v.vertex.y > 0.99 ? 0 : whf > 0.5 ? v.normal.z * wh : 0;
			v.normal = normalize(float3(v.normal.x, v.normal.y + wh, v.normal.z));
		}
		sampler2D _MainTex;
		float4 _Color;
		half _Spec;
		fixed _Gloss;
		half _Alpha;

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb * tex2D(_MainTex,IN.uv_MainTex);
			o.Specular = _Spec;
			o.Gloss = _Gloss;
			o.Alpha = _Alpha;
		}
		ENDCG
	}
	Fallback "Diffuse"
}