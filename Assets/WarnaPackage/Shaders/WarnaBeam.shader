Shader "Custom/WarnaBeam"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MetallicTex("Metallic (R)", 2D) = "white" {}
        _Metallic("Metallic", Range(0,1)) = 0.0
        _RimColor("Rim Color", Color) = (0,0.5,0.5,0)
        _RimPower("Rim Power", Range(0.5,8.0)) = 3.0
    }
        SubShader
        {
            Tags{
                "Queue" = "Geometry"
            }
            CGPROGRAM
            #pragma surface surf Standard 

            sampler2D _MetallicTex;
            half _Metallic;
            fixed4 _Color;
            float4 _RimColor;
            float _RimPower;

            struct Input
            {
                float2 uv_MetallicTex;
                float3 viewDir;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                o.Albedo = _Color.rgb;
                o.Smoothness = tex2D(_MetallicTex, IN.uv_MetallicTex).r;
                o.Metallic = _Metallic;
                half rim = saturate(dot(normalize(IN.viewDir), o.Normal));
                o.Emission = _RimColor.rgb * pow(rim, _RimPower) * 10;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
