using AddressBook.Business.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.API.Infrastructure
{	public class ErrorDetails
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }

		public RuleViolation[] ValidationErrors { get; set; }

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}

