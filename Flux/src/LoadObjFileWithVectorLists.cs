using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Flux.src.Flux.Renderer;

namespace Flux.src
{
	public class LoadObjFileWithVectorLists : GameWindow
	{
		FreeSpaceCameraController camController;
		Shader flatShader;
		IShape cube;

		Tuple<float[], float[], uint[]> ModelData;
		Tuple<Vector3[], Vector2[], uint[]> TestData;
		int vao;
		Texture2D tex; 

		public LoadObjFileWithVectorLists() : base(512,512, new GraphicsMode(32,24,0,4), "Load .obj model with vector lists.")
		{
			camController = new FreeSpaceCameraController(Vector3.UnitZ * 10, Width / (float)Height);
			flatShader = Shader.Create("Texture ", "texture.vert", "texture.frag");
			cube = Renderer.CreateCube();

			ModelData = ObjLoader.LoadFromFile("mycube.obj");
			TestData = ObjLoader.Load("mycube.obj");

			int[] vboHandles = new int[3];
			GL.GenBuffers(3, vboHandles);
			int vboPos = vboHandles[0];
			int vboTex = vboHandles[1];
			int ibo = vboHandles[2];

			GL.BindBuffer(BufferTarget.ArrayBuffer, vboPos);
			GL.BufferData(BufferTarget.ArrayBuffer, TestData.Item1.Length * Vector3.SizeInBytes, TestData.Item1, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, vboTex);
			GL.BufferData(BufferTarget.ArrayBuffer, (TestData.Item2.Length * Vector2.SizeInBytes), TestData.Item2, BufferUsageHint.StaticDraw);

			GL.GenVertexArrays(1, out vao);
			GL.BindVertexArray(vao);

			GL.EnableVertexAttribArray(0);  // pos
			GL.EnableVertexAttribArray(1);  // tex

			GL.BindBuffer(BufferTarget.ArrayBuffer, vboPos);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, vboTex);
			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);
			GL.BufferData(BufferTarget.ElementArrayBuffer, TestData.Item3.Length * sizeof(uint), TestData.Item3, BufferUsageHint.StaticDraw);

			GL.BindVertexArray(0);

			tex = Texture2D.Create("crate.jpg");

			Console.WriteLine();
		}

		protected override void OnLoad(EventArgs e)
		{
			RenderCommand.Init();
			RenderCommand.SetClearColor(Color4.AliceBlue);
			RenderCommand.Clear();

			RenderFrame += LoadObjFileWithVectorLists_RenderFrame;
			UpdateFrame += LoadObjFileWithVectorLists_UpdateFrame;
		}

		private void LoadObjFileWithVectorLists_UpdateFrame(object sender, FrameEventArgs e)
		{
			if (!Focused) return;

			camController.OnUpdate(e.Time);
		}

		private void LoadObjFileWithVectorLists_RenderFrame(object sender, FrameEventArgs e)
		{
			RenderCommand.Clear();
			Matrix4 model = Matrix4.Identity;
			flatShader.Bind();
			flatShader.SetMVPMatrix(model, camController.Camera.ViewMatrix, camController.Camera.ProjectionMatrix);

			GL.BindVertexArray(vao);
			tex.Bind();
			GL.DrawElements(PrimitiveType.Triangles, TestData.Item3.Length, DrawElementsType.UnsignedInt, 0);
			GL.BindVertexArray(0);
			SwapBuffers();
		}

		protected override void OnResize(EventArgs e)
		{
			RenderCommand.SetViewport(0, 0, Width, Height);
			camController.OnResize(Width, Height);
		}
		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			if (Focused) Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
		}
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			camController.OnMouseScroll(e.DeltaPrecise);
		}
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Exit();
		}
	}
}
