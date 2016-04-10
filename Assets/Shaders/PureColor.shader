Shader "Custom/PureColor" {
	Properties{
		_Color ("颜色", Color) = (0.8 ,0,0,0)
	}
	SubShader {
		Pass{
			Color[_Color]
		}
	}
	FallBack "Diffuse"
}
