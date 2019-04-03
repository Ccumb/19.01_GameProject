Shader "Custom/IconShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_TextureForFull("When item slot is full", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" }
		cull off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf NoLight alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _TextureForFull;
		float _isFull;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_TextureForFull;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

			if (_isFull > 0)
			{
				c = tex2D(_TextureForFull, IN.uv_TextureForFull);
			}

			o.Emission = c.rgb;
            o.Alpha = c.a;
        }

		float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten)
		{
			return float4(0, 0, 0, s.Alpha);
		}
        ENDCG
    }
	FallBack "Lagacy Shaders/Transparent/VertexLit"
}
