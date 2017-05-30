﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
	public class Post : BaseEntity
	{
		
		public string AuthorName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		[Required(ErrorMessage = "Enter the Issued date.")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
		public byte[] Image { get; set; }
	}
}