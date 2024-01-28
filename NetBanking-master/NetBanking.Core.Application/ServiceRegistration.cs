

using Microsoft.Extensions.DependencyInjection;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.Services;
using System.Reflection;

namespace NetBanking.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services
           
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient((typeof(IGenericService<,,>)), (typeof(GenericService<,,>)));

            services.AddTransient<IBeneficiaryService,BeneficiaryService>();
            services.AddTransient<ISavingAccountService, SavingAccountService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<ICashOutService, CahsOutService>();
            
            #endregion
        }
    }
}
