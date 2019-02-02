Shader "Projector/Tattoo" {
	Properties{
		_ShadowTex("Cookie", 2D) = "white" {}
	   _alpha("Alpha", Range(0.0,1.0)) = 1.
	}

		Subshader{
			Tags {
				"RenderType" = "Transparent"
				"Queue" = "Transparent+100"
			}
			Pass {
				ZWrite Off
				ColorMask RGB
				Blend DstColor Zero
		        Blend OneMinusSrcAlpha SrcAlpha
				Offset -1, -1

				Fog{ Mode Off }

				
			//	Blend OneMinusSrcAlpha SrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
		    	#pragma multi_compile_fog
			//	#pragma fragmentoption ARB_fog_exp2
				//#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				struct v2f
				{
		   float4 uv : TEXCOORD0;
		   float4 uvFalloff : TEXCOORD1;
		            UNITY_FOG_COORDS(2)
					float4 pos : SV_POSITION;
					
				};

				sampler2D _ShadowTex;
				float _alpha;
				float4x4 unity_Projector;
				float4 _Color;
				

				v2f vert(float4 vertex : POSITION)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(vertex);
					o.uv = mul(unity_Projector, vertex);
					UNITY_TRANSFER_FOG(o, o.pos);
					return o;
				}

				half4 frag(v2f i) : SV_Target
				{
					half4 tex = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD (i.uv));
					//tex.a =  1 - tex.a;
					
					if (tex.a > 0.1) { tex.a = 1 - tex.a; }
					else
					tex.a = 1;
					if (i.uv.w < 0.0)
					{
						tex = float4(0,0,0,1);
					}
				//	fixed4 texF = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvFalloff));
					//fixed4 res = lerp(fixed4(0, 0, 0, 0), tex, texF.a);
					UNITY_APPLY_FOG_COLOR(i.fogCoord, tex, fixed4(1, 1, 1, 1));
					return tex;
				}
				ENDCG
			}
	}
}