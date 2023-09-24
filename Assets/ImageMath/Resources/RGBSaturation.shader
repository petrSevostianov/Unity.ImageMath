Shader "ImageMath/Operations/RGBSaturation"
{
    Properties {}
    SubShader {

        Cull Off ZWrite Off ZTest Always

        CGINCLUDE
        #include "UnityCG.cginc"

        struct VSI {
            float4 position : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct VSO {
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        VSO vert(VSI input) {
            VSO result;
            result.position = UnityObjectToClipPos(input.position);
            result.uv = input.uv;
            return result;
        }

        float4 ImageMath_V0;
        sampler2D ImageMath_T0;

        float4 frag(VSO input) : SV_Target{
            float4 t = tex2D(ImageMath_T0, input.uv);

            float origin = (t.r + t.g + t.b) / 3;
            //float origin = max(t.r, max(t.g, t.b));
            float4 result = float4((t.xyz - origin) * ImageMath_V0 + origin, t.a);
            return result;
        }

        ENDCG

        Pass {
            //Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag            
            ENDCG
        }

        Pass{
            Blend One One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag            
            ENDCG
        }

        Pass{
            Blend DstColor Zero
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag            
            ENDCG
        }

    }
}
