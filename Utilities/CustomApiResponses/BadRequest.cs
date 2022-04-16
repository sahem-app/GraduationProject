﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GraduationProject.Utilities.CustomApiResponses
{
	public class BadRequest : IActionResult, IApiResponse
	{
		public byte Status { get; } = 0;
		public string Message { get; } = "Error";
		public IDictionary<string, IEnumerable<string>> Errors { get; private set; }

		public BadRequest(string errorMessage)
		{
			Message = errorMessage;
		}

		public BadRequest(ModelStateDictionary modelState)
		{
			Errors = new Dictionary<string, IEnumerable<string>>();
			foreach (var model in modelState.Where(m => m.Value.Errors.Count > 0))
				Errors.Add(model.Key, model.Value.Errors.Select(e => e.ErrorMessage));
		}

		public BadRequest(IDictionary<string, IEnumerable<string>> errors)
		{
			Errors = errors;
		}

		public BadRequest(KeyValuePair<string, IEnumerable<string>> error)
		{
			Errors = new Dictionary<string, IEnumerable<string>>();
			Errors.Add(error);
		}

		public BadRequest(string propertyName, string errorMessage)
		{
			Errors = new Dictionary<string, IEnumerable<string>>();
			Errors.Add(new KeyValuePair<string, IEnumerable<string>>(propertyName, new[] { errorMessage }));
		}

		public async Task ExecuteResultAsync(ActionContext context)
		{
			var objectResult = new ObjectResult(this)
			{
				StatusCode = StatusCodes.Status400BadRequest
			};

			await objectResult.ExecuteResultAsync(context);
		}
	}
}
