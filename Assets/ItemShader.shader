Shader "Unlit/ItemShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SparkleTex ("Sparkle Texture", 2D) = "white" {}
        _SparkleColor ("Sparkle Color", Color) = (1, 1, 1, 1)
        _SparkleSpeed ("Sparkle Speed", Range(0, 10)) = 1.0
        _SparkleFrequency ("Sparkle Frequency", Range(0, 10)) = 2.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _SparkleTex;
            float4 _MainTex_ST;
            float4 _SparkleColor;
            float _SparkleSpeed;
            float _SparkleFrequency;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the main texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Calculate the sparkle pattern using time and uv
                float sparkle = frac(i.uv.y * _SparkleFrequency + _Time.y * _SparkleSpeed);
                
                // Use the sparkle pattern to modulate the sparkle color
                fixed4 sparkleColor = _SparkleColor * sparkle;

                // Add the sparkle color to the original color
                col.rgb += sparkleColor.rgb;

                // Apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG
        }
    }
}
