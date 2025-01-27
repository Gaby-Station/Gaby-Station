using Content.Server.EUI;
using Content.Shared.Eui;
using Content.Shared._Gabystation.ServerCurrency.UI;
using Robust.Shared.Player;
using Content.Server.Administration.Notes;
using Content.Server._Goobstation.ServerCurrency;

namespace Content.Server._Gabystation.ServerCurrency.UI
{
    public sealed class CurrencyEui : BaseEui
    {
        [Dependency] private readonly ServerCurrencyManager _currencyMan = default!;
        [Dependency] private readonly IAdminNotesManager _notesMan = default!;
        public CurrencyEui()
        {
            IoCManager.InjectDependencies(this);
        }

        public override void Opened()
        {
            StateDirty();
        }

        public override EuiStateBase GetNewState()
        {
            return new CurrencyEuiState();
        }


        public override void HandleMessage(EuiMessageBase msg)
        {
            base.HandleMessage(msg);
            switch (msg)
            {
                case CurrencyEuiMsg.Buy Buy:

                    BuyToken(Buy.BuyId, Player);
                    StateDirty();
                    break; //grrr fix formatting
            }
        }

        private async void BuyToken(BuyIdList buyId, ICommonSession playerName)
        {
            var balance = _currencyMan.GetBalance(Player.UserId);
            switch (buyId) //!shitcode
            {
                case BuyIdList.AntagToken:
                    if (balance < 325)
                        return;
                    await _notesMan.AddAdminRemark(Player, Player.UserId, 0, Loc.GetString("gs-balanceui-remark-token-antag"), 0, false, null);
                    _currencyMan.RemoveCurrency(Player.UserId, 325);
                    break;

                case BuyIdList.GhostToken:
                    if (balance < 450)
                        return;
                    await _notesMan.AddAdminRemark(Player, Player.UserId, 0, Loc.GetString("gs-balanceui-remark-token-ghost"), 0, false, null);
                    _currencyMan.RemoveCurrency(Player.UserId, 450);
                    break;

                case BuyIdList.EventToken:
                    if (balance < 150)
                        return;
                    await _notesMan.AddAdminRemark(Player, Player.UserId, 0, Loc.GetString("gs-balanceui-remark-token-event"), 0, false, null);
                    _currencyMan.RemoveCurrency(Player.UserId, 150);
                    break;
            }
        }
    }
}
