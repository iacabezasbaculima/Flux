
namespace Flux.src.Flux.Renderer
{
	public class Renderer
	{
		static SceneData sceneData;
		public static void BeginScene(Camera cam) 
		{
			sceneData.ViewMatrix = cam.GetViewMatrix();
			sceneData.ProjectionMatrix = cam.GetProjectionMatrix();
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
		public struct SceneData
		{
			public OpenTK.Matrix4 ViewMatrix { get; set; }
			public OpenTK.Matrix4 ProjectionMatrix { get; set; }
		}
	}
}
	