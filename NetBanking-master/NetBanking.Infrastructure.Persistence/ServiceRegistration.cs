using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Infrastructure.Persistence.Context;
using NetBanking.Infrastructure.Persistence.Repositories;

namespace NetBanking.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<NetBankingContext>(options => options.UseInMemoryDatabase("NetBankingDb"));
            }
            else
            {
                services.AddDbContext<NetBankingContext > (options =>
                options.UseSqlServer(configuration.GetConnectionString("NetBankingConnectionString"),
                m => m.MigrationsAssembly(typeof(NetBankingContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories

            services.AddTransient(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddTransient<IBeneficiaryRepository, BeneficiaryRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();
            services.AddTransient<ISavingAccountRepository, SavingAccountRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();

            #endregion
        }
    }
}
