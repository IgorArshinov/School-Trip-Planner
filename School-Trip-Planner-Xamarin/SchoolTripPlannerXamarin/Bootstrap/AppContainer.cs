using Autofac;
using AutoMapper;
using SchoolTripPlannerXamarin.Contracts.Repository;
using SchoolTripPlannerXamarin.Contracts.Services.Data;
using SchoolTripPlannerXamarin.Contracts.Services.General;
using SchoolTripPlannerXamarin.DTOs.AutoMapperProfiles;
using SchoolTripPlannerXamarin.Services.Data;
using SchoolTripPlannerXamarin.Services.General;
using SchoolTripPlannerXamarin.Services.Repository;
using SchoolTripPlannerXamarin.ViewModels;
using System;
using BlobCache = Akavache.BlobCache;

namespace SchoolTripPlannerXamarin.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<NewSchoolTripScanViewModel>();
            builder.RegisterType<SchoolTripDetailsViewModel>();
            builder.RegisterType<ScanToddlerDetailsViewModel>();
            builder.RegisterType<SchoolTripsViewModel>();
            builder.RegisterType<RegistrationViewModel>();
            builder.RegisterType<TeacherViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<AccountTabbedViewModel>();
            builder.RegisterType<ShellViewModel>();

            //Services
            builder.RegisterType<ScanToddlerDataService>().As<IScanToddlerService>();
            builder.RegisterType<ScanDataService>().As<IScanDataService>();
            builder.RegisterType<ConnectionService>().As<IConnectionService>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<TeacherDataService>().As<ITeacherDataService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<SchoolTripDataService>().As<ISchoolTripDataService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();
            builder.RegisterType<SyncService>().As<ISyncService>().SingleInstance();
            builder.RegisterInstance<ITeacherSQLDataService>(new TeacherSQLDataService()).SingleInstance();
            builder.RegisterInstance(BlobCache.LocalMachine).SingleInstance();
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();
            builder.RegisterInstance<IMapper>(new Mapper(CreateMapperConfiguration()));

            _container = builder.Build();
        }

        private static IConfigurationProvider CreateMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<GeneralProfile>(); });

            return config;
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}