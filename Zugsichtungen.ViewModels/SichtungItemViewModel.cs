using AsyncAwaitBestPractices.MVVM;
using System.Windows.Input;
using Zugsichtungen.Abstractions.DTO;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels
{
    public class SichtungItemViewModel : ViewModelBase
    {
        private readonly IDialogService dialogService;

        public SightingViewEntryDto Sichtung { get; }

        public int Id => Sichtung.Id;
        public DateOnly? Date => Sichtung.Date;
        public string? Number => this.Sichtung.VehicleNumber;
        public string? Location => this.Sichtung.Location;
        public string? Context => this.Sichtung.Context;
        public string? Note => this.Sichtung.Note;
        public byte[]? Thumbnail => this.Sichtung.Thumbnail;

        public ICommand DeleteSightingCommand { get; }

        public SichtungItemViewModel(SightingViewEntryDto sighting,
            IDialogService dialogService)
        {
            this.Sichtung = sighting;
            this.dialogService = dialogService;

            this.DeleteSightingCommand = new AsyncCommand(ExecuteDeleteSightingCommand);
        }

        private async Task ExecuteDeleteSightingCommand()
        {
            await this.dialogService.ShowIndeterminateDialogAsync(async (updateMessage, parameter) =>
            {
                await Task.Delay(5000);
            });
        }
    }
}
