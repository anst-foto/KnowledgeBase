// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;

namespace KnowledgeBase.Core;

public partial class Service : IDisposable
{
    /// <summary>
    /// Освобождение ресурсов объекта.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Освобождение ресурсов объекта.
    /// </summary>
    /// <param name="disposing">Вызывается ли метод вручную (true) или автоматически (false).</param>
    protected virtual void Dispose(bool disposing)
    {
        _client.Dispose();
    }
}