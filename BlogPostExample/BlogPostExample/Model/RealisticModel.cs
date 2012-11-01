using System;
using System.Collections.Generic;

namespace Macopus.Examples.DataStorage.RealisticModel
{
	public class User
	{
		public string Username {
			get;
			set;
		}
		
		public List<Post> Posts {
			get;
			set;
		}
	}
	
	public class Topic
	{
		public string Subject {
			get;
			set;
		}

		public User Owner {
			get;
			set;
		}
		
		public List<Post> Posts {
			get;
			set;
		}
	}
	
	public class Post
	{
		public User Owner {
			get;
			set;
		}
		
		public string Body {
			get;
			set;
		}
	}

	public class Forum
	{
		public List<Topic> Topics {
			get;
			set;
		}
	}
}

