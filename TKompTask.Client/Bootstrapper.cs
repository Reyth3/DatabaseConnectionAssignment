using TKompTask.Client.Pages;
using Stylet;
using StyletIoC;
using Stylet.Samples.ModelValidation;
using FluentValidation;

namespace TKompTask.Client
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.Bind(typeof(IModelValidator<>)).To(typeof(FluentModelValidator<>));
            builder.Bind(typeof(IValidator<>)).ToAllImplementations();
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
