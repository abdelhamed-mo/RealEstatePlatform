using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using System.Reflection;


namespace RealEstateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
			//services.AddMediatR(typeof(ServiceRegistration).Assembly);
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			
			#region Services
			services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IImprovementsService, ImprovementsService>();
            services.AddTransient<ITypeOfPropertiesService, TypeOfPropertiesService>();
            services.AddTransient<ITypeOfSalesService, TypeOfSalesService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPropertiesService, PropertiesService>();
            services.AddTransient<IPropertiesImprovementsService, PropertiesImprovementsService>();
            #endregion
        }
    }
}
