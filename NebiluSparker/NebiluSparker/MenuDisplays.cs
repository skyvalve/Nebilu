using System;
using System.Threading;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
// Custom using.
using Sce.PlayStation.HighLevel.UI;


namespace NebiluSparker
{
	public class MenuDisplays
	{
		private Label announcement;		
		private GraphicsContext graphics;		
		
		public string Announcement{
			get {return announcement.Text;}
			set {announcement.Text = value;}
		}
		public MenuDisplays (GraphicsContext gs)
		{
			graphics = gs;
			// Ui systems.
			UISystem.Initialize(graphics);
			Scene scene = new Scene();
			announcement = new Label();
			announcement.X = 10; announcement.Y = graphics.Screen.Rectangle.Height
				/ 2 - announcement.TextHeight / 2;
			announcement.Width = graphics.Screen.Rectangle.Width;
			// Line will do horizontal center.
			announcement.HorizontalAlignment = HorizontalAlignment.Center;
			announcement.Text = "TBA :D";
			// Adding label to scene. 
			scene.RootWidget.AddChildLast(announcement);			
			
			// Set scene.
			UISystem.SetScene(scene, null);		
		} 	

			
		public void Render(){
			
			// Has using UISystem instead of HUD to rendering variable.
			UISystem.Render();					
		}
	}
}

