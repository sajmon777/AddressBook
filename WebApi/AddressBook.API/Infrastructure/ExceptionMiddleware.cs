using AddressBook.Business.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AddressBook.API.Infrastructure
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (RuleViolationException ex)
			{
				await HandleRuleViolationExceptionAsync(httpContext, ex);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private Task HandleRuleViolationExceptionAsync(HttpContext context, RuleViolationException exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

			return context.Response.WriteAsync(new ErrorDetails()
			{
				StatusCode = context.Response.StatusCode,
				ValidationErrors = exception.RuleViolations
			}.ToJson());
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			return context.Response.WriteAsync(new ErrorDetails()
			{
				StatusCode = context.Response.StatusCode,
				Message = exception.Message
			}.ToJson());
		}
	}
}
