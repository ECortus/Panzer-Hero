using GameDevUtils.Runtime;

namespace PanzerHero.Runtime.Currency
{
    public class CoinsManager : AbstractCurrencyManager<CoinsManager>
    {
        protected override string CurrencyName => "Coins";
    }
}