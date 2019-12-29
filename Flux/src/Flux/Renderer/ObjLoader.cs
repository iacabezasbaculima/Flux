using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.src.Flux.Renderer
{
	public class ObjLoader
	{
		public static Tuple<float[], float[], uint[]> LoadFromFile(string filename)
		{
			try
			{
				string path = $"assets/models/{filename}";
				using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
				{
					string line;

					List<Vector3> vertices = new List<Vector3>();
					List<Vector2> textures = new List<Vector2>();
					List<Vector3> normals = new List<Vector3>();
					List<uint> indices = new List<uint>();

					float[] vertexData = null;
					float[] textureData = null;
					float[] normalData = null;
					uint[] indexData = null;

					while (true)
					{
						line = reader.ReadLine();
						String[] currentLine = line.Split(' ');

						if (line.StartsWith("v "))
						{
							Vector3 vertex = new Vector3();

							bool success = float.TryParse(currentLine[1], out vertex.X);
							success |= float.TryParse(currentLine[2], out vertex.Y);
							success |= float.TryParse(currentLine[3], out vertex.Z);

							vertices.Add(vertex);
						}
						else if (line.StartsWith("vt "))
						{
							Vector2 texture = new Vector2();

							bool success = float.TryParse(currentLine[1], out texture.X);
							success |= float.TryParse(currentLine[2], out texture.Y);

							textures.Add(texture);
						}
						else if (line.StartsWith("vn "))
						{
							Vector3 normal = new Vector3();

							bool success = float.TryParse(currentLine[1], out normal.X);
							success |= float.TryParse(currentLine[2], out normal.Y);
							success |= float.TryParse(currentLine[3], out normal.Z);

							normals.Add(normal);
						}
						else if (line.StartsWith("f "))
						{
							textureData = new float[vertices.Count * 2];
							normalData = new float[vertices.Count * 3];
							break;
						}
					}

					while (line != null)
					{
						if(!line.StartsWith("f "))
						{
							line = reader.ReadLine();
							continue;
						}
						// line = e.g. f 12/2/4 63/4/9 46/1/19
						// i.e. f v/t/n v/t/n v/t/n
						String[] currentLine = line.Split(' ');
						String[] vertex1 = currentLine[1].Split('/');
						String[] vertex2 = currentLine[2].Split('/');
						String[] vertex3 = currentLine[3].Split('/');
						// Process each vertex of a face => triangle
						ProcessVertex(vertex1, indices, textures, normals, textureData, normalData);
						ProcessVertex(vertex2, indices, textures, normals, textureData, normalData);
						ProcessVertex(vertex3, indices, textures, normals, textureData, normalData);
						line = reader.ReadLine();
					}

					vertexData = new float[vertices.Count * 3];
					indexData = new uint[indices.Count];

					int vertexPointer = 0;

					foreach (Vector3 vertex in vertices)
					{
						vertexData[vertexPointer++] = vertex.X;
						vertexData[vertexPointer++] = vertex.Y;
						vertexData[vertexPointer++] = vertex.Z;
					}

					for (int i = 0; i < indexData.Length; i++)
					{
						indexData[i] = indices.ElementAt(i);
					}

					return new Tuple<float[], float[], uint[]>(vertexData, textureData, indexData);
				}
			}
			catch (FileNotFoundException e)
			{
				Console.Error.WriteLine("Could not find file!");
				Console.WriteLine(e.StackTrace);
			}

			return null;
		}
		private static void ProcessVertex(String[] vertexData, List<uint> indices,
			List<Vector2> textures, List<Vector3> normals, float[] textureArray,
			float[] normalArray)
		{
			uint currentVertexPointer = uint.Parse(vertexData[0]) - 1; // obj file starts at 1, ogl at 0
			indices.Add(currentVertexPointer);

			Vector2 currentTex = textures.ElementAt(int.Parse(vertexData[1]) - 1);
			textureArray[currentVertexPointer * 2] = currentTex.X;
			textureArray[currentVertexPointer * 2 + 1] = 1 - currentTex.Y; // ogl starts from top-left, blender from bottom-left

			Vector3 currentNorm = normals.ElementAt(int.Parse(vertexData[2]) - 1);
			normalArray[currentVertexPointer * 3] = currentNorm.X;
			normalArray[currentVertexPointer * 3 + 1] = currentNorm.Y;
			normalArray[currentVertexPointer * 3 + 2] = currentNorm.Z;
		}
		public static Tuple<Vector3[], Vector2[], uint[]> Load(string filename)
		{
			try
			{
				string path = $"assets/models/{filename}";
				using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
				{
					string line;

					List<Vector3> vertices = new List<Vector3>();
					List<Vector2> textures = new List<Vector2>();
					List<Vector3> normals = new List<Vector3>();
					List<uint> indices = new List<uint>();

					Vector2[] textureData;
					Vector3[] normalData;
					uint[] indexData = null;

					while (true)
					{
						line = reader.ReadLine();
						String[] currentLine = line.Split(' ');

						if (line.StartsWith("v "))
						{
							Vector3 vertex = new Vector3();

							bool success = float.TryParse(currentLine[1], out vertex.X);
							success |= float.TryParse(currentLine[2], out vertex.Y);
							success |= float.TryParse(currentLine[3], out vertex.Z);

							vertices.Add(vertex);
						}
						else if (line.StartsWith("vt "))
						{
							Vector2 texture = new Vector2();

							bool success = float.TryParse(currentLine[1], out texture.X);
							success |= float.TryParse(currentLine[2], out texture.Y);

							textures.Add(texture);
						}
						else if (line.StartsWith("vn "))
						{
							Vector3 normal = new Vector3();

							bool success = float.TryParse(currentLine[1], out normal.X);
							success |= float.TryParse(currentLine[2], out normal.Y);
							success |= float.TryParse(currentLine[3], out normal.Z);

							normals.Add(normal);
						}
						else if (line.StartsWith("f "))
						{
							textureData = new Vector2[vertices.Count];
							normalData = new Vector3[vertices.Count];
							break;
						}
					}

					while (line != null)
					{
						if (!line.StartsWith("f "))
						{
							line = reader.ReadLine();
							continue;
						}
						// line = e.g. f 12/2/4 63/4/9 46/1/19
						// i.e. f v/t/n v/t/n v/t/n
						String[] currentLine = line.Split(' ');
						String[] vertex1 = currentLine[1].Split('/');
						String[] vertex2 = currentLine[2].Split('/');
						String[] vertex3 = currentLine[3].Split('/');
						// Process each vertex of a face => triangle
						ProcessVertex(vertex1, indices, textures, normals, textureData, normalData);
						ProcessVertex(vertex2, indices, textures, normals, textureData, normalData);
						ProcessVertex(vertex3, indices, textures, normals, textureData, normalData);
						line = reader.ReadLine();
					}
					indexData = new uint[indices.Count];

					for (int i = 0; i < indexData.Length; i++)
					{
						indexData[i] = indices.ElementAt(i);
					}

					return new Tuple<Vector3[], Vector2[], uint[]>(vertices.ToArray(), textureData, indexData);
				}
			}
			catch (FileNotFoundException e)
			{
				Console.Error.WriteLine("Could not find file!");
				Console.WriteLine(e.StackTrace);
			}

			return null;
		}
		// Called per-vertex data
		private static void ProcessVertex(String[] vertexData, List<uint> indices,
			List<Vector2> textures, List<Vector3> normals, Vector2[] textureArray, 
			Vector3[] normalArray)
		{
			uint currentVertexPointer = uint.Parse(vertexData[0]) - 1; // obj file starts at 1, ogl at 0
			indices.Add(currentVertexPointer);
			
			Vector2 currentTex = textures.ElementAt(int.Parse(vertexData[1]) - 1);
			textureArray[currentVertexPointer] = new Vector2(currentTex.X, 1 - currentTex.Y);

			Vector3 currentNorm = normals.ElementAt(int.Parse(vertexData[2]) - 1);
			normalArray[currentVertexPointer] = currentNorm;
		}

	}
}
