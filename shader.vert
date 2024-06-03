#version 440 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec4 color;

out vec4 FragColor;

layout (location = 20) uniform mat4 projection;
layout (location = 21) uniform mat4 view;
layout (location = 22) uniform mat4 model;

void main() 
{
    gl_Position = projection * model * view * position;
    
    FragColor = color;
}