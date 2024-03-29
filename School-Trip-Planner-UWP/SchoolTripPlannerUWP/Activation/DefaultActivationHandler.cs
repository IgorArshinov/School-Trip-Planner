﻿using System;
using System.Threading.Tasks;
using SchoolTripPlannerUWP.Contracts;
using SchoolTripPlannerUWP.Utilities;
using Windows.ApplicationModel.Activation;

namespace SchoolTripPlannerUWP.Activation
{
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        private readonly string _navElement;
        private INavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public DefaultActivationHandler(Type navElement)
        {
            _navElement = navElement.FullName;
        }

        protected override async Task HandleInternalAsync(IActivatedEventArgs args)
        {
            // When the navigation stack isn't restored, navigate to the first page and configure
            // the new page by passing required information in the navigation parameter
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }

            NavigationService.NavigateAsync(_navElement, arguments);
            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(IActivatedEventArgs args)
        {
            // None of the ActivationHandlers has handled the app activation
            return NavigationService.Frame.Content == null && _navElement != null;
        }
    }
}
