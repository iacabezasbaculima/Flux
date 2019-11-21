using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Flux.src.Flux.Core;
using FL = Flux.src.Flux.Renderer;

namespace Flux.src
{
	public class SandBox : Window
	{
		FL.IShape cube;
		FL.IShape quad;
		FL.IShape pyramid;
		FL.IShape triangle;
		FL.Texture woodTex;
		FL.Shader simple;
		FL.Shader texShader;
		FL.Shader lightShader;
		FL.Shader lampShader;
		static 
		// This is the position of both the light and the place the lamp cube will be drawn in the scene
		private readonly Vector3 _lightPos = new Vector3(2f, 0.0f, 0.0f);

		#region CAMERA 
		FL.Camera camera;
		bool firstMove = true;
		Vector2 lastPos;
		const float cameraSpeed = 1.5f;
		const float sensitivity = 0.2f;
		#endregion

		#region CUBE VERTICES
		// Here we now have added the normals of the vertices
		// Remember to define the layouts to the VAO's
		private readonly float[] vertices =
		{
             // Position          Normal
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
			 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
			 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
			-0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
			-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

			-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
			-0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
			-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

			-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
			-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
			-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
			-0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
			-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

			 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
			 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
			 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
			 0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
			 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

			-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
			 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
			 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
			-0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
			-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

			-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
			 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
			-0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
			-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
		};
		// Here we now have added the normals of the vertices
		// Remember to define the layouts to the VAO's
		private readonly float[] _vertices =
		{
             // Position        
            -0.5f, -0.5f, -0.5f, // Front face
             0.5f, -0.5f, -0.5f,
			 0.5f,  0.5f, -0.5f,
			 0.5f,  0.5f, -0.5f,
			-0.5f,  0.5f, -0.5f,
			-0.5f, -0.5f, -0.5f,

			-0.5f, -0.5f,  0.5f, // Back face
             0.5f, -0.5f,  0.5f,
			 0.5f,  0.5f,  0.5f,
			 0.5f,  0.5f,  0.5f,
			-0.5f,  0.5f,  0.5f,
			-0.5f, -0.5f,  0.5f,

			-0.5f,  0.5f,  0.5f, // Left face
            -0.5f,  0.5f, -0.5f,
			-0.5f, -0.5f, -0.5f,
			-0.5f, -0.5f, -0.5f,
			-0.5f, -0.5f,  0.5f,
			-0.5f,  0.5f,  0.5f,

			 0.5f,  0.5f,  0.5f, // Right face
             0.5f,  0.5f, -0.5f,
			 0.5f, -0.5f, -0.5f,
			 0.5f, -0.5f, -0.5f,
			 0.5f, -0.5f,  0.5f,
			 0.5f,  0.5f,  0.5f,

			-0.5f, -0.5f, -0.5f, // Bottom face
             0.5f, -0.5f, -0.5f,
			 0.5f, -0.5f,  0.5f,
			 0.5f, -0.5f,  0.5f,
			-0.5f, -0.5f,  0.5f,
			-0.5f, -0.5f, -0.5f,

			-0.5f,  0.5f, -0.5f, // Top face
             0.5f,  0.5f, -0.5f,
			 0.5f,  0.5f,  0.5f,
			 0.5f,  0.5f,  0.5f,
			-0.5f,  0.5f,  0.5f,
			-0.5f,  0.5f, -0.5f 
		};
		#endregion

		public SandBox(int width, int height, string title) : base(width, height, new GraphicsMode(32, 24, 0, 8), title) { }
		protected override void OnLoad(EventArgs e)
		{
			FL.RenderCommand.SetClearColor(Color4.Black);
			FL.RenderCommand.EnableDepthTest();
			FL.RenderCommand.EnableMSAA();
			FL.RenderCommand.Clear();

			simple = new FL.Shader("assets/shaders/simple.vert", "assets/shaders/simple.frag");
			if (simple != null) Console.WriteLine($"{"Simple Shader loaded successfully."}{" ID: "+simple.Handle}");
			texShader = new FL.Shader("assets/shaders/texture.vert", "assets/shaders/texture.frag");
			if (texShader != null) Console.WriteLine($"{"Texture Shader loaded successfully."}{" ID: " + texShader.Handle}");

			lightShader = new FL.Shader("assets/shaders/light.vert", "assets/shaders/light.frag");
			if (lightShader != null) Console.WriteLine($"{"Light Shader loaded successfully."}{" ID: " + lightShader.Handle}");

			lampShader = new FL.Shader("assets/shaders/light.vert", "assets/shaders/lamp.frag");
			if (lampShader != null) Console.WriteLine($"{"Lamp Shader loaded successfully."}{" ID: " + lampShader.Handle}");

			// Wooden crate texture
			woodTex = new FL.Texture("assets/images/crate.jpg");

			// Camera
			camera = new FL.Camera(Vector3.UnitZ * 10, Width / (float)Height);
			
			// Shapes
			quad = FL.Renderer.CreateQuad();
			cube = FL.Renderer.CreateCube();
			pyramid = FL.Renderer.CreatePyramid();
			triangle = FL.Renderer.CreateTriangle();

			CursorVisible = false;

			base.OnLoad(e);
		}
		public double t = 0;
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			t += 3.5 + e.Time;
			FL.RenderCommand.Clear();
			FL.Renderer.BeginScene(camera);
			{
				#region THREE SHAPES
				//var md = Matrix4.Identity;
				//md *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)t));
				//pyramid.Draw(simple, md, camera.GetViewMatrix(), camera.GetProjectionMatrix());

				//md *= Matrix4.CreateScale(0.2f) * Matrix4.CreateTranslation(-3f, 0f, 0f);
				//quad.Draw(simple, md, camera.GetViewMatrix(), camera.GetProjectionMatrix());

				//md *= Matrix4.CreateTranslation(6f, 0f, 0f);
				//cube.Draw(simple, md, camera.GetViewMatrix(), camera.GetProjectionMatrix());

				//var trimd = Matrix4.CreateTranslation(0f, 3f, 0f);
				//triangle.Draw(texShader, trimd, camera.GetViewMatrix(), camera.GetProjectionMatrix());
				#endregion

				lightShader.Use();

				lightShader.SetMatrix4("model", Matrix4.Identity);
				lightShader.SetMatrix4("view", camera.GetViewMatrix());
				lightShader.SetMatrix4("projection", camera.GetProjectionMatrix());
				
				lightShader.SetVector3("viewPos", camera.Position);

				// Here we set the material values of the cube, the material struct is just a container so to access
				// the underlying values we simply type "material.value" to get the location of the uniform
				lightShader.SetVector3("material.ambient", new Vector3(1.0f, 0.5f, 0.31f));
				lightShader.SetVector3("material.diffuse", new Vector3(1.0f, 0.5f, 0.31f));
				lightShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
				lightShader.SetFloat("material.shininess", 32.0f);

				// This is where we change the lights color over time using the sin function
				Vector3 lightColor;
				float time = DateTime.Now.Second + DateTime.Now.Millisecond / 1000f;
				Console.WriteLine(time);
				lightColor.X = (float)Math.Sin(time * 2.0f);
				lightColor.Y = (float)Math.Sin(time * 0.7f);
				lightColor.Z = (float)Math.Sin(time * 1.3f);

				// The ambient light is less intensive than the diffuse light in order to make it less dominant
				Vector3 ambientColor = lightColor * new Vector3(0.2f);
				Vector3 diffuseColor = lightColor * new Vector3(0.5f);

				lightShader.SetVector3("light.position", _lightPos);
				lightShader.SetVector3("light.ambient", ambientColor);
				lightShader.SetVector3("light.diffuse", diffuseColor);
				lightShader.SetVector3("light.specular", new Vector3(1.0f, 1.0f, 1.0f));

				cube.Draw();

				// Draw lamp
				lampShader.Use();

				Matrix4 lampMatrix = Matrix4.Identity;
				lampMatrix *= Matrix4.CreateScale(0.2f); 
				lampMatrix *= Matrix4.CreateTranslation(_lightPos);

				lampShader.SetMatrix4("model", lampMatrix);
				lampShader.SetMatrix4("view", camera.GetViewMatrix());
				lampShader.SetMatrix4("projection", camera.GetProjectionMatrix());

				cube.Draw();

			}
			FL.Renderer.EndScene();

			Context.SwapBuffers();

			base.OnRenderFrame(e);
		}
		protected override void OnResize(EventArgs e)
		{
			FL.RenderCommand.SetViewport(0, 0, Width, Height); // Maps Native Device Coordinates (NDC) to Window context
			camera.AspectRatio = Width / (float)Height;
			base.OnResize(e);
		}
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
			
			if (!Focused) // check to see if the window is focused
			{
				return;
			}

			var input = Keyboard.GetState();

			if (input.IsKeyDown(Key.W))
				camera.Position += camera.Front * cameraSpeed * (float)e.Time; // Forward 
			if (input.IsKeyDown(Key.S))
				camera.Position -= camera.Front * cameraSpeed * (float)e.Time; // Backwards
			if (input.IsKeyDown(Key.A))
				camera.Position -= camera.Right * cameraSpeed * (float)e.Time; // Left
			if (input.IsKeyDown(Key.D))
				camera.Position += camera.Right * cameraSpeed * (float)e.Time; // Right
			if (input.IsKeyDown(Key.LShift))
				camera.Position += camera.Up * cameraSpeed * (float)e.Time; // Up 
			if (input.IsKeyDown(Key.LControl))
				camera.Position -= camera.Up * cameraSpeed * (float)e.Time; // Down
			
			
			// Get the mouse state
			var mouse = Mouse.GetState();
			
			if (firstMove) // this bool variable is initially set to true
			{
				lastPos = new Vector2(mouse.X, mouse.Y);
				firstMove = false;
			}
			else
			{
				// Calculate the offset of the mouse position
				var deltaX = mouse.X - lastPos.X;
				var deltaY = mouse.Y - lastPos.Y;
				lastPos = new Vector2(mouse.X, mouse.Y);

				// Apply the camera pitch and yaw (we clamp the pitch in the camera class)
				camera.Yaw += deltaX * sensitivity;
				camera.Pitch -= deltaY * sensitivity; // reversed since y-coordinates range from bottom to top
												   //Console.WriteLine("Yaw: {0}, Pitch: {1}", camera.Yaw, camera.Pitch);
			}

		}
		protected override void OnMouseMove(MouseMoveEventArgs e)
		{
			base.OnMouseMove(e);
			if (Focused) // check to see if the window is focused
			{
				Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
			}
		}
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			camera.Fov -= e.DeltaPrecise;
			base.OnMouseWheel(e);
		}
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Key == Key.Escape)
				Exit();
				
		}
		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{
			base.OnKeyUp(e);

		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			var input = Keyboard.GetState();

			//if (input.IsKeyDown(Key.N)) isready = true;

			base.OnKeyPress(e);
		}
	}
}
