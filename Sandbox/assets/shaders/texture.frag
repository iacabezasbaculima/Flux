#version 430 core

out vec4 outColor;

in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
	//vec2 f = vec2(texCoord.x, 1.0 - texCoord.y);
	outColor = texture(texture0, texCoord);
}