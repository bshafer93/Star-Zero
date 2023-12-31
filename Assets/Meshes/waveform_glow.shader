Shader "Custom/waveform_glow"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Amplitude ("Amplitude",Range(0,1)) = 0.5
        _Strength ("Strength",Range(1.0,100.0)) = 100.0
        _Emission("Emission", float) = 0
        [HDR] _EmissionColor("Color", Color) = (0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Amplitude;
        float _Strength;
        fixed4 _EmissionColor;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float4 adjust_height(float4 v){

                float4 v_localPosition = v;
                if(_Strength < 1.0){
                    _Strength = 1.0;
                }
                v_localPosition.y *= _Amplitude * _Strength;
                return v_localPosition;
        }

        void vert(inout appdata_full v)
        {
            v.vertex = adjust_height(v.vertex);

        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Alpha = c.a;
            o.Emission = c.rgb * tex2D(_MainTex, IN.uv_MainTex).a * _EmissionColor;
             o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
