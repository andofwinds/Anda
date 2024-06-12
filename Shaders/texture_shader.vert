#version 440 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 textureCoord;
layout (location = 2) in vec2 textureOff;

out vec2 vs_textureCoord;
out vec2 vs_textureOff;

layout (location = 20) uniform mat4 projection;
layout (location = 21) uniform mat4 modelView;

void main()
{
  vs_textureCoord = textureCoord;
  vs_textureOff = textureOff;
  gl_Position = projection * modelView * position;
}
