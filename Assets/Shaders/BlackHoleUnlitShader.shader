Shader "Custom/BlackHoleUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}                 // �������� ��������
        _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1) // ���� �������
        _PulseSpeed ("Pulse Speed", Float) = 1.0               // �������� ���������
        _DeformationAmount ("Deformation Amount", Float) = 0.1 // �������� ����������
        _TimeScale ("Time Scale", Float) = 1.0                 // ������� �������
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // �������� ����������� ��������� ��� URP
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // ��������� ������� ������ ��� ���������� �������
            struct appdata_t
            {
                float4 vertex : POSITION; // ������� ������
                float2 uv : TEXCOORD0;    // UV ����������
            };

            // ��������� �������� ������ �� ���������� �������
            struct v2f
            {
                float2 uv : TEXCOORD0;       // UV ����������
                float4 vertex : SV_POSITION; // ������� � ������������ ������
                float time : TEXCOORD1;      // �������� ������� �����
            };

            sampler2D _MainTex;           // �������� ��������
            float4 _EmissionColor;        // ���� �������
            float _PulseSpeed;            // �������� ���������
            float _DeformationAmount;     // �������� ����������
            float _TimeScale;             // ������� �������

            // ��������� ������
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex); // ���������� TransformObjectToHClip ��� URP
                o.uv = v.uv;                                // �������� UV ����������
                o.time = _Time.y * _TimeScale;              // ���������� _Time ��� ���������
                return o;
            }

            // ����������� ������
            half4 frag(v2f i) : SV_Target
            {
                // ���������
                float pulse = sin(i.time * _PulseSpeed) * 0.5 + 0.5; // ��������� ���������
                float2 uvOffset = sin(i.uv.x * 10.0 + i.time) * _DeformationAmount; // ������ ����������
                float2 uv = i.uv + uvOffset; // ��������� UV ���������

                half4 col = tex2D(_MainTex, uv) * pulse; // �������� ���� � ������ ���������

                // �������
                col += _EmissionColor * pulse; // ��������� ������ �������

                return col; // ��������� ����
            }
            ENDHLSL
        }
    }
    FallBack "Unlit"
}
