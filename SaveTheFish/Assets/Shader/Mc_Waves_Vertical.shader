Shader "Muhammed/MC_Waves_Vertical"
{
	Properties
	{
		_MainTex("Main Texture",2D)="white" {}
		_MainColor("Main Color",Color) = (1,1,1,1)
		_Freq("Frequency",Range(0,10)) = 0
		_Speed("Speed",Range(-300,300)) = 0
		_Amp("Amplitude",Range(0,2)) = 0
	}

		SubShader
		{
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		float4 _MainColor;
		half _Freq;
		half _Speed;
		half _Amp;
		half _WhfVa;

		struct Input {
			float2 uv_MainTex;
			float3 vertColor;
		};

		struct appdata {
			float4 vertex: POSITION;
			float3 normal: NORMAL;
			float4 texcoord: TEXCOORD0;
			float4 texcoord1: TEXCOORD1;
			float4 texcoord2: TEXCOORD2;
		};

		void vert(inout appdata v) {
			//UNITY_INITIALIZE_OUTPUT(Input, o);
			float t = _Time * _Speed;
			float wh = pow(sin(t + v.vertex.y * _Freq),3)*_Amp;
			float whf = sin(t + v.vertex.y * _Freq);
			v.vertex.x += v.vertex.y > 1.99 ? 0 : whf > 0.5 ? v.normal.x * wh : 0;
			v.vertex.z += v.vertex.y > 1.99 ? 0 : whf > 0.5 ? v.normal.z * wh : 0;
			v.normal = whf > 0.5 ? normalize(float3(v.normal.x, v.normal.y + wh, v.normal.z)) : normalize(float3(v.normal.x, v.normal.y, v.normal.z));
			//o.vertColor = wh + 2;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			float4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = _MainColor.rgb * c.rgb;
		}

		ENDCG
	}
	Fallback "Diffuse"
}