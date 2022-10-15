using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElloNote.Views.Extensions
{
    internal static class MarkupHelper
    {
        public static MouseButtonEventHandler DragWindow { get; } = (sender, _) =>
        {
            var window = sender as Window;
            window?.DragMove();
        };
    }
}
