// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Partial2_Homework/NicolasAJ/Ex2"
{
	Properties
	{
		_MainTex("Primary", 2D) = "white" {}

	}

		SubShader
	{
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

		sampler2D _MainTex;
	float4 _Color;

	struct appdata
	{
		float4 vertex : POSITION;	// object space		
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex : SV_POSITION; // screen space
		float2 uv : TEXCOORD0;
	};

	v2f vert(appdata v)
	{
		v2f o;

		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;

		return o;
	}

	float4 frag(v2f i) : COLOR
	{
		float4 mainTex = tex2D(_MainTex, i.uv + float2(-_Time.x, 0));

		return mainTex;
	}

		ENDCG
	}
	}
}
