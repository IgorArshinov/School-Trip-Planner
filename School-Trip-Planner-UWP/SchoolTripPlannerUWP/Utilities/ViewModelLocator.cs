using Akavache;
using AutoMapper;
using GalaSoft.MvvmLight.Ioc;
using SchoolTripPlannerUWP.Contracts;
using SchoolTripPlannerUWP.Services;
using SchoolTripPlannerUWP.ViewModels;
using SchoolTripPlannerUWP.Views;
using Windows.UI.Xaml;
using SchoolTripPlannerUWP.Core.Contracts.Repository;
using SchoolTripPlannerUWP.Core.Contracts.Services.Data;
using SchoolTripPlannerUWP.Core.DTOs.AutoMapperProfiles;
using SchoolTripPlannerUWP.Core.Repository;
using SchoolTripPlannerUWP.Core.Services.Data;

namespace SchoolTripPlannerUWP.Utilities
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static readonly SimpleIoc SimpleIoc = SimpleIoc.Default;

        public ViewModelLocator()
        {
            //            var config = new MapperConfiguration(cfg => { cfg.AddProfile<GeneralProfile>(); });

            //            SimpleIoc.Register(() => new MapperConfiguration(cfg => { cfg.AddProfile<GeneralProfile>(); }));

            SimpleIoc.Register<INavigationServiceEx, NavigationServiceEx>();
            SimpleIoc.Register<IGenericRepository, GenericRepository>();
            SimpleIoc.Register<ISchoolTripDataService, SchoolTripDataService>();
            SimpleIoc.Register<IClassDataService, ClassDataService>();
            SimpleIoc.Register<IToddlerDataService, ToddlerDataService>();
            SimpleIoc.Register<ITeacherDataService, TeacherDataService>();

            SimpleIoc.Register(() => BlobCache.LocalMachine);
            SimpleIoc.Register(() => new NavigationServiceEx());

            SimpleIoc.Register<IMapper>(() => new Mapper(CreateMapperConfiguration()));
            SimpleIoc.Register<ShellViewModel>();

            Register<SchoolTripsMasterViewModel, SchoolTripsMasterPage>();
            Register<RegisterToddlerViewModel, RegisterToddlerPage>();
            Register<PlanSchoolTripViewModel, PlanSchoolTripPage>();
            Register<SchoolTripsDetailsViewModel, SchoolTripsDetailsControl>();
            Register<EditToddlerViewModel, EditToddlerPage>();
        }

        public static ViewModelLocator Current => Application.Current.Resources["Locator"] as ViewModelLocator;
        public SchoolTripsMasterViewModel SchoolTripsMasterViewModel => SimpleIoc.GetInstance<SchoolTripsMasterViewModel>();
        public ShellViewModel ShellViewModel => SimpleIoc.GetInstance<ShellViewModel>();
        public RegisterToddlerViewModel RegisterToddlerViewModel => SimpleIoc.GetInstance<RegisterToddlerViewModel>();
        public PlanSchoolTripViewModel PlanSchoolTripViewModel => SimpleIoc.GetInstance<PlanSchoolTripViewModel>();
        public SchoolTripsDetailsViewModel SchoolTripsDetailsViewModel => SimpleIoc.GetInstance<SchoolTripsDetailsViewModel>();
        public EditToddlerViewModel EditToddlerViewModel => SimpleIoc.GetInstance<EditToddlerViewModel>();
        public INavigationServiceEx NavigationService => SimpleIoc.GetInstance<INavigationServiceEx>();

        private void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }

        private IConfigurationProvider CreateMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<GeneralProfile>(); });

            return config;
        }
    }
}
