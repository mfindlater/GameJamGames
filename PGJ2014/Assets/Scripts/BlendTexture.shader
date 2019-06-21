Shader "Custom/BlendTexture" {

  Properties {
	_Blend ("Blend", Range (0, 1) ) = 0.5 
	_DayTexture("Day Texture", 2D) = "" 
	_NightTexture ("Night Texture", 2D) = ""
	}
 
SubShader {	
	Pass {
		SetTexture[_DayTexture]
		SetTexture[_NightTexture] { 
			ConstantColor (0,0,0, [_Blend]) 
			Combine texture Lerp(constant) previous
		}		
	}
}
} 
