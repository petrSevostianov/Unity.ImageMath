Shader "ImageDisplay/ImageDisplay"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        //Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D Image;
            float ImageDisplayMultiplierPower;


            float4 frag(v2f i) : SV_Target{
              

                float4 color = tex2D(Image, i.uv);
                color.rgb *= pow(2, ImageDisplayMultiplierPower);
                /*color.rgb /= color.a;
                clip(color.a - 0.5);*/

                color.rgb = pow(color.rgb , 2.2);

                return color;
            }
            ENDCG
        }
    }
}
