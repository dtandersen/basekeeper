namespace Basekeeper.Command;

public interface CommandHandler<IN>
{
    void Handle(IN command);
}

public interface QueryHandler<IN, OUT>
{
    OUT Handle(IN query);
}
