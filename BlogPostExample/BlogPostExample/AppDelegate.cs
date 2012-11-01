using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.IO;
using System.Threading;

namespace Macopus.Examples.DataStorage
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			// If you have defined a view, add it here:
			// window.AddSubview (navigationController.View);
			
			// make the window visible
			window.MakeKeyAndVisible ();

			var threadstart = new ThreadStart (delegate {

				// Simple
				var simpleTopic = new SimpleModel.Topic()
				{
					Subject = "My first topic",
					Posts = new List<SimpleModel.Post>()
					{
						new SimpleModel.Post ()
						{
							Body = "Hello world!"
						}
					}
				};
				FileHelper.WriteToFile (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "filenameSimple.xml"), simpleTopic);

				// More Complex
				var realisticUser = new RealisticModel.User()
				{
					Username = "Macropus"
				};
				var realisticTopic = new RealisticModel.Topic()
				{
					Subject = "My first topic",
					Owner = realisticUser,
					Posts = new List<RealisticModel.Post>()
					{
						new RealisticModel.Post ()
						{
							Body = "Hello world!",
							Owner = realisticUser
						}
					}
				};
				FileHelper.WriteToFile (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "filenameRealistic.xml"), realisticTopic);

				// More Complexer
				var moreRealisticUser = new EvenMoreRealisticModel.User()
				{
					Username = "Macropus",
					Posts = new List<EvenMoreRealisticModel.Post>()
				};
				var moreRealisticTopic = new EvenMoreRealisticModel.Topic()
				{
					Subject = "My first topic",
					Owner = moreRealisticUser,
					Posts = new List<EvenMoreRealisticModel.Post>()
				};
				var moreRealisticPost = new EvenMoreRealisticModel.Post ()
				{
					Body = "Hello world!",
					Owner = moreRealisticUser
				};
				moreRealisticUser.Posts.Add (moreRealisticPost);
				try
				{
					FileHelper.WriteToFile (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "filenameMostRealistic.xml"), moreRealisticTopic);
				}
				catch (Exception ex)
				{
					Console.Write (ex);
				}


				// Benchmarking
				// More Complex
				var benchmarkForum = new RealisticModel.Forum()
				{
					Topics = new List<RealisticModel.Topic>()
				};

				var benchmarkUser = new RealisticModel.User()
				{
					Username = "BenchmarkUser"
				};

				for (int i = 0; i <= 1000; i++)
				{
					var benchmarkTopic = new RealisticModel.Topic()
					{
						Subject = String.Format ("Topic {0}", i),
						Owner = benchmarkUser,
						Posts = new List<RealisticModel.Post>()
					};
					benchmarkForum.Topics.Add (benchmarkTopic);

					for (int o = 0; o <= 100; o++)
					{
						var benchmarkPost =	new RealisticModel.Post ()
						{
							Body = "Hello world!",
							Owner = benchmarkUser
						};
						benchmarkTopic.Posts.Add (benchmarkPost);
						//benchmarkUser.Posts.Add (benchmarkPost);
					}
				}
				var stopwatch = new System.Diagnostics.Stopwatch();
				stopwatch.Start();
				FileHelper.WriteToFile (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "filenameBenchmark.xml"), benchmarkForum);
				stopwatch.Stop();

				var writeTime = stopwatch.ElapsedMilliseconds;

				stopwatch.Reset();
				stopwatch.Start();
				var forum = FileHelper.ReadFromFile<RealisticModel.Forum> (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "filenameBenchmark.xml"));
				stopwatch.Stop();

				var readTime = stopwatch.ElapsedMilliseconds;
			});
			var thread = new Thread (threadstart);
			thread.Start ();

			return true;
		}
	}
}

