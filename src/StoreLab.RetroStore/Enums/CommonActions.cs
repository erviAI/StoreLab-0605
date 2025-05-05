namespace StoreLab.RetroStore.Enums;

[Flags]
public enum CommonActions
{
    Repeat = 0,
    Select = 1,
    Quit = 2,
    SearchCatalog = 4,
    ToggleSellOrPay = 8,
    Sell = 16,
    Help = 32,
}