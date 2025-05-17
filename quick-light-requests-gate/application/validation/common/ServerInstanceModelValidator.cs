﻿using domain.models.dynamicgatesettings.internalusage;
using FluentValidation;
using System.Net;

namespace application.validation.common
{
	public class ServerInstanceModelValidator : AbstractValidator<ServerInstanceModel>
	{
		public ServerInstanceModelValidator()
		{
			RuleFor(x => x.Host)
				.NotEmpty().WithMessage("Host cannot be null or empty.")
				.Must(IsValidIPAddress).WithMessage("Invalid host address.");

			RuleFor(x => x.Port)
				.GreaterThan(0).WithMessage("Port must be greater than 0.");
		}

		private bool IsValidIPAddress(string host) => IPAddress.TryParse(host, out _);
	}
}
