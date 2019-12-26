using Flux.src.Flux.Renderer;

namespace Flux.src.Flux.Renderer
{
	public class Renderer
	{
		public static void Init()
		{
			RenderCommand.Init();
		}
		public static void BeginScene(Camera camera)
		{
			sceneData.ViewProjectionMatrix = camera.GetViewProjectMatrix();
		}
		public static void EndScene()
		{
			//TODO: Need to decide what to do here
		}
		public static void Submit(Shader shader, VertexArray vertexArray, OpenTK.Matrix4 transform)
		{
			shader.Bind();
			shader.SetMatrix4("ViewProjection", sceneData.ViewProjectionMatrix);
			shader.SetMatrix4("model", transform);

			vertexArray.Bind();
			RenderCommand.DrawIndexed(vertexArray);
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
		public static IShape CreateTriangle()
		{
			var factory = new ShapeFactory(ShapeFactory.FactoryType.Triangle);
			return factory.Create();
		}
		private struct SceneData
		{
			public OpenTK.Matrix4 ViewProjectionMatrix { get; set; }
		}
		private static SceneData sceneData;
	}
}
	