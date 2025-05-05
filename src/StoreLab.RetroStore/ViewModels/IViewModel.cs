using StoreLab.RetroStore.Enums;

namespace StoreLab.RetroStore.ViewModels;

public interface IViewModel<TE> where TE : Enum
{
    string State { get; set; }
    CommonActions Actions { get; set; }
    public Tuple<TE, ViewDataMode, object?> ViewData { get; set; }
    void ResetViewData();
}

public enum ViewDataMode
{
    Info,
    Warning,
    Error,
}