Shader "Custom/ItemPulsateShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _ShakeAmount ("Shake Amount", Range(0, 2)) = 0.2
        _ShakeSpeed ("Shake Speed", Range(0, 10)) = 3.5
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

            float _ShakeAmount;
            float _ShakeSpeed;
            sampler2D _MainTex;
            float _TimeOffset; // Time offset for the fading effect

            v2f vert(appdata_t v) {
                v.vertex.xy += sin((_Time.y + _TimeOffset) * _ShakeSpeed) * _ShakeAmount;

                // Shift each item up by 0.5 on the y-axis
                v.vertex.y += 0.5;

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                // Calculate the current time within the 2-second period
                float currentTime = (_Time.y + _TimeOffset) % 2.0;

                // Calculate the fade effect based on currentTime, smoothstep both ways
                float fade = smoothstep(0.0, 1.0, abs(currentTime - 1.0));

                // Sample the texture and apply the fade effect
                fixed4 originalColor = tex2D(_MainTex, i.uv);
                fixed4 finalColor = lerp(originalColor, originalColor * 1.5, fade);
                return finalColor;
            }
            ENDCG
        }
    }
}
