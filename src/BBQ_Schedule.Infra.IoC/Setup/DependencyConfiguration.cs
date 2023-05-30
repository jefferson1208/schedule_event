using BBQ_Schedule.Application.Schedule;
using BBQ_Schedule.Domain.Commands;
using BBQ_Schedule.Domain.Handlers;
using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Domain.Interfaces.Repositories;
using BBQ_Schedule.Domain.Mediator;
using BBQ_Schedule.Domain.Notifications;
using BBQ_Schedule.Infra.Data.Repositories;
using BBQ_Schedule.Infra.Identity.Authentication;
using BBQ_Schedule.Infra.IoC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BBQ_Schedule.Infra.IoC.Setup
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            #region APPLICATION
            services.AddScoped<IScheduleApplicationService, ScheduleApplicationService>();
            #endregion

            #region HANDLERS
            services.AddScoped<IMediatorHandler, Handler>();
            services.AddScoped<IRequestHandler<ScheduleCommand, Command>, ScheduleCommandHandler>();
            services.AddScoped<IRequestHandler<GuestCommand, Command>, ScheduleCommandHandler>();
            #endregion

            #region REPOSITORIES
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            #endregion

            return services;
        }

    }
}
