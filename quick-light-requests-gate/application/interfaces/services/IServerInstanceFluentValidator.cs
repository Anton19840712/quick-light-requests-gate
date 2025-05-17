using domain.models.dynamicgatesettings.internalusage;
using domain.models.response;

namespace application.interfaces.services
{
	public interface IServerInstanceFluentValidator
	{
		ResponseIntegration Validate(ServerInstanceModel instanceModel);
	}
}
