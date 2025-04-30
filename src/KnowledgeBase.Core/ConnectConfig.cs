namespace KnowledgeBase.Core;

/// <summary>
/// Конфигурация подключения к базе данных
/// </summary>
/// <param name="ConnectionString">Строка подключения</param>
/// <param name="DatabaseName">Имя базы данных</param>
/// <param name="CollectionName">Имя коллекции</param>
public record ConnectConfig(string ConnectionString, string DatabaseName, string CollectionName);