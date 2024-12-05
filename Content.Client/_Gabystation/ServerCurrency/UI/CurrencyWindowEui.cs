using Content.Client.Eui;
using Content.Shared.Administration;
using Robust.Client.UserInterface.Controls;
using Content.Shared._Gabystation.ServerCurrency.UI;

namespace Content.Client._Gabystation.ServerCurrency.UI
{
    public class CurrencyEui : BaseEui
    {
        private readonly CurrencyWindow _window;
        public CurrencyEui()
        {
            _window = new CurrencyWindow();
            _window.OnClose += () => SendMessage(new CurrencyEuiMsg.Close());
        }
        public override void Opened()
        {
            _window.OpenCentered();
        }
        public override void Closed()
        {
            _window.Close();
        }
    }
}
