Shader "Custom/SlimeShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Metallic ("Metallic", Range(0, 1)) = 0.5
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
		_SpecularCol ("Specular Color", Color) = (1, 1, 1, 1)
		_SpecularPow ("Specular Power", Range(10, 200)) = 100
		_RimColor("RimColor", Color) = (1, 1, 1, 1)
		_RimPower("RimPower", Range(1, 10)) = 3
		_AlphaPower("Alpha Power", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

		// 1 pass
        CGPROGRAM
        #pragma surface surf Standard alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;

		fixed4 _Color;

		float4 _RimColor;
		float _RimPower;

		fixed4 _SpecularColor;
		float _SpecularPow;

		float _Smoothness;
		float _Metallic;

		fixed _AlphaPower;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            o.Alpha = _AlphaPower;

			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;

			float rim = saturate(dot(o.Normal, IN.viewDir));
			o.Emission = pow(1 - rim, _RimPower) * _RimColor.rgb;
        }
        ENDCG

    }
    FallBack "Diffuse"
}
