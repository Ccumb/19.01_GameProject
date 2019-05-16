Shader "Custom/IconShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_TextureForFull("When item slot is full", 2D) = "white" {}
		_isFull ("is Full", Range(-1, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" }
		cull off

        CGPROGRAM
        #pragma surface surf NoLight alpha:fade

        sampler2D _MainTex;
        sampler2D _TextureForFull;
		float _isFull;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_TextureForFull;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 d = tex2D(_TextureForFull, IN.uv_TextureForFull);

			if (_isFull < 0)
			{
				o.Emission = d.rgb;
				o.Alpha = d.a;
			}
			else
			{
				o.Emission = c.rgb;
				o.Alpha = c.a;
			}
        }

		float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten)
		{
			return float4(0, 0, 0, s.Alpha);
		}
        ENDCG
    }
	FallBack "Lagacy Shaders/Transparent/VertexLit"
}
