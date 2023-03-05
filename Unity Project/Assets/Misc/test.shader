Shader "Unlit/test"
{
    Properties
    {
        // properties of shader
    }
        SubShader
    {
        //code of shaders

        Tags{
        "RenderType" = "Opaque"
        "Queue" = "Geometry"
        }

        Pass{
        CGPROGRAM
        #include "unityCG.cginc"
        #pragma vertex vert
        #pragma fragment frag

        struct appdata {
        float4 vertex: POSITION;
        };

    struct v2f { float4 position: SV_POSITION; };

        v2f vert(appdata v) {
        v2f o;
        o.position = UnityObjectToClipPos(v.vertex);
        return o;
        }

        fixed4 frag(v2f i) : SV_TARGET{
            return fixed4(0.2, 0, 0, 1);
        }

        ENDCG
    }
    }
}
