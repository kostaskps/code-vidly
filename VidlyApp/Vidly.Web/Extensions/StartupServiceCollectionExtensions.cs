using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Vidly.Web.Contracts;
using Vidly.Web.DataAccess;
using Vidly.Web.DataAccess.Repositories;

namespace Vidly.Web.Extensions
{
    public static class StartupServiceCollectionExtensions
    {
        public static IServiceCollection RegisterLocalizationServices(this IServiceCollection serviceCollection)
        {
            // Add the localization services to the services container
            serviceCollection.AddLocalization(options => options.ResourcesPath = "Resources");

            serviceCollection.AddMvc()
                // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                // Add support for localizing strings in data annotations (e.g. validation messages) via the
                // IStringLocalizer abstractions.
                .AddDataAnnotationsLocalization();

            // Configure supported cultures and localization options
            serviceCollection.Configure<RequestLocalizationOptions>(ConfigureLocalization);

            return serviceCollection;
        }

        public static IServiceCollection RegisterRepositoryServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IProvideVidlyRepository<>), typeof(VidlyRepositoryBase<>));
            serviceCollection.AddTransient<IProvideGenres, GenresRepository>();
            serviceCollection.AddTransient<IProvideMembershipTypes, MembershipTypesRepository>();

            // Finally register UnitOfWork
            serviceCollection.AddTransient<IProvideUnitOfWork, UnitOfWork>();
            return serviceCollection;
        }

        private static void ConfigureLocalization(RequestLocalizationOptions options)
        {
            var supportedCultures = ProvideSupportedCultures();

            // State what the default culture for your application is. This will be used if no specific culture
            // can be determined for a given request.
            options.DefaultRequestCulture = new RequestCulture(supportedCultures[1]);

            // You must explicitly state which cultures your application supports.
            // These are the cultures the app supports for formatting numbers, dates, etc.
            options.SupportedCultures = supportedCultures;

            // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
            options.SupportedUICultures = supportedCultures;

            // You can change which providers are configured to determine the culture for requests, or even add a custom
            // provider with your own logic. The providers will be asked in order to provide a culture for each request,
            // and the first to provide a non-null result that is in the configured supported cultures list will be used.
            // By default, the following built-in providers are configured:
            // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
            // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
            // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
            //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
            //{
            //  // My custom request culture logic
            //  return new ProviderCultureResult("en");
            //}));
        }

        private static CultureInfo[] ProvideSupportedCultures()
        {
            return new[]
                {
                    new CultureInfo("en-US"){
                        DateTimeFormat =
                        {
                            ShortDatePattern = "MM/dd/yyyy"
                        }
                    },
                    new CultureInfo("el-GR"){
                        DateTimeFormat =
                        {
                            ShortDatePattern = "dd/MM/yyyy"
                        }
                    }
                };
        }
    }
}
