#version 330 core

out vec4 outColor;

in vec3 vColor;

//uniform sampler2D texture0;

void main()
{
	outColor = vec4(vColor, 1);
}