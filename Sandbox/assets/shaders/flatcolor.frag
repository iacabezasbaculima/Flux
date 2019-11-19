#version 330 core

layout(location = 0) out vec4 color; 
//out vec4 outColor;

uniform vec4 uColor;

in vec3 vColor;

void main()
{
	color = vec4(vColor, 1);
	//color = uColor;
}