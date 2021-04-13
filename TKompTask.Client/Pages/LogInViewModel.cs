using FluentValidation;
using Stylet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TKompTask.Client.Pages
{
    public class LogInViewModel : Screen
    {
        private readonly IEventAggregator _events;

        public LogInViewModel(IModelValidator<LogInViewModel> validator, IEventAggregator events) :base(validator)
        {
            this.Validate();
            base.AutoValidate = true;
            _events = events;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionStatus { get; set; }
        public string ConnectionStatusColor { get; set; }
        public bool IsBusy { get; set; }
        public ApiClient Api { get; set; }

        public async Task LogInAsync()
        {
            IsBusy = true;
            ConnectionStatusColor = "#000000";
            ConnectionStatus = "Trwa łączenie z serwerem...";
            Api = new ApiClient(Username, Password);
            if (await Api.TestConnection())
            {
                ConnectionStatus = "Zalogowano do bazy danych!";
                ConnectionStatusColor = "#00FF00";
                _events.Publish(new Events.ActivateFetchDataButtonEvent(Api));
            }
            else
            {
                ConnectionStatus = "Logowanie nie powiodło się!";
                ConnectionStatusColor = "#FF0000";
            }
            IsBusy = false;
        }

        public bool CanLogInAsync => !this.HasErrors && !IsBusy;

        protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
        {
            base.OnValidationStateChanged(changedProperties);
            NotifyOfPropertyChange(() => this.CanLogInAsync);
        }
    }

    public class LogInViewModelValidator : AbstractValidator<LogInViewModel>
    {
        public LogInViewModelValidator()
        {
            RuleFor(o => o.Username).NotEmpty().MinimumLength(3);
            RuleFor(o => o.Password).NotEmpty().MinimumLength(3);
        }
    }

}