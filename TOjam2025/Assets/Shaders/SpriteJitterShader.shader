Shader "Sprites/Default with UV Noise"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _NoiseAmt("Noise Amt", Range(0, 1)) = 0.5
        _NoiseSpeed("Noise Speed", Float) = 1
        _NoiseScaleX("Noise Scale X", Float) = 1
        _NoiseScaleY("Noise Scale Y", Float) = 1
        _NoiseTex("Noise Texture", 2D) = "white" {}
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            Blend One OneMinusSrcAlpha

            Pass
            {
            CGPROGRAM
                #pragma vertex SpriteVert
                #pragma fragment SpriteFrag
                #pragma target 2.0
                #pragma multi_compile_instancing
                #pragma multi_compile_local _ PIXELSNAP_ON
                #pragma multi_compile _ ETC1_EXTERNAL_ALPHA

                #ifndef UNITY_SPRITES_INCLUDED
                #define UNITY_SPRITES_INCLUDED

                #include "UnityCG.cginc"

                #ifdef UNITY_INSTANCING_ENABLED

                    UNITY_INSTANCING_BUFFER_START(PerDrawSprite)
            // SpriteRenderer.Color while Non-Batched/Instanced.
            UNITY_DEFINE_INSTANCED_PROP(fixed4, unity_SpriteRendererColorArray)
            // this could be smaller but that's how bit each entry is regardless of type
            UNITY_DEFINE_INSTANCED_PROP(fixed2, unity_SpriteFlipArray)
        UNITY_INSTANCING_BUFFER_END(PerDrawSprite)

        #define _RendererColor  UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteRendererColorArray)
        #define _Flip           UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteFlipArray)

    #endif // instancing

    CBUFFER_START(UnityPerDrawSprite)
    #ifndef UNITY_INSTANCING_ENABLED
        fixed4 _RendererColor;
        fixed2 _Flip;
    #endif
        float _EnableExternalAlpha;
    CBUFFER_END

        // Material Color.
        fixed4 _Color;
        fixed _NoiseAmt;
        fixed _NoiseScaleX;
        fixed _NoiseScaleY;
        fixed _NoiseSpeed;

        struct appdata_t
        {
            float4 vertex   : POSITION;
            float4 color    : COLOR;
            float2 texcoord : TEXCOORD0;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        struct v2f
        {
            float4 vertex   : SV_POSITION;
            fixed4 color : COLOR;
            float2 texcoord : TEXCOORD0;
            UNITY_VERTEX_OUTPUT_STEREO
        };

        inline float4 UnityFlipSprite(in float3 pos, in fixed2 flip)
        {
            return float4(pos.xy * flip, pos.z, 1.0);
        }

        v2f SpriteVert(appdata_t IN)
        {
            v2f OUT;

            UNITY_SETUP_INSTANCE_ID(IN);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

            OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
            OUT.vertex = UnityObjectToClipPos(OUT.vertex);
            OUT.texcoord = IN.texcoord;
            OUT.color = IN.color * _Color * _RendererColor;

            #ifdef PIXELSNAP_ON
            OUT.vertex = UnityPixelSnap(OUT.vertex);
            #endif

            return OUT;
        }

        sampler2D _MainTex;
        sampler2D _AlphaTex;
        sampler2D _NoiseTex;

        fixed4 SampleSpriteTexture(float2 uv)
        {
            fixed4 color = tex2D(_MainTex, uv);

        #if ETC1_EXTERNAL_ALPHA
            fixed4 alpha = tex2D(_AlphaTex, uv);
            color.a = lerp(color.a, alpha.r, _EnableExternalAlpha);
        #endif

            return color;
        }

        fixed4 SpriteFrag(v2f IN) : SV_Target
        {
            float2 noiseUV = IN.texcoord * float2(_NoiseScaleX, _NoiseScaleY);
            float2 noise = tex2D(_NoiseTex, noiseUV);
            float2 xyOffset = tex2D(_NoiseTex, noiseUV + float2(_Time.x, _Time.x)).rg;
            float2 offsetNoise = noise.xy + xyOffset;
            float2 finalNoise = offsetNoise * _NoiseAmt;
            //float2 uv = IN.texcoord + noise.xy * _NoiseAmt;
            float2 uv = IN.texcoord + finalNoise;
            fixed4 c = SampleSpriteTexture(uv) * IN.color;
            c.rgb *= c.a;
            return c;



            // sample noise texture red and green as x and y offsets, 
            //  scrolling the noise texture over time.
            //float2 xyOffset = tex2D(_NoiseTex, uv + float2(_Time.x, _Time.x)).rg;
            // sample the sprite texture at the new offset that changes over time.
            //fixed4 color = tex2D(_MainTex, uv + xyOffset);
            //return color;
        }

        #endif // UNITY_SPRITES_INCLUDED

    ENDCG
    }
        }
}