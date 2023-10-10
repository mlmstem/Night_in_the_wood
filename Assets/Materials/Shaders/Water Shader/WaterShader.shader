// Code modified from: 
// https://www.youtube.com/watch?v=wcGT_jji5xQ
// https://learn.unity.com/tutorial/creating-a-vertex-displacement-shader#

Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color ("Colour", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise texture", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // Plane amplitude
        _Scale ("Noise scale", Range(0.1, 1.5)) = 0.5
        _PlaneAmp ("Plane amplitude", Range (0.05, 0.2)) = 0.15
        _Speed ("Speed", Range(0.005, 0.1)) = 0.01

        // Wave shape
        _WindDir("Wind direction", Vector) = (1.0,0.0,0.0,1.0)
        _WaveAmp ("Wave amplitude", Range(0.01, 0.2)) = 0.1
        _Period ("Period", Range (1.0, 15.0)) = 3.0
        _PhaseShift ("Phase shift", Range (0.0, 5.0)) = 1 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;

        // Plane amplitude
        float _Scale;
        float _PlaneAmp;
        float _Speed;

        // Wave shape
        float4 _WindDir;
        float _WaveAmp;
        float _Period; 
        float _PhaseShift;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Implementation
        void vert(inout appdata_full v)
        {
            // Change amplitude of the plane (continuously moves up and down)
            float2 NoiseUV = float2((v.texcoord.xy + _Time * _Speed) * _Scale);
            float NoiseVal = tex2Dlod(_NoiseTex, float4(NoiseUV, 0, 0)).x * _PlaneAmp;
            v.vertex = v.vertex + float4(0, NoiseVal, 0, 0);

            // Change the shape of the wave based on: _WaveAmp * sin(_Period + _PhaseShift)
            float3 pos = v.vertex.xyz; // position of all vertices
            float4 dir = normalize(_WindDir); // normalise direction of waves

            float waveLength = 2 * UNITY_PI / _Period;
            float disp = waveLength * (dot(dir, pos) - (_PhaseShift * _Time.y));

            pos.x += dir.x * (_WaveAmp * cos(disp));
            pos.y = _WaveAmp * sin(disp);
            pos.z += dir.y * (_WaveAmp * sin(disp));
            v.vertex.xyz = v.vertex.xyz + pos;
        }


        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            // Texture
            o.Normal = UnpackNormal(tex2D (_MainTex, IN.uv_MainTex));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
