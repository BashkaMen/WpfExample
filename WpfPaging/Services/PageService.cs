using System;
using System.Windows.Controls;
using WpfPaging.Pages;

namespace WpfPaging.Services
{
    public class PageService
    {

        public event Action<Page> OnPageChanged;
        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
