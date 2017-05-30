using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
	public abstract class BaseEntity
	{
		private String id;
		[Key]
		[Required]
		public String Id
		{
			get
			{
				return id ?? (id = Guid.NewGuid().ToString());
			}
			set
			{
				id = value;
			}
		}
	}
}