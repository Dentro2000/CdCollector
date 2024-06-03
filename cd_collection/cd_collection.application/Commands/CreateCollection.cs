using cd_collection.application.Abstractions;

namespace cd_collection.application.Commands;

public record CreateCollection(string Name): ICommand;