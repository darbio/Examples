using System;
using System.Collections.Generic;

namespace Macopus.Examples.DataStorage.SimpleModel
{
	public class Topic
	{
		public string Subject {
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
		public string Body {
			get;
			set;
		}
	}
}

