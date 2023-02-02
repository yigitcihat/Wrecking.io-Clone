// Toony Colors Pro+Mobile 2
// (c) 2014-2020 Jean Moreno

Shader "UKIYO/Water/MobileWater"
{
	Properties
	{
		[TCP2HelpBox(Warning,Make sure that the Camera renders the depth texture for this material to work properly.    You can use the script __TCP2_CameraDepth__ for this.)]
	[TCP2HeaderHelp(BASE, Base Properties)]
		//TOONY COLORS
		_HColor ("Highlight Color", Color) = (0.6,0.6,0.6,1.0)
		_SColor ("Shadow Color", Color) = (0.3,0.3,0.3,1.0)

		//DIFFUSE
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
	[TCP2Separator]

		//TOONY COLORS RAMP
		_RampThreshold ("Ramp Threshold", Range(0,1)) = 0.5
		_RampSmooth ("Ramp Smoothing", Range(0.001,1)) = 0.1
	[TCP2Separator]
	[TCP2HeaderHelp(WATER)]
		_Color ("Water Color (RGB) Opacity (A)", Color) = (0.5,0.5,0.5,1.0)

		[Header(Depth Color)]
		_DepthColor ("Depth Color", Color) = (0.5,0.5,0.5,1.0)
		[PowerSlider(5.0)] _DepthDistance ("Depth Distance", Range(0.01,3)) = 0.5

		[Header(Foam)]
		_FoamSpread ("Foam Spread", Range(0.01,5)) = 2
		_FoamStrength ("Foam Strength", Range(0.01,1)) = 0.8
		_FoamColor ("Foam Color (RGB) Opacity (A)", Color) = (0.9,0.9,0.9,1.0)
		[NoScaleOffset]
		_FoamTex ("Foam (RGB)", 2D) = "white" {}
		_FoamSmooth ("Foam Smoothness", Range(0,0.5)) = 0.02
		_FoamSpeed ("Foam Speed", Vector) = (2,2,2,2)
		[Header(Depth based Transparency)]
		[PowerSlider(5.0)] _DepthAlpha ("Depth Alpha", Range(0.01,10)) = 0.5
		_DepthMinAlpha ("Depth Min Alpha", Range(0,1)) = 0.5

		[Header(Vertex Waves Animation)]
		_WaveSpeed ("Speed", Float) = 2
		_WaveHeight ("Height", Float) = 0.1
		_WaveFrequency ("Frequency", Range(0,10)) = 1
	[TCP2Separator]
	[TCP2HeaderHelp(TRANSPARENCY)]
		//Blending
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlendTCP2 ("Blending Source", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlendTCP2 ("Blending Dest", Float) = 10
	[TCP2Separator]
		//Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}

	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Blend [_SrcBlendTCP2] [_DstBlendTCP2]


		CGPROGRAM

		#pragma surface surf ToonyColorsWater keepalpha vertex:vert nolightmap
		#pragma target 2.5

		//================================================================
		// VARIABLES

		fixed4 _Color;
		sampler2D _MainTex;
		float4 _MainTex_ST;
		sampler2D_float _CameraDepthTexture;
		fixed4 _DepthColor;
		half _DepthDistance;
		half4 _FoamSpeed;
		half _FoamSpread;
		half _FoamStrength;
		sampler2D _FoamTex;
		fixed4 _FoamColor;
		half _FoamSmooth;
		half _DepthAlpha;
		fixed _DepthMinAlpha;
		half _WaveHeight;
		half _WaveFrequency;
		half _WaveSpeed;



		struct Input
		{
			float2 texcoord;
			float4 sPos;
		};

		//================================================================
		// CUSTOM LIGHTING

		//Lighting-related variables
		half4 _HColor;
		half4 _SColor;
		half _RampThreshold;
		half _RampSmooth;

		// Instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		//Custom SurfaceOutput
		struct SurfaceOutputWater
		{
			half atten;
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			fixed Alpha;
		};

		inline half4 LightingToonyColorsWater (inout SurfaceOutputWater s, half3 viewDir, UnityGI gi)
		{
			half3 lightDir = gi.light.dir;
		#if defined(UNITY_PASS_FORWARDBASE)
			half3 lightColor = _LightColor0.rgb;
			half atten = s.atten;
		#else
			half3 lightColor = gi.light.color.rgb;
			half atten = 1;
		#endif

			s.Normal = normalize(s.Normal);			
			fixed ndl = max(0, dot(s.Normal, lightDir));
			#define NDL ndl
			#define		RAMP_THRESHOLD	_RampThreshold
			#define		RAMP_SMOOTH		_RampSmooth

			fixed3 ramp = smoothstep(RAMP_THRESHOLD - RAMP_SMOOTH*0.5, RAMP_THRESHOLD + RAMP_SMOOTH*0.5, NDL);
		#if !(POINT) && !(SPOT)
			ramp *= atten;
		#endif
		#if !defined(UNITY_PASS_FORWARDBASE)
			_SColor = fixed4(0,0,0,1);
		#endif
			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
			ramp = lerp(_SColor.rgb, _HColor.rgb, ramp);
			fixed4 c;
			c.rgb = s.Albedo * lightColor.rgb * ramp;
			c.a = s.Alpha;

		#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
			c.rgb += s.Albedo * gi.indirect.diffuse;
		#endif
			return c;
		}

		void LightingToonyColorsWater_GI(inout SurfaceOutputWater s, UnityGIInput data, inout UnityGI gi)
		{
			gi = UnityGlobalIllumination(data, 1.0, s.Normal);

			gi.light.color = _LightColor0.rgb;	//remove attenuation
			s.atten = data.atten;	//transfer attenuation to lighting function
		}

		//================================================================
		// VERTEX FUNCTION


		struct appdata_tcp2
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
			float4 texcoord2 : TEXCOORD2;
	#if !defined(LIGHTMAP_OFF) && defined(DIRLIGHTMAP_COMBINED)
			float4 tangent : TANGENT;
	#endif
	#if UNITY_VERSION >= 550
			UNITY_VERTEX_INPUT_INSTANCE_ID
	#endif
		};

			#define TIME (_Time.y)

		void vert(inout appdata_tcp2 v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);

			//Main texture UVs
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			float2 mainTexcoords = worldPos.xz * 0.1;
			o.texcoord.xy = TRANSFORM_TEX(mainTexcoords.xy, _MainTex);
			//vertex waves
			float3 _pos = worldPos.xyz * _WaveFrequency;
			float _phase = TIME * _WaveSpeed;
			half4 vsw_offsets_x = half4(1.0, 2.2, 2.7, 3.4);
			half4 vsw_ph_offsets_x = half4(1.0, 1.3, 0.7, 1.75);
			half4 vsw_offsets_z = half4(0.6, 1.3, 3.1, 2.4);
			half4 vsw_ph_offsets_z = half4(2.2, 0.4, 3.3, 2.9);

			half4 vsw_offsets_x2 = half4(1.4, 1.8, 4.2, 3.6);
			half4 vsw_ph_offsets_x2 = half4(0.2, 2.6, 0.7, 3.1);
			half4 vsw_offsets_z2 = half4(1.1, 2.8, 1.7, 4.3);
			half4 vsw_ph_offsets_z2 = half4(0.5, 4.8, 3.1, 2.3);

			half4 waveX = sin((_pos.xxxx * vsw_offsets_x) + (_phase.xxxx * vsw_ph_offsets_x));
			half4 waveZ = sin((_pos.zzzz * vsw_offsets_z) + (_phase.xxxx * vsw_ph_offsets_z));
			half4 waveX2 = sin((_pos.xxxx * vsw_offsets_x2) + (_phase.xxxx * vsw_ph_offsets_x2));
			half4 waveZ2 = sin((_pos.zzzz * vsw_offsets_z2) + (_phase.xxxx * vsw_ph_offsets_z2));

			float waveFactorX = (dot(waveX.xyzw, 1) + dot(waveX2.xyzw, 1)) * _WaveHeight / 8;
			float waveFactorZ = (dot(waveZ.xyzw, 1) + dot(waveZ2.xyzw, 1)) * _WaveHeight / 8;
		#define VSW_STRENGTH 1
			v.vertex.y += (waveFactorX + waveFactorZ) * VSW_STRENGTH;
			float4 pos = UnityObjectToClipPos(v.vertex);
			o.sPos = ComputeScreenPos(pos);
			COMPUTE_EYEDEPTH(o.sPos.z);
		}

		//================================================================
		// SURFACE FUNCTION

		void surf(Input IN, inout SurfaceOutputWater o)
		{
			fixed4 mainTex = tex2D(_MainTex, IN.texcoord.xy);
			float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.sPos));
			if(unity_OrthoParams.w > 0)
			{
				//orthographic camera
			#if defined(UNITY_REVERSED_Z)
				sceneZ = 1.0f - sceneZ;
			#endif
				sceneZ = (sceneZ * _ProjectionParams.z) + _ProjectionParams.y;
			}
			else
				//perspective camera
				sceneZ = LinearEyeDepth(sceneZ);
			float partZ = IN.sPos.z;
			float depthDiff = abs(sceneZ - partZ);
			//Depth-based foam
			half2 foamUV = IN.texcoord.xy;
			foamUV.xy += TIME.xx*_FoamSpeed.xy*0.05;
			fixed4 foam = tex2D(_FoamTex, foamUV);
			foamUV.xy += TIME.xx*_FoamSpeed.zw*0.05;
			fixed4 foam2 = tex2D(_FoamTex, foamUV);
			foam = (foam + foam2) / 2;
			float foamDepth = saturate(_FoamSpread * depthDiff);
			half foamTerm = (smoothstep(foam.r - _FoamSmooth, foam.r + _FoamSmooth, saturate(_FoamStrength - foamDepth)) * saturate(1 - foamDepth)) * _FoamColor.a;
			//Alter color based on depth buffer (soft particles technique)
			mainTex.rgb = lerp(_DepthColor.rgb, mainTex.rgb, saturate(_DepthDistance * depthDiff));	//N.V corrects the result based on view direction (depthDiff tends to not look consistent depending on view angle)));
			o.Albedo = lerp(mainTex.rgb * _Color.rgb, _FoamColor.rgb, foamTerm);
			_Color.a *= saturate((_DepthAlpha * depthDiff) + _DepthMinAlpha);
			o.Alpha = mainTex.a * _Color.a;
			o.Alpha = lerp(o.Alpha, _FoamColor.a, foamTerm);
		}

		ENDCG

	}

	//Fallback "Diffuse"
	CustomEditor "TCP2_MaterialInspector_SG"
}
