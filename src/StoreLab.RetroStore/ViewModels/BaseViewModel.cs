using StoreLab.RetroStore.Enums;

namespace StoreLab.RetroStore.ViewModels;

public class BaseViewModel <TE> : IViewModel<TE> where TE : Enum
{
    public string State { get; set; } = string.Empty;
    public CommonActions Actions { get; set; }
    public Tuple<TE, ViewDataMode, object?> ViewData { get; set; } = null!;

    // Add methods to set the ViewData
    public void SetViewData(TE action, ViewDataMode mode, object? data = null)
    {
        ViewData = new Tuple<TE, ViewDataMode, object?>(action, mode, data);
    }

    public void ResetViewData()
    {
        ViewData = null;
    }
}