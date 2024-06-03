#version 440 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 textureCoord;

out vec2 vs_textureCoord;

layout (location = 20) uniform mat4 projection;
layout (location = 21) uniform mat4 view;
layout (location = 22) uniform mat4 model;

void main()
{
  vs_textureCoord = textureCoord;
  gl_Position = projection * model * view * position;
}
