using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AddressBook.Business.Infrastructure
{
	public static class RuleViolationExtension
	{
		public static IEnumerable<RuleViolation> EnrichIdentifier(this IEnumerable<RuleViolation> src, Expression<Func<object>> property)
		{
			if (src == null)
				return null;

			foreach (var s in src)
				s.EnrichIdentifier(property);

			return src;
		}

		public static IEnumerable<RuleViolation> EnrichIdentifier(this IEnumerable<RuleViolation> src, string identifierPrefix)
		{
			if (src == null)
				return null;

			foreach (var s in src)
				s.EnrichIdentifier(identifierPrefix);

			return src;
		}
	}

	public class RuleViolation
	{
		public string Identifier { get; private set; }
		public string Message { get; private set; }
		public bool IsWarning { get; private set; }

		public RuleViolation() { }

		public RuleViolation(string message, params object[] args)
			: this(String.Empty, message, args) { }

		public RuleViolation(Expression<Func<object>> property, string message, params object[] args)
			: this(false, GetPath(property), message, args) { }

		public RuleViolation(bool isWarning, Expression<Func<object>> property, string message, params object[] args)
			: this(isWarning, GetPath(property), message, args) { }

		public RuleViolation(string identifier, string message, params object[] args)
			: this(false, identifier, message, args) { }

		public RuleViolation(bool isWarning, string identifier, string message, params object[] args)
		{
			Message = message;
			Identifier = string.IsNullOrEmpty(identifier) ? "_" : identifier;
			IsWarning = isWarning;
		}

		public RuleViolation(Exception ex)
		{
			Identifier = "_";
			Message = ex.Message;
		}

		public void EnrichIdentifier(Expression<Func<object>> property)
		{
			EnrichIdentifier(GetPath(property));
		}

		public void EnrichIdentifier(string identifierPrefix)
		{
			Identifier = string.Format("{0}_{1}", identifierPrefix, Identifier);
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}", Identifier, Message);
		}

		static string GetPath(Expression<Func<object>> expr)
		{
			var stack = new Stack<string>();

			MemberExpression me;
			switch (expr.Body.NodeType)
			{
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
					var ue = expr.Body as UnaryExpression;
					me = ((ue != null) ? ue.Operand : null) as MemberExpression;
					break;
				default:
					me = expr.Body as MemberExpression;
					break;
			}

			while (me != null)
			{
				stack.Push(me.Member.Name);
				me = me.Expression as MemberExpression;
			}

			int skip = 0;
			if (stack.Count > 1)
				skip = 1;

			return string.Join("_", stack.Skip(skip).ToArray());
		}
	}
}
