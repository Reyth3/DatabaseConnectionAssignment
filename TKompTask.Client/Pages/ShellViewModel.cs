using Stylet;

namespace TKompTask.Client.Pages
{
    public class ShellViewModel : Screen
    {
        public string Title => $"Struktura danych" ;
        public LogInViewModel LogIn { get; set; }
        public DataViewModel Data { get; set; }

        public ShellViewModel(LogInViewModel login, DataViewModel data)
        {
            LogIn = login;
            Data = data;
        }
    }
}
