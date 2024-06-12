#version 440
in vec2 vs_textureOff;
in vec4 vs_color;

uniform sampler2D textureObject;

out vec4 color;

void main(void)
{
 vec4 alpha = texture(textureObject, vs_textureOff);
 color = vs_color;
 color.a = alpha.r;
}
