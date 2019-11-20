
namespace Flux.src.Flux.Renderer
{
	public class ShapeFactory : AbstractShapeFactory
	{
		private readonly IShapeFactory _factory;
		// TODO: private readonly enum shapeType
		public enum FactoryType { Quad, Cube, Triangle, Pyramid};

		public ShapeFactory(FactoryType type)
		{
			switch (type)
			{
				case FactoryType.Quad:
					_factory = (IShapeFactory)new QuadShapeFactory();
					break;
				case FactoryType.Cube:
					_factory = (IShapeFactory)new CubeShapeFactory();
					break;
				case FactoryType.Pyramid:
					_factory = (IShapeFactory)new PyramidShapeFactory();
					break;
				case FactoryType.Triangle:
					_factory = (IShapeFactory)new TriangleShapeFactory();
					break;
				default:
					break;
			}
		}

		public override IShape Create()
		{
			return _factory.Create();
		}
	}
}
