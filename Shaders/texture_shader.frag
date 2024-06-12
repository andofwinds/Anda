#version 440 core

in vec2 vs_textureCoord;

uniform sampler2D textureObject;
out vec4 color;

void main()
{
  //color = texelFetch(textureObject, ivec2(vs_textureCoord.x, vs_textureCoord.y), 0);
  color = texture(textureObject, vs_textureCoord);
}
