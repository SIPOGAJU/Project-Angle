// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'



Shader "Custom/NewSurfaceShader"

{

    Properties

    {

        _Color ("Color", Color) = (240, 255, 0, 1)

        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _Glossiness ("Smoothness", Range(0,1)) = 0.5

        _Metallic ("Metallic", Range(0,1)) = 0.0

    }

  

    SubShader 

    {

        Pass 

    {

    CGPROGRAM



    #pragma vertex vert             

    #pragma fragment frag



    struct vertInput {

        float4 pos : POSITION;

    };  



    struct vertOutput {

        float4 pos : SV_POSITION;

    };



    vertOutput vert(vertInput input) {

        vertOutput o;

        o.pos = UnityObjectToClipPos(input.pos);

        return o;

    }



    half4 frag(vertOutput output) : COLOR {

        return half4(240, 255, 0, 1); 

    }

    ENDCG

}

    }

    

    FallBack "Diffuse"

}