Shader "Custom/ItemPulsateShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        // Controls the pulse amount (i.e range of pulse)
        _PulseAmount ("Pulse Amount", Range(0, 2)) = 0.2
        // Controls how fast it pulses
        _PulseSpeed ("Pulse Speed", Range(0, 10)) = 3.5
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _PulseAmount;
            float _PulseSpeed;
            sampler2D _MainTex;
            float _TimeOffset;
            v2f vert(appdata_t v) {
                // Create pulse effect
                v.vertex.xy += sin((_Time.y + _TimeOffset) * _PulseSpeed) * _PulseAmount;

                // Shift each item up so they don't clip through the ground
                v.vertex.y += 0.5;

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                // 2-second fade
                float currentTime = (_Time.y + _TimeOffset) % 2.0;

                // Smoothly change the item color
                float fade = smoothstep(0.0, 1.0, abs(currentTime - 1.0));

                fixed4 originalColor = tex2D(_MainTex, i.uv);
                fixed4 finalColor = lerp(originalColor, originalColor * 1.5, fade);
                return finalColor;
            }
            ENDCG
        }
    }
}
