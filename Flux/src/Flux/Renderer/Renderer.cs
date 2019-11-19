
namespace Flux.src.Flux.Renderer
{
	public class Renderer : RenderCommand
	{
		static SceneData sceneData;
		public static void BeginScene(Camera cam) 
		{
			sceneData.ViewMatrix = cam.GetViewMatrix();
			sceneData.ProjectionMatrix = cam.GetProjectionMatrix();
		}
		public static void EndScene() { }
		public static void Submit(Shader shader, Platform.OpenGL.OpenGLVertexArray vertexArray, OpenTK.Matrix4 transform)
		{
			shader.Use();
			shader.SetMatrix4("model", transform);
			shader.SetMatrix4("view", sceneData.ViewMatrix);
			shader.SetMatrix4("projection", sceneData.ProjectionMatrix);
			//TODO: GLDrawIndexed are still visible here! Ask for help

			vertexArray.Bind();
			DrawIndexed(vertexArray);
		}
		public static IShape CreateCube()
		{
			var factory = new ShapeFactory(ShapeFactory.FactoryType.Cube);
			return factory.Create();
		}
		public static IShape CreateQuad()
		{
			var factory = new ShapeFactory(ShapeFactory.FactoryType.Quad);
			return factory.Create();
		}
		public static IShape CreatePyramid()
		{
			var factory = new ShapeFactory(ShapeFactory.FactoryType.Pyramid);
			return factory.Create();
		}
		public struct SceneData
		{
			public OpenTK.Matrix4 ViewMatrix { get; set; }
			public OpenTK.Matrix4 ProjectionMatrix { get; set; }
		}
	}
}
	