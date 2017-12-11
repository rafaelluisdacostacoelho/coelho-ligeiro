using Microsoft.Extensions.DependencyInjection;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Interfaces;
using CoelhoLigeiro.Infrastructure.Repositories;
using CoelhoLigeiro.Infrastructure.Contexts;
using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Services;

namespace CoelhoLigeiro.Application
{
    public class ApplicationDependencyInjection
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IStepService, StepService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IUserService, UserService>();

            // Infrastructure
            services.AddScoped<WeEntrepreneurContext>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IStepRepository, StepRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}