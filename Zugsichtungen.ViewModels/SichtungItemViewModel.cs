using AsyncAwaitBestPractices.MVVM;
using System.Windows.Input;
using Zugsichtungen.Abstractions.Services;
using Zugsichtungen.Domain.Models;
using Zugsichtungen.Foundation.ViewModel;

namespace Zugsichtungen.ViewModels
{
    public class SichtungItemViewModel : ViewModelBase
    {
        private readonly IDialogService dialogService;

        public SightingViewEntry Sichtung { get; }
        public SightingPicture? SightingPicture { get; }

        public int? Id => Sichtung.Id;
        public DateOnly? Date => Sichtung.Date;
        public string? Number => this.Sichtung.VehicleNumber;
        public string? Location => this.Sichtung.Location;
        public string? Context => this.Sichtung.Context;
        public string? Note => this.Sichtung.Note;
        public byte[]? Picture
        {
            get
            {
                if (this.SightingPicture == null)
                {
                    return null;
                }

                return this.SightingPicture.Image;
            }
        }

        public ICommand DeleteSightingCommand { get; }

        public SichtungItemViewModel(SightingViewEntry sighting,
            IDialogService dialogService, 
            SightingPicture? sightingPicture)
        {
            this.Sichtung = sighting;
            this.dialogService = dialogService;
            this.SightingPicture = sightingPicture;

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
