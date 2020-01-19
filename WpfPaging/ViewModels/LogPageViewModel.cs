using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfPaging.Events;
using WpfPaging.Messages;
using WpfPaging.Pages;
using WpfPaging.Services;

namespace WpfPaging.ViewModels
{
    public class LogPageViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly EventBus _eventBus;
        private readonly MessageBus _messageBus;

        public ObservableCollection<string> Logs { get; set; } = new ObservableCollection<string>();


        public LogPageViewModel(PageService pageService, EventBus eventBus, MessageBus messageBus)
        {
            _pageService = pageService;
            _eventBus = eventBus;
            _messageBus = messageBus;

            _eventBus.Subscribe<LeaveFromFirstPageEvent>(async @event => Debug.WriteLine($"You leave from fist page"));

            _messageBus.Receive<TextMessage>(this,  async message =>
            {
                await Task.Delay(3000);
                Logs.Add(message.Text);
            });
            _messageBus.Receive<TextMessage>(new object(),  async message => Logs.Add(message.Text));
        }

        public ICommand AppendLog => new DelegateCommand(() =>
        {
            Logs.Add(Guid.NewGuid().ToString());
        });

        public ICommand ChangePage => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Page1());
        });
    }
}
