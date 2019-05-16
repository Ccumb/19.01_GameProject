Shader "Custom/SlimeShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RimColor("RimColor", Color) = (1, 1, 1, 1)
		_RimPower("RimPower", Range(1, 10)) = 3
		_SpecularColor("Specular Color", Color) = (1, 1, 1, 1)
		_SpecPow("Specular Power", Range(10, 500)) = 100
		_AlphaPower("Alpha Power", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

		// 1 pass
        CGPROGRAM
        #pragma surface surf Toon alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;

		fixed4 _Color;

		float4 _RimColor;
		float _RimPower;

		float4 _SpecularColor;
		float _SpecPow;

		fixed _AlphaPower;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            o.Alpha = _AlphaPower;
			
			float rim = saturate(dot(o.Normal, IN.viewDir));
			o.Emission = pow(1 - rim, _RimPower) * _RimColor.rgb * _LightColor0.rgb;
        }

		float4 LightingToon(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			float3 DiffuseColor;

			float ndot1 = saturate(dot(s.Normal, lightDir));
			DiffuseColor = ndot1 * s.Albedo * _LightColor0.rgb * atten;

			float3 specColor;
			float3 H = normalize(lightDir + viewDir);
			float spec = saturate(dot(H, s.Normal));
			spec = pow(spec, _SpecPow);
			spec = ceil(spec * 5) / 3;
			specColor = spec * _SpecularColor;

			float4 final;

			final.rgb = DiffuseColor + specColor;
			final.a = s.Alpha + (spec + 0.3);

			return final;
		}

        ENDCG

    }
    FallBack "Diffuse"
}
