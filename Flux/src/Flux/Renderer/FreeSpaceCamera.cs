﻿using System;
using OpenTK;

namespace Flux.src.Flux.Renderer
{	
	public class FreeSpaceCamera
	{
		private Vector3 front = -Vector3.UnitZ;
		private Vector3 up = Vector3.UnitY;
		private Vector3 right = Vector3.UnitX;

		private float pitch;
		private float yaw = -MathHelper.PiOver2;
		private float fov = MathHelper.DegreesToRadians(45f);

		public Matrix4 ProjectionMatrix { get => GetProjectionMatrix(); }
		public Matrix4 ViewMatrix { get => GetViewMatrix(); }
		public Matrix4 ViewProjectionMatrix { get => GetViewProjectMatrix(); }

		public FreeSpaceCamera(Vector3 position, float aspectRatio)
		{
			Position = position;
			AspectRatio = aspectRatio;
		}
		public Vector3 Position { get; set; }
		public float AspectRatio { private get; set; }

		public Vector3 Front => front;
		public Vector3 Up => up;
		public Vector3 Right => right;

		public float Pitch
		{
			get => MathHelper.RadiansToDegrees(pitch);
			set
			{
				var angle = MathHelper.Clamp(value, -89.89f, 89.89f);
				pitch = MathHelper.DegreesToRadians(angle);
				UpdateVectors();
			}
		}

		public float Yaw
		{
			get => MathHelper.RadiansToDegrees(yaw);
			set
			{
				yaw = MathHelper.DegreesToRadians(value);
				UpdateVectors();
			}
		}
		public float Fov
		{
			get => MathHelper.RadiansToDegrees(fov);
			set
			{
				var angle = MathHelper.Clamp(value, 1f, 45f);
				fov = MathHelper.DegreesToRadians(angle);
			}
		}
		private Matrix4 GetViewMatrix()
		{
			return Matrix4.Identity * Matrix4.LookAt(Position, Position + front, up);
		}
		private Matrix4 GetProjectionMatrix()
		{
			return Matrix4.Identity * Matrix4.CreatePerspectiveFieldOfView(fov, AspectRatio, 0.01f, 1000.0f);
		}
		public Matrix4 GetViewProjectMatrix()
		{
			return GetViewMatrix() * GetProjectionMatrix();
		}
		public void UpdateVectors()
		{
			// First the front matrix is calculated using some basic trigonometry
			front.X = (float)Math.Cos(pitch) * (float)Math.Cos(yaw);
			front.Y = (float)Math.Sin(pitch);
			front.Z = (float)Math.Cos(pitch) * (float)Math.Sin(yaw);

			// We need to make sure the vectors are all normalized, as otherwise we would get some funky results
			front = Vector3.Normalize(front);

			// Calculate both the right and the up vector using cross product
			// Note that we are calculating the right from the global up, this behaviour might
			// not be what you need for all cameras so keep this in mind if you do not want a FPS camera
			right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
			up = Vector3.Normalize(Vector3.Cross(right, front));
		}
	}
}
