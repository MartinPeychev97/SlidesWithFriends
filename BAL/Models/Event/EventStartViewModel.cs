using BAL.Models.Presentation;

namespace BAL.Models.Event
{
    public class EventStartViewModel
    {
        public string Username { get; set; }
        public string Image { get; set; }
        public QrCodeViewModel QRCodeViewModel { get; set; }
        public bool IsPresenter { get; set; }
        public PresentationEventViewModel Presentation { get; set; }
    }
}
