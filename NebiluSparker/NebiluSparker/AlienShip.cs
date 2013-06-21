using System;
using System.Threading;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace NebiluSparker
{
	public class AlienShip
	{
		private GraphicsContext graphics;
		private Sprite Alien;
		private int Speed;
		
		public bool isLive;
		
		public Rectangle Extents{
			/* This create a collision between the bullet and Aliens. 
			 * */
			get {return new Rectangle(X, Y, Alien.Width, Alien.Height);} 
		}
		
		public float X{
			// get syntax use return to variable.
			get{return Alien.Position.X;}
		}
		
		public float Y{
			// get syntax use return to variable.
			get{return Alien.Position.Y;}
		}
		
		public AlienShip (GraphicsContext gs, int x, int y, int sp)
		{
			graphics = gs;
			
			Texture2D t = new Texture2D("Application/Resources/AlienShip.png", false);
			Alien = new Sprite(gs, t);
			// Set position center.
			Alien.Position.X = x;
			Alien.Position.Y = y;
			Speed = sp;
			
			//Hero.SetColor(1,0,0,2);
			Alien.Scale.X = 0.4f;
			Alien.Scale.Y = .4f;
			
			isLive = true;
		}
		
		public void Update(){
			
			Alien.Position.X -= Speed;
		}
		
		public void Render(){
			
			// Hero rendering itself.
			Alien.Render();
		}
	}
}

