Shader "Custom/BlurAndNoise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurRadius ("Blur Radius", Range(0, 10)) = 1
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.1
    }
    
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
        
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float _BlurRadius;
        float _NoiseIntensity;
        
        // Declare _MainTex_TexelSize
        float4 _MainTex_TexelSize;

        fixed4 LightingLambert(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            fixed4 c;
            c.rgb = (s.Albedo * _LightColor0.rgb * (atten * 2)) * s.Normal.z;
            c.a = s.Alpha;
            return c;
        }

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Gaussian (half x, half sigma)
        {
            return (1.0 / sqrt(2.0 * 3.14159265359 * sigma * sigma)) * exp(-(x * x) / (2 * sigma * sigma));
        }

        float _Random (float2 uv)
        {
            return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 uv = IN.uv_MainTex;
            half4 col = tex2D(_MainTex, uv);
            
            // Apply Gaussian blur
            half blurWeight = _Gaussian(0, _BlurRadius);
            half4 blurCol = col * blurWeight;
            half sumWeights = blurWeight;
            
            for (int i = 1; i <= _BlurRadius; i++)
            {
                blurWeight = _Gaussian(i, _BlurRadius);
                sumWeights += blurWeight * 2;
                float2 uvOffset = _MainTex_TexelSize.xy * i;
                blurCol += (tex2D(_MainTex, uv + uvOffset) + tex2D(_MainTex, uv - uvOffset)) * blurWeight;
            }
            
            col = blurCol / sumWeights;
            
            // Apply noise
            float noise = _Random(uv) * 2 - 1;
            col.rgb += noise * _NoiseIntensity;
            
            o.Albedo = col.rgb;
            o.Alpha = col.a;
            o.Normal = float3(0, 0, 1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
