Shader "Custom/BlackHoleUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}                 // Основная текстура
        _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1) // Цвет эмиссии
        _PulseSpeed ("Pulse Speed", Float) = 1.0               // Скорость пульсации
        _DeformationAmount ("Deformation Amount", Float) = 0.1 // Величина деформации
        _TimeScale ("Time Scale", Float) = 1.0                 // Масштаб времени
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

            // Включаем необходимые заголовки для URP
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Структура входных данных для вершинного шейдера
            struct appdata_t
            {
                float4 vertex : POSITION; // Позиция вершин
                float2 uv : TEXCOORD0;    // UV координаты
            };

            // Структура выходных данных из вершинного шейдера
            struct v2f
            {
                float2 uv : TEXCOORD0;       // UV координаты
                float4 vertex : SV_POSITION; // Позиция в пространстве экрана
                float time : TEXCOORD1;      // Передаем текущее время
            };

            sampler2D _MainTex;           // Основная текстура
            float4 _EmissionColor;        // Цвет эмиссии
            float _PulseSpeed;            // Скорость пульсации
            float _DeformationAmount;     // Величина деформации
            float _TimeScale;             // Масштаб времени

            // Вершинный шейдер
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex); // Используем TransformObjectToHClip для URP
                o.uv = v.uv;                                // Передаем UV координаты
                o.time = _Time.y * _TimeScale;              // Используем _Time для пульсации
                return o;
            }

            // Фрагментный шейдер
            half4 frag(v2f i) : SV_Target
            {
                // Пульсация
                float pulse = sin(i.time * _PulseSpeed) * 0.5 + 0.5; // Генерация пульсации
                float2 uvOffset = sin(i.uv.x * 10.0 + i.time) * _DeformationAmount; // Эффект деформации
                float2 uv = i.uv + uvOffset; // Изменение UV координат

                half4 col = tex2D(_MainTex, uv) * pulse; // Получаем цвет с учетом пульсации

                // Эмиссия
                col += _EmissionColor * pulse; // Добавляем эффект эмиссии

                return col; // Финальный цвет
            }
            ENDHLSL
        }
    }
    FallBack "Unlit"
}
