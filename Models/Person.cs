using System;
namespace Registration.Models
{
	public class Person
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int? Age { get; set; }
		public string? Occupation { get; set; }

		public Person()
		{
		}
	}
}

