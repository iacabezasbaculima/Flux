# Flux
Flux is primarily an early-stage interactive application and rendering engine for Windows. Currently, it is possible to create beautiful primitive shapes with flashy colors in a 3D scene.

OpenTK is used as a third-party toolkit library to access OpenGL API in C# .NET Framework. 

C# is the main programming language for this project.

## Getting Started

Visual Studio 2017 or 2019 is recommended, Flux is officially untested on other development environments whilst I focus on a Windows build.

You can clone the repository to a local destination using git:

`git clone --recursive https://github.com/iacabezasbaculima/Flux.git`

Make sure that you do a `--recursive` clone to fetch all of the submodules!

## The Plan
The plan for Flux is two-fold: to create a functional 3D engine, but also to serve as an education tool for learning game engine design and architecture. And last but not least important, to learn more C# programming. 

### Main features to expect:
- 2D rendering (UI, particles, sprites, etc.)
- Physically-Based 3D rendering (Maybe Image-Based Rendering)
- Support for Mac, Linux, Android iOS
    - Native rendering API support (DirectX, Vulkan, Metal)
- Integrated 3rd party 2D and 3D physics engine
- Procedural terrain and world generation
- Audio system
