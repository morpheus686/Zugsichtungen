namespace Zugsichtungen.Foundation.ViewModel
{
    public abstract class DialogViewModelBase : LoadableViewModel
    {  
        private string? title;

        public abstract bool HasErrors { get; }
        public string? Title
        {
            get { return title; }
            set 
            { 
                if (title == value) return;
                title = value; 
            }
        }

    }
}
