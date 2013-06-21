using System;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

// Custom using.
using Sce.PlayStation.HighLevel.UI;


namespace NebiluSparker
{
	public class HUD
	{
		private Label position;
		private Label FPS;
		private Label AlienEnemy;
		private Label missile;
		private GraphicsContext graphics;
		// Time stamp variable.
		private long elapsedTime;
		private int frameCount;
		
		public HUD (GraphicsContext gs)
		{
			graphics = gs;
			// Ui systems.
			UISystem.Initialize(graphics);
			Scene scene = new Scene();
			position = new Label();
			position.X = 10; position.Y = 10;
			position.Width = 660;
			position.Text = "Hello from Light :D";
			// Adding label to scene. 
			scene.RootWidget.AddChildLast(position);
			
			// Add another counter.
			FPS = new Label();
			FPS.X = graphics.Screen.Rectangle.Width - 310;
			FPS.Y = 10;
			FPS.HorizontalAlignment = HorizontalAlignment.Right;
			FPS.Width = 300;
			FPS.Text = "FPS = XXX";
			scene.RootWidget.AddChildLast(FPS);
			// Aliens counter during game.
			AlienEnemy = new Label();
			AlienEnemy.X = 10;
			AlienEnemy.Y = 500;
			AlienEnemy.Width = 300;
			AlienEnemy.Text = "Enemy count = 0";
			scene.RootWidget.AddChildLast(AlienEnemy);
			// Realtime Missile counter.
			missile = new Label();
			missile.X = 10;
			missile.Y = 521;
			missile.Width = 300;
			missile.Text = "Missile count = 0";
			scene.RootWidget.AddChildLast(missile);
			// Set scene.
			UISystem.SetScene(scene, null);
			
			elapsedTime = 0;
			frameCount = 0;
		} 	
		
		// Objective for this function is display position
		// with .Text //
		public void UpdatePosition(string Better){
			position.Text = Better;
		}
		
		public void UpdateFPS(long TimeDelta){
			
			elapsedTime += TimeDelta;
			if(elapsedTime >= 1000){
				FPS.Text = "FPS = " +frameCount;
				// frameCount reset to 0.
				frameCount = 0;
				// Count down.
				elapsedTime -= 1000;
				
			}
		}
		
		public void UpdateAlienCount(int count){
			// Manual counter for counting existing Aliens.
			AlienEnemy.Text = "Enemy count = " +count;
		}
				
		public void UpdateMissileCount(int count){
			// Manual counter for counting existing missiles.
			missile.Text = "Missile count = " +count;
		}
		
		public void Render(){
			
			// Has using UISystem instead of HUD to rendering variable.
			UISystem.Render();
			// Time stamp frame count.
			frameCount++;
		
		}
	}
}

