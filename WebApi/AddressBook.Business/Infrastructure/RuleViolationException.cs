using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook.Business.Infrastructure
{
	public class RuleViolationException : Exception
	{
		public RuleViolation[] RuleViolations { get; private set; }

		public RuleViolationException(IEnumerable<RuleViolation> ruleViolations) :
			this(ruleViolations.ToArray())
		{ }
		public RuleViolationException(params RuleViolation[] ruleViolations) :
			base(ruleViolations.Length > 1 ? "RuleViolationException" : ruleViolations[0].Message)
		{
			RuleViolations = ruleViolations;
		}
	}
}
