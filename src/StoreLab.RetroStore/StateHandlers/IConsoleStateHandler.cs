namespace StoreLab.RetroStore.StateHandlers;

public interface IConsoleStateHandler
{
    Task<ConsoleStateResult<object?>> HandleAsync(ConsoleApp context);
}