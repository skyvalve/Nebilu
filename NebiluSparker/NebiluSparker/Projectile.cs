using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace NebiluSparker
{
	public class Projectile
	{
		private GraphicsContext graphics;
		private Sprite missile;
		private float Speed;
		
		public bool isLive;
		
		public Rectangle Extents{
			/* This create a collision between the bullet and Aliens. 
			 * */
			get {return new Rectangle(X, Y, missile.Width, missile.Height);} 
		}
		
		public float X{
			// get syntax use return to variable.
			get{return missile.Position.X;}
		}
		
		public float Y{
			// get syntax use return to variable.
			get{return missile.Position.Y;}
		}
		
		public Projectile (GraphicsContext gs, int x, int y)
		{
			graphics = gs;
			
			Texture2D t = new Texture2D("Application/Resources/missile.png", false);
			missile = new Sprite(gs, t);
			// Set position center.
			missile.Position.X = x;
			missile.Position.Y = y;
			Speed = 30.7f;
			
		    //missile.SetColor(1,0,0,2);
			missile.Scale.X = 1.6f;
			missile.Scale.Y = .7f;
			
			isLive = true;
		}
		
		public void Update(){
			
			missile.Position.X += Speed;
		}
		
		public void Render(){
			
			// Hero rendering itself.
			missile.Render();
		}
	}
}

