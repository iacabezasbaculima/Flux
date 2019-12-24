#version 330 core

in vec2 texCoords;

out vec4 outColor;

uniform sampler2D guiTex;

void main()
{
	outColor = texture(guiTex, texCoords);
}
