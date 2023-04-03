Shader "Custom/TransparentUnlit"
{
  Properties 
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Color ("Color (RGBA)", Color) = (1, 1, 1, 1) // add _Color property
  }

  SubShader 
  {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha
    LOD 100

    Pass 
    {
      CGPROGRAM

      #pragma vertex vert alpha
      #pragma fragment frag alpha

      #include "UnityCG.cginc"

      struct appdata
      {
        float4 vertex   : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct v2f 
      {
        float4 vertex  : SV_POSITION;
        half2 uv : TEXCOORD0;
      };

      sampler2D _MainTex;
      float4 _Color;

      v2f vert (appdata v) {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }
      
      fixed4 frag (v2f i) : SV_Target
      {
        fixed4 col = tex2D(_MainTex, i.uv) * _Color;
        col.a = col.a > 0.1;
        col.r = _Color.r;
        col.g = _Color.g;
        col.b = _Color.b;
        return col;
      }

      ENDCG
    }
  }
}