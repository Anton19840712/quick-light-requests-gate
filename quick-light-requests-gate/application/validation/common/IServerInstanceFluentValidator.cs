using domain.models.dynamicgatesettings.internalusage;
using domain.models.response;

namespace application.validation.common
{
	public interface IServerInstanceFluentValidator
	{
		ResponseIntegration Validate(ServerInstanceModel instanceModel);
	}
}
