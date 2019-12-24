using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using Flux.src.Flux.Renderer;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLShader : Shader
	{
		public int ProgramHandle { get; private set; } = -1;
		public string ShaderName { get; private set; }
		public int AttributeCount { get; private set; } = 0;
		public int UniformCount { get; private set; } = 0;

		public Dictionary<string, AttributeInfo> Attributes = new Dictionary<string, AttributeInfo>();
		public Dictionary<string, UniformInfo> Uniforms = new Dictionary<string, UniformInfo>();

		public OpenGLShader(string shaderName, string vertFilename, string fragFilename)
		{
			ShaderName = shaderName;
			LoadFromFile(vertFilename, fragFilename);
		}
		private void LoadFromFile(string vertFileName, string fragFileName)
		{
			// 1. Load vertex/fragment shader code
			// 2. Create vertex/fragment shader objects
			// 3. Compile vertex/fragment shaders
			// 4. Create shader program
			// 5. Attach shader objects and link shader program
			// 6. Detach shader objects and delete

			string vertexSource = LoadSource(vertFileName);
			int vertexShader = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vertexShader, vertexSource);
			CompileShader(vertexShader);

			string fragmentSource = LoadSource(fragFileName);
			int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fragmentShader, fragmentSource);
			CompileShader(fragmentShader);

			ProgramHandle = GL.CreateProgram();

			GL.AttachShader(ProgramHandle, vertexShader);
			GL.AttachShader(ProgramHandle, fragmentShader);

			LinkProgram();
		
			GL.DetachShader(ProgramHandle, vertexShader);
			GL.DetachShader(ProgramHandle, fragmentShader);
			GL.DeleteShader(vertexShader);
			GL.DeleteShader(fragmentShader);
		}
		private string LoadSource(string filename)
		{
			// All shader files are located in assets/shaders/ directory
			// so we hardcode the path here to read from file
			string path = $"assets/shaders/{filename}";

			// Load the vertex shader file and return as a string
			using (var src = new StreamReader(path, Encoding.UTF8))
			{
				return src.ReadToEnd();
			}
		}
		private void CompileShader(int shaderHandle)
		{
			// Try to compile shader
			GL.CompileShader(shaderHandle);

			// Check for compilation errors
			GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out var result);
			if (result != (int)All.True)
			{
				throw new ShaderProgramException(GL.GetShaderInfoLog(shaderHandle));
			}
		}
		private void LinkProgram()
		{
			// Link shader program
			GL.LinkProgram(ProgramHandle);

			// Check for linking errors
			GL.GetProgram(ProgramHandle, GetProgramParameterName.LinkStatus, out var result);
			if (result != (int)All.True)
			{
				throw new ShaderProgramException("Error linking shader program.");
			}
			else
			{
				// Get all active attributes and uniforms
				GetAttributes();
				GetUniforms();
				Console.WriteLine($"\n{ShaderName.ToUpper()} shader has been successfully created:\nID: ({ProgramHandle})");
				foreach (var item in Uniforms)
				{
					Console.WriteLine($"{item.Key, -20}: {item.Value.type}");
				}
				Console.WriteLine("\n");
			}
		}
		private void GetAttributes()
		{
			GL.GetProgram(ProgramHandle, GetProgramParameterName.ActiveAttributes, out var numberOfAttribs);

			for (int i = 0; i < numberOfAttribs; i++)
			{
				AttributeInfo info = new AttributeInfo();
				int length = 0;

				GL.GetActiveAttrib(ProgramHandle, i, 256, out length, out info.size, out info.type, out info.name);

				Attributes.Add(info.name, info);
			}
		}
		private void GetUniforms()
		{
			GL.GetProgram(ProgramHandle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

			for (int i = 0; i < numberOfUniforms; i++)
			{
				UniformInfo info = new UniformInfo();
				int length = 0;

				GL.GetActiveUniform(ProgramHandle, i, 256, out length, out info.size, out info.type, out info.name);
				info.location = GL.GetUniformLocation(ProgramHandle, info.name);
				Uniforms.Add(info.name, info);
			}
		}
		public override void Bind()
		{
			GL.UseProgram(ProgramHandle);
		}
		public override void Unbind()
		{
			GL.UseProgram(0);
		}
		public override void SetInt(string name, int val)
		{
			GL.Uniform1(Uniforms[name].location, val);
		}
		public override void SetFloat(string name, float val)
		{
			GL.Uniform1(Uniforms[name].location, val);
		}
		public override void SetMatrix4(string name, OpenTK.Matrix4 m, bool transpose = true)
		{
			GL.UniformMatrix4(Uniforms[name].location, transpose, ref m);
		}
		public override void SetVector3(string name, OpenTK.Vector3 v)
		{
			GL.Uniform3(Uniforms[name].location, ref v);
		}
		public override void SetVector4(string name, OpenTK.Vector4 v)
		{
			GL.Uniform4(Uniforms[name].location, ref v);
		}
		public override void SetVector4(string name, OpenTK.Graphics.Color4 c)
		{
			GL.Uniform4(Uniforms[name].location, c);
		}
		public class AttributeInfo
		{
			public string name = "";
			public int location = -1;
			public int size = 0;
			public ActiveAttribType type;
		}
		public class UniformInfo
		{
			public string name = "";
			public int location = -1;
			public int size = 0;
			public ActiveUniformType type;
		}
		class ShaderProgramException : Exception
		{
			public ShaderProgramException(string message) : base(message) { }
		}
	}
}
