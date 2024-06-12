#version 440

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 textureCoord;
layout (location = 2) in vec2 textureOff;
layout (location = 3) in vec4 color;

out vec2 vs_textureOff;
out vec4 vs_color;

layout (location = 20) uniform mat4 projection;
layout (location = 21) uniform mat4 modelView;

void main()
{
  vs_textureOff = textureCoord + textureOff;

  gl_Position = projection * modelView * position;

  vs_color = color;
}
