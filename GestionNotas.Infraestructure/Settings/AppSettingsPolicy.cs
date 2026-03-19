using GestionNotas.Api.Application.Common.Settings;
using Microsoft.Extensions.Options;

namespace GestionNotas.Api.Infrastructure.Settings
{
    public class AppSettingsPolicy : IAppSettingsPolicy
    {
        private readonly AppSettings _settings;

        public AppSettingsPolicy(IOptions<AppSettings> options)
        {
            _settings = options.Value;
        }

        public string ConnectionBd => _settings.ConnectionBd;
    }
}
